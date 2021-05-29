using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Win32;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Converters;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ContractsPageViewModel : ItemsListViewModel<Contract>
    {
        private string _contractNameSearchQuery = "";
        private string _employeeSearchQuery = "";
        private string _clientSearchQuery = "";
        private string _ownerSearchQuery = "";

        public ContractsPageViewModel()
            : base(typeof(Client), typeof(Employee), typeof(Estate), typeof(Service), typeof(Owner))
        {
            ContractsView = CollectionViewSource.GetDefaultView(Items);
            ContractsView.Filter += ContractsViewFilter;
        }

        private bool ContractsViewFilter(object o)
        {
            var contract = (Contract)o;

            return CheckMatch(ContractNameSearchQuery, contract.Name)
                && CheckMatch(EmployeeSearchQuery, contract.Employee.FullName)
                && CheckMatch(ClientSearchQuery, contract.Client.FullName)
                && CheckMatch(OwnerSearchQuery, contract.Estate.Owner.FullName);
        }

        private static bool CheckMatch(string query, string value)
        {
            return string.IsNullOrWhiteSpace(query)
                || value.Trim().ToLower().Contains(query.Trim().ToLower());
        }

        public string ContractNameSearchQuery
        {
            get => _contractNameSearchQuery;
            set
            {
                _contractNameSearchQuery = value;
                OnPropertyChanged();
                ContractsView.Refresh();
            }
        }

        public string EmployeeSearchQuery
        {
            get => _employeeSearchQuery;
            set
            {
                _employeeSearchQuery = value;
                OnPropertyChanged();
                ContractsView.Refresh();
            }
        }

        public string ClientSearchQuery
        {
            get => _clientSearchQuery;
            set
            {
                _clientSearchQuery = value;
                OnPropertyChanged();
                ContractsView.Refresh();
            }
        }

        public string OwnerSearchQuery
        {
            get => _ownerSearchQuery;
            set
            {
                _ownerSearchQuery = value;
                OnPropertyChanged();
                ContractsView.Refresh();
            }
        }

        public ICollectionView ContractsView { get; }

        public Contract SelectedContract { get; set; }

        public Command AddCommand => new(async _ =>
        {
            var dialogWindow = new ContractEditDialogWindow();
            if (dialogWindow.ShowDialog() != true) return;

            var contract = dialogWindow.Contract;
            await using var db = new AppDbContext();
            var dbContract = new Contract();
            await dbContract.CopyPropertiesAsync(contract, db);
            await db.AddAsync(dbContract);
            await db.SaveChangesAsync();
            Items.Add(dbContract);
        });

        public Command EditCommand => new(async _ =>
        {
            var dialogWindow = new ContractEditDialogWindow(SelectedContract);
            if (dialogWindow.ShowDialog() != true) return;

            var contract = dialogWindow.Contract;
            await using var db = new AppDbContext();
            var dbContract = await db.Contracts.FindAsync(contract.Id);
            await dbContract.CopyPropertiesAsync(contract, db);
            await SelectedContract.CopyPropertiesAsync(dbContract, db);
            await db.SaveChangesAsync();
        }, _ => SelectedContract != null);

        public Command RemoveCommand => new(async _ =>
        {
            var mbox = MessageBox.Show($"Удалить договор №{SelectedContract.Id}?", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbox != MessageBoxResult.OK) return;

            await using var db = new AppDbContext();
            var dbContract = await db.Contracts.FindAsync(SelectedContract.Id);
            db.Remove(dbContract);
            Items.Remove(SelectedContract);
            await db.SaveChangesAsync();
        }, _ => SelectedContract != null);

        /// <summary>
        /// Создание документа по выбранному договору.
        /// </summary>
        public Command CreateDocumentCommand => new(_ =>
        {
            var fileName = $"{SelectedContract.Name.Replace(" ", "_")}_{DateTime.Now:yyyy-MM-ddTHH-mm-ss}.docx";

            fileName = GetPathToSaveFile("*.docx|*.docx", fileName);
            if (fileName == null) return;

            SaveContractDocumentToDocx(fileName);
        }, _ => SelectedContract != null);

        /// <summary>
        /// Экспорт таблицы всех договоров в DOCX.
        /// </summary>
        public Command ExportAllCommand => new(_ =>
        {
            var fileName = $"Договоры_{DateTime.Now:yyyy-MM-ddTHH-mm-ss}.docx";

            fileName = GetPathToSaveFile("*.docx|*.docx", fileName);
            if (fileName == null) return;

            SaveContractsTableToDocx(fileName);
        });

        public Command ResetFilterCommand => new(_ => 
        {
            ContractNameSearchQuery = "";
            EmployeeSearchQuery = "";
            ClientSearchQuery = "";
            OwnerSearchQuery = "";
        });

        /// <summary>
        /// Получает путь для сохранения файла путём открытия <see cref="SaveFileDialog"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetPathToSaveFile(string filter, string fileName = null)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = filter;
            if (fileName != null)
            {
                saveFileDialog.FileName = fileName;
            }
            var dialogResult = saveFileDialog.ShowDialog();

            return dialogResult == true ? saveFileDialog.FileName : null;
        }

        /// <summary>
        /// Сохраняет таблицу с договорами в DOCX-документ.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveContractsTableToDocx(string fileName)
        {
            var document = new XWPFDocument();
            const ulong documentMargin = 200;
            //Задаём настройки документа.
            document.Document.body.sectPr = new CT_SectPr 
            {
                //Устанавливаем отступы от краёв.
                pgMar = new CT_PageMar
                {
                    header = documentMargin,
                    right = documentMargin,
                    footer = documentMargin,
                    left = documentMargin,
                    top = documentMargin.ToString(),
                    bottom = documentMargin.ToString(),
                    gutter = 0
                }
            };
            var paragraph = document.CreateParagraph();
            var titleRun = paragraph.CreateRun();
            titleRun.SetText($"Учёт клиентов за {DateTime.Now:dd.MM.yyyy}");
            titleRun.FontSize = 18;

            var titles = new List<string>
            {
                "№", "Название", "Сумма", "Дата", "Статус", "Клиент", "Сотрудник", "Владелец", "Имущество"
            };

            var rows = Items.Count + 1;
            var cols = titles.Count;
            var table = document.CreateTable(rows, cols);
            //Устанавливаем отступы для каждой ячейки таблицы.
            table.SetCellMargins(50, 50, 50, 50);

            //Добавляем заголовки столбцов.
            for (int colIndex = 0; colIndex < cols; colIndex++)
            {
                var cell = table.GetRow(0).GetCell(colIndex);
                cell.SetText(titles[colIndex]);
            }

            //Заполняем данные.
            for (int rowIndex = 1; rowIndex < rows; rowIndex++)
            {
                //Текущая строка.
                var row = table.GetRow(rowIndex);

                //Договор, соответствующий текущей строке.
                var contract = Items[rowIndex - 1];

                //Данные, которыми будут заполнены ячейки строки. Индекс элемента соответствует индексу ячейки в строке.
                var rowData = new List<string>
                {
                    contract.Id.ToString(),
                    contract.Name,
                    contract.Amount.ToString("C"),
                    contract.Date.ToString("dd.MM.yyyy"),
                    ContractStatusConverter.GetString(contract.Status),
                    contract.Client.FullName,
                    contract.Employee.FullName,
                    contract.Estate.Owner.FullName,
                    contract.Estate.Address
                };

                //Заполняем ячейки строки данными.
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    var cell = row.GetCell(colIndex);
                    cell.SetText(rowData[colIndex]);
                }
            }

            //Сохраняем в файл.
            using (var stream = new FileStream(fileName, FileMode.Create)) 
            {
                document.Write(stream);
            }

            //Открываем проводник с выделением на созданном файле.
            Process.Start("explorer.exe", $"/select,\"{fileName}\"");
            //Также, мы можем открыть сразу сам файл:
            //Process.Start(fileName);
        }

        /// <summary>
        /// Сохраняет документ по текущему договору.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveContractDocumentToDocx(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string templateResourceName = "SmirnovApp.Resources.Templates.ContractTemplate.docx";
            using var templateStream = assembly.GetManifestResourceStream(templateResourceName);

            var document = new XWPFDocument(templateStream);

            var replaces = new List<(string From, string To)>
            {
                ("CurrentDateYear", DateTime.Now.Year.ToString()),
                ("OwnerFullName", SelectedContract.Estate.Owner.FullName),
                ("OwnerPassportSeries", SelectedContract.Estate.Owner.PassportSeries),
                ("OwnerPassportNumber", SelectedContract.Estate.Owner.PassportNumber),
                ("OwnerPassportIssuedInfo", SelectedContract.Estate.Owner.PassportIssued),
                ("OwnerLivingAddress", SelectedContract.Estate.Owner.LivingAddress),
                ("ClientFullName", SelectedContract.Client.FullName),
                ("ClientPassportSeries", SelectedContract.Client.PassportSeries),
                ("ClientPassportNumber", SelectedContract.Client.PassportNumber),
                ("ClientPassportIssuedInfo", SelectedContract.Client.PassportIssued),
                ("ClientLivingAddress", SelectedContract.Client.LivingAddress),
                ("EstateAddress", SelectedContract.Estate.Address),
                ("EstateEffectiveArea", SelectedContract.Estate.Area.ToString()),
                //("EstateLivingArea", SelectedContract.Estate.Area.ToString()),
                ("EstateCost", SelectedContract.Estate.Cost.ToString()),
                ("ContractAmount", SelectedContract.Amount.ToString()),
                ("OwnerPassportRegistrationAddress", SelectedContract.Estate.Owner.RegistrationAddress),
                ("OwnerPassportPostAddress", SelectedContract.Estate.Owner.LivingAddress),
                ("OwnerPassportFullNumber", SelectedContract.Estate.Owner.PassportFullNumber),
                ("OwnerPassportIssuedBy", SelectedContract.Estate.Owner.PassportIssuedBy),
                ("OwnerPassportIssueDate", SelectedContract.Estate.Owner.PassportIssueDate.ToString("dd.MM.yyyy")),
                ("OwnerPhone", SelectedContract.Estate.Owner.Phone),
                ("ClientPassportRegistrationAddress", SelectedContract.Client.RegistrationAddress),
                ("ClientPassportPostAddress", SelectedContract.Client.LivingAddress),
                ("ClientPassportFullNumber", SelectedContract.Client.PassportFullNumber),
                ("ClientPassportIssuedBy", SelectedContract.Client.PassportIssuedBy),
                ("ClientPassportIssueDate", SelectedContract.Client.PassportIssueDate.ToString("dd.MM.yyyy")),
                ("ClientPhone", SelectedContract.Estate.Owner.Phone),
            };

            //Заменяем параметры в документе на значения.
            replaces.ForEach(x => document.ReplaceText(x.From, x.To));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                document.Write(fileStream);
            }

            Process.Start("explorer.exe", $"/select,\"{fileName}\"");
        }
    }
}
