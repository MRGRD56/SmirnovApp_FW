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
using System.Windows.Input;
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
    public class ContractsPageViewModel : ItemsListViewModel<Contract>, ICrudViewModel
    {
        private string _contractNameSearchQuery = "";
        private string _employeeSearchQuery = "";
        private string _clientSearchQuery = "";
        private string _ownerSearchQuery = "";
        private bool _isSearchExpanded;
        private DateTime? _dateSearchQuery;
        private int _foundContractsCount;
        private int _foundClientsCount;
        private int _foundServicesCount;
        private int _lastMonthClientsCount;
        private int _lastMonthServicesCount;

        public ContractsPageViewModel()
            : base(typeof(Service), typeof(IndividualClient), typeof(LegalEntityClient), typeof(Employee), typeof(Estate), typeof(Owner))
        {
            ContractsView = CollectionViewSource.GetDefaultView(Items);
            ContractsView.Filter += ContractsViewFilter;
            ItemsLoaded += OnItemsLoaded;
        }

        private void OnItemsLoaded(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private bool ContractsViewFilter(object item)
        {
            var contract = (Contract)item;

            return CheckMatch(ContractNameSearchQuery, contract.Name)
                && CheckMatch(EmployeeSearchQuery, contract.Employee.FullName)
                && CheckMatch(ClientSearchQuery, contract.Client.FullName)
                && CheckMatch(OwnerSearchQuery, contract.Estate.Owner.FullName)
                && CheckDateMatch(DateSearchQuery, contract.Date);
        }

        private static bool CheckMatch(string query, string value)
        {
            return string.IsNullOrWhiteSpace(query)
                || value.Trim().ToLower().Contains(query.Trim().ToLower());
        }

        private static bool CheckDateMatch(DateTime? query, DateTime value) =>
            !query.HasValue || query.Value.Date == value.Date;

        private void RefreshContracts()
        {
            ContractsView.Refresh();
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            var foundContracts = ContractsView.Cast<Contract>().ToList();
            FoundContractsCount = foundContracts.Count;
            FoundClientsCount = GetDifferentClientsCount(foundContracts);
            FoundServicesCount = GetDifferentServicesCount(foundContracts);

            var lastMonthContracts = Items
                .Where(contract => contract.Date.Date >= DateTime.Today.AddDays(-30))
                .ToList();
            LastMonthClientsCount = GetDifferentClientsCount(lastMonthContracts);
            LastMonthServicesCount = GetDifferentServicesCount(lastMonthContracts);
        }

        private static int GetDifferentServicesCount(List<Contract> contracts) => 
            contracts.GroupBy(contract => contract.Service.Id).Count();

        private static int GetDifferentClientsCount(List<Contract> contracts) => 
            contracts.GroupBy(contract => contract.Client.Id).Count();

        public string ContractNameSearchQuery
        {
            get => _contractNameSearchQuery;
            set
            {
                _contractNameSearchQuery = value;
                OnPropertyChanged();
                RefreshContracts();
            }
        }

        public string EmployeeSearchQuery
        {
            get => _employeeSearchQuery;
            set
            {
                _employeeSearchQuery = value;
                OnPropertyChanged();
                RefreshContracts();
            }
        }

        public string ClientSearchQuery
        {
            get => _clientSearchQuery;
            set
            {
                _clientSearchQuery = value;
                OnPropertyChanged();
                RefreshContracts();
            }
        }

        public string OwnerSearchQuery
        {
            get => _ownerSearchQuery;
            set
            {
                _ownerSearchQuery = value;
                OnPropertyChanged();
                RefreshContracts();
            }
        }

        public DateTime? DateSearchQuery
        {
            get => _dateSearchQuery;
            set
            {
                _dateSearchQuery = value;
                OnPropertyChanged();
                RefreshContracts();
            }
        }

        public ICollectionView ContractsView { get; }

        public bool IsSearchExpanded
        {
            get => _isSearchExpanded;
            set
            {
                _isSearchExpanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSearchNotExpanded));
            }
        }

        public bool IsSearchNotExpanded => !IsSearchExpanded;

        public int FoundContractsCount
        {
            get => _foundContractsCount;
            set
            {
                _foundContractsCount = value;
                OnPropertyChanged();
            }
        }

        public int FoundClientsCount
        {
            get => _foundClientsCount;
            set
            {
                _foundClientsCount = value;
                OnPropertyChanged();
            }
        }
        public int FoundServicesCount
        {
            get => _foundServicesCount;
            set
            {
                _foundServicesCount = value;
                OnPropertyChanged();
            }
        }

        public int LastMonthClientsCount 
        { 
            get => _lastMonthClientsCount; 
            set 
            {
                _lastMonthClientsCount = value;
                OnPropertyChanged();
            } 
        }
        public int LastMonthServicesCount 
        { 
            get => _lastMonthServicesCount; 
            set 
            {
                _lastMonthServicesCount = value;
                OnPropertyChanged();
            } 
        }

        public ICommand ToggleExpandSearchCommand => new Command(_ =>
        {
            IsSearchExpanded = !IsSearchExpanded;
        });

        public ICommand AddCommand => new Command(async _ =>
        {
            var dialogWindow = new ContractEditDialogWindow();
            if (dialogWindow.ShowDialog() != true) return;

            var contract = dialogWindow.Contract;
            using (var db = new AppDbContext())
            {
                var dbContract = new Contract();
                await dbContract.CopyPropertiesAsync(contract, db);
                await db.AddAsync(dbContract);
                await db.SaveChangesAsync();
                Items.Add(dbContract);
                UpdateStatistics();
            }
        });

        public ICommand EditCommand => new Command(async _ =>
        {
            var dialogWindow = new ContractEditDialogWindow(SelectedItem);
            if (dialogWindow.ShowDialog() != true) return;

            var contract = dialogWindow.Contract;
            using (var db = new AppDbContext())
            {
                var dbContract = await db.Contracts.FindAsync(contract.Id);
                await dbContract.CopyPropertiesAsync(contract, db);
                await SelectedItem.CopyPropertiesAsync(dbContract, db);
                await db.SaveChangesAsync();
                UpdateStatistics();
            }
        }, _ => SelectedItem != null);
        
        public ICommand RemoveCommand => new Command(async _ =>
        {
            var mbox = MessageBox.Show($"Удалить договор №{SelectedItem.Id}?", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbox != MessageBoxResult.OK) return;

            using (var db = new AppDbContext())
            {
                var dbContract = await db.Contracts.FindAsync(SelectedItem.Id);
                db.Remove(dbContract);
                Items.Remove(SelectedItem);
                await db.SaveChangesAsync();
                UpdateStatistics();
            }
        }, _ => SelectedItem != null);

        /// <summary>
        /// Создание документа по выбранному договору.
        /// </summary>
        public ICommand CreateDocumentCommand => new Command(_ =>
        {
            var fileName = $"{SelectedItem.Name.Replace(" ", "_")}_{DateTime.Now:yyyy-MM-ddTHH-mm-ss}.docx";

            fileName = GetPathToSaveFile("*.docx|*.docx", fileName);
            if (fileName == null) return;

            SaveContractDocumentToDocx(fileName);
        }, _ => SelectedItem != null);

        /// <summary>
        /// Экспорт таблицы всех договоров в DOCX.
        /// </summary>
        public ICommand ExportAllCommand => new Command(_ =>
        {
            var fileName = $"Договоры_{DateTime.Now:yyyy-MM-ddTHH-mm-ss}.docx";

            fileName = GetPathToSaveFile("*.docx|*.docx", fileName);
            if (fileName == null) return;

            SaveContractsTableToDocx(fileName);
        });

        public ICommand ResetFilterCommand => new Command(_ =>
        {
            ContractNameSearchQuery = "";
            EmployeeSearchQuery = "";
            ClientSearchQuery = "";
            OwnerSearchQuery = "";
            DateSearchQuery = null;
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
            using (var templateStream = assembly.GetManifestResourceStream(templateResourceName))
            {
                var document = new XWPFDocument(templateStream);

                var replaces = new List<(string From, string To)>
                {
                    ("CurrentDateYear", DateTime.Now.Year.ToString()),
                    ("OwnerFullName", SelectedItem.Estate.Owner.FullName),
                    ("OwnerPassportSeries", SelectedItem.Estate.Owner.PassportSeries),
                    ("OwnerPassportNumber", SelectedItem.Estate.Owner.PassportNumber),
                    ("OwnerPassportIssuedInfo", SelectedItem.Estate.Owner.PassportIssued),
                    ("OwnerLivingAddress", SelectedItem.Estate.Owner.LivingAddress),
                    ("ClientFullName", SelectedItem.Client.FullName),
                    ("ClientPassportSeries", SelectedItem.Client.PassportSeries),
                    ("ClientPassportNumber", SelectedItem.Client.PassportNumber),
                    ("ClientPassportIssuedInfo", SelectedItem.Client.PassportIssued),
                    ("ClientLivingAddress", SelectedItem.Client.LivingAddress),
                    ("EstateAddress", SelectedItem.Estate.Address),
                    ("EstateEffectiveArea", SelectedItem.Estate.Area.ToString()),
                    //("EstateLivingArea", SelectedItem.Estate.Area.ToString()),
                    ("EstateCost", SelectedItem.Estate.Cost.ToString()),
                    ("ContractAmount", SelectedItem.Amount.ToString()),
                    ("OwnerPassportRegistrationAddress", SelectedItem.Estate.Owner.RegistrationAddress),
                    ("OwnerPassportPostAddress", SelectedItem.Estate.Owner.LivingAddress),
                    ("OwnerPassportFullNumber", SelectedItem.Estate.Owner.PassportFullNumber),
                    ("OwnerPassportIssuedBy", SelectedItem.Estate.Owner.PassportIssuedBy),
                    ("OwnerPassportIssueDate", SelectedItem.Estate.Owner.PassportIssueDate.ToString("dd.MM.yyyy")),
                    ("OwnerPhone", SelectedItem.Estate.Owner.Phone),
                    ("ClientPassportRegistrationAddress", SelectedItem.Client.RegistrationAddress),
                    ("ClientPassportPostAddress", SelectedItem.Client.LivingAddress),
                    ("ClientPassportFullNumber", SelectedItem.Client.PassportFullNumber),
                    ("ClientPassportIssuedBy", SelectedItem.Client.PassportIssuedBy),
                    ("ClientPassportIssueDate", SelectedItem.Client.PassportIssueDate.ToString("dd.MM.yyyy")),
                    ("ClientPhone", SelectedItem.Estate.Owner.Phone),
                };

                //Заменяем параметры в документе на значения.
                replaces.ForEach(x => document.ReplaceText(x.From, x.To));

                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    document.Write(fileStream);
                }
            }

            Process.Start("explorer.exe", $"/select,\"{fileName}\"");
        }
    }
}
