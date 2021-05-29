using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SmirnovApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal const string AppName = "AppName";

        internal new static MainWindow MainWindow => (MainWindow)Current.MainWindow;

        /// <summary>
        /// Заполняет базу данных начальными значениями.
        /// </summary>
        /// <param name="force">Заполняет БД данными, даже если БД уже была создана.</param>
        /// <param name="drop">Удаляет базу данных перед заполнением. БД будет пересоздана. Все внесённые изменения в БД будут потеряны.</param>
        private void FillDatabase(bool force = false, bool drop = false)
        {
            using var db = new AppDbContext();

            if (drop)
            {
                db.Database.EnsureDeleted();
            }

            //Заполняет таблицу начальными данными при первом создании БД или при force == true.
            if (!db.Database.EnsureCreated() && !force) return;

            var users = new List<User>
            {
                new()
                {
                    Login = "1",
                    Password = "1"
                },
                new()
                {
                    Login = "ivanov",
                    Password = "123123"
                }
            };
            db.Users.AddRange(users);

            var positions = new List<Position>
            {
                new()
                {
                    Name = "Первая должность"
                },
                new()
                {
                    Name = "Должность два"
                }
            };
            db.Positions.AddRange(positions);

            var owners = new List<Owner>
            {
                new()
                {
                    LastName = "Иванов",
                    FirstName = "Сергей",
                    Patronymic = "Петрович",
                    BirthDate = DateTime.Parse("1994-11-21"),
                    Phone = "+79123214152",
                    ApplicationDate = DateTime.Now.AddDays(-2),
                    LivingAddress = "Россия, г. Батайск, Коммунистическая ул., д. 20 кв.203",
                    RegistrationAddress = "Россия, г. Щёлково, Железнодорожная ул., д. 10 кв.15",
                    PassportSeries = "4780",
                    PassportNumber = "342513",
                    PassportIssueDate = DateTime.Parse("2002-12-17"),
                    PassportIssuedBy = "Отделом УФМС России по г. Батайск"
                },
                new()
                {
                    LastName = "Ефремова",
                    FirstName = "Ольга",
                    Patronymic = "Викторовна",
                    BirthDate = DateTime.Parse("1997-01-17"),
                    Phone = "+79123523625",
                    ApplicationDate = DateTime.Now.AddDays(-14),
                    LivingAddress = "Россия, г. Королёв, Центральная ул., д. 23 кв.101",
                    RegistrationAddress = "Россия, г. Иркутск, Тихая ул., д. 5 кв.211",
                    PassportSeries = "4391",
                    PassportNumber = "840271",
                    PassportIssueDate = DateTime.Parse("2003-11-09"),
                    PassportIssuedBy = "ОУФМС России по г. Королёв"
                },
                new()
                {
                    LastName = "Хлопунов",
                    FirstName = "Олег",
                    Patronymic = "Николаевич",
                    BirthDate = DateTime.Parse("1985-08-01"),
                    Phone = "+79178721461",
                    ApplicationDate = DateTime.Now.AddDays(-5),
                    LivingAddress = "Россия, г. Саратов, Речная ул., д. 19 кв.130",
                    RegistrationAddress = "Россия, г. Ковров, Школьный пер., д. 25 кв.74",
                    PassportSeries = "2156",
                    PassportNumber = "272357",
                    PassportIssueDate = DateTime.Parse("1983-10-12"),
                    PassportIssuedBy = "Отделением УФМС России по г. Саратов",
                }
            };
            db.Owners.AddRange(owners);

            var clients = new List<Client>
            {
                new()
                {
                    LastName = "Пшеницын",
                    FirstName = "Данила",
                    Patronymic = "Владимирович",
                    BirthDate = DateTime.Parse("2000-12-08"),
                    ApplicationDate = DateTime.Now.AddDays(-4),
                    LivingAddress = "Россия, г. Сочи, Белорусская ул., д. 6 кв.34",
                    RegistrationAddress = "Россия, г. Мытищи, Севернаяул., д. 6 кв.27",
                    PassportSeries = "4597",
                    PassportNumber = "953586",
                    PassportIssueDate = DateTime.Parse("2004-01-22"),
                    PassportIssuedBy = "Отделением УФМС России по г. Сочи"
                },
                new()
                {
                    LastName = "Николаев",
                    FirstName = "Сергей",
                    Patronymic = "Сергеевич",
                    BirthDate = DateTime.Parse("2000-04-04"),
                    ApplicationDate = DateTime.Now.AddDays(-1),
                    LivingAddress = "Россия, г. Элиста, Космонавтов ул., д. 15 кв.39",
                    RegistrationAddress = "Россия, г. Нижневартовск, Пионерская ул., д. 10 кв.79",
                    PassportSeries = "4378",
                    PassportNumber = "449572",
                    PassportIssueDate = DateTime.Parse("2008-11-24"),
                    PassportIssuedBy = "Отделением УФМС России по г. Элиста"
                },
                new()
                {
                    LastName = "Шишкина",
                    FirstName = "Ольга",
                    Patronymic = "Сергеевна",
                    BirthDate = DateTime.Parse("1989-07-28"),
                    ApplicationDate = DateTime.Now.AddDays(-40),
                    LivingAddress = "Россия, г. Махачкала, Победы ул., д. 22 кв.159",
                    RegistrationAddress = "Россия, г. Самара, Восточная ул., д. 8 кв.183",
                    PassportSeries = "4590",
                    PassportNumber = "112174",
                    PassportIssueDate = DateTime.Parse("2011-10-05"),
                    PassportIssuedBy = "ОВД России по г. Махачкала"
                }
            };
            db.Clients.AddRange(clients);

            var employees = new List<Employee>
            {
                new()
                {
                    LastName = "Гришин",
                    FirstName = "Александр",
                    Patronymic = "Иванович",
                    BirthDate = DateTime.Parse("1978-03-08"),
                    Phone = "+73214242936",
                    Position = positions[0],
                    Salary = 100500,
                    LivingAddress = "Россия, г. Арзамас, Интернациональная ул., д. 16 кв.189",
                    RegistrationAddress = "Россия, г. Омск, Спортивная ул., д. 21 кв.173",
                    PassportSeries = "4476",
                    PassportNumber = "188380",
                    PassportIssueDate = DateTime.Parse("2007-02-15"),
                    PassportIssuedBy = "Отделением УФМС России по г. Арзамас"
                },
                new()
                {
                    LastName = "Максимова",
                    FirstName = "София",
                    Patronymic = "Данииловна",
                    BirthDate = DateTime.Parse("1988-09-21"),
                    Phone = "+793475629534",
                    Position = positions[0],
                    Salary = 86000,
                    LivingAddress = "Россия, г. Дербент, Пролетарская ул., д. 13 кв.88",
                    RegistrationAddress = "Россия, г. Стерлитамак, Дружная ул., д. 10 кв.203",
                    PassportSeries = "4477",
                    PassportNumber = "534579",
                    PassportIssueDate = DateTime.Parse("2016-03-03"),
                    PassportIssuedBy = "Управление внутренних дел по г. Дербент"
                },
                new()
                {
                    LastName = "Соколов",
                    FirstName = "Дмитрий",
                    Patronymic = "Артёмович",
                    BirthDate = DateTime.Parse("2013-01-30"),
                    Phone = "+7 (970) 981-34-41",
                    Position = positions[1],
                    Salary = 19000,
                    LivingAddress = "Россия, г. Грозный, Луговой пер., д. 1 кв.186",
                    RegistrationAddress = "Россия, г. Каменск - Уральский, Ленина ул., д. 16 кв.181",
                    PassportSeries = "4931",
                    PassportNumber = "763979",
                    PassportIssueDate = DateTime.Parse("2012-10-07"),
                    PassportIssuedBy = "ОВД России по г. Грозный"
                }
            };
            db.Employees.AddRange(employees);

            var estateTypes = new List<EstateType>
            {
                new()
                {
                    Name = "Квартира"
                },
                new()
                {
                    Name = "Частный дом"
                },
                new()
                {
                    Name = "Студия"
                }
            };
            db.EstateTypes.AddRange(estateTypes);

            var estates = new List<Estate>
            {
                new()
                {
                    Name = "Кстати, независимые государства",
                    Address = "Россия, г. Брянск, Почтовая ул., д. 7 кв.97",
                    Area = 120,
                    Cost = 4_000_000,
                    FloorsCount = 1,
                    Floor = 1,
                    Owner = owners[0],
                    Type = estateTypes[0]
                },
                new()
                {
                    Name = "Учитывая ключевые сценарии поведения",
                    Address = "Россия, г. Сочи, Дорожная ул., д. 17 кв.210",
                    Area = 80,
                    Cost = 2_200_000,
                    FloorsCount = 20,
                    Floor = 12,
                    Owner = owners[1],
                    Type = estateTypes[1]
                },
                new()
                {
                    Name = "Банальные, но неопровержимые выводы",
                    Address = "Россия, г. Хабаровск, Заречная ул., д. 24 кв.178",
                    Area = 150,
                    Cost = 14_000_000,
                    FloorsCount = 16,
                    Floor = 3,
                    Owner = owners[2],
                    Type = estateTypes[2]
                },
            };
            db.Estates.AddRange(estates);

            var services = new List<Service>
            {
                new()
                {
                    Name = "Задача организации, в особенности",
                    Cost = 12_000
                },
                new()
                {
                    Name = "Но укрепление и развитие",
                    Cost = 6_600
                }
            };
            db.Services.AddRange(services);

            var contracts = new List<Contract>
            {
                new()
                {
                    Name = "В рамках спецификации современных стандартов",
                    Amount = 120_000,
                    Client = clients[0],
                    Employee = employees[0],
                    Estate = estates[0],
                    Service = services[0],
                    Date = DateTime.Parse("2021-03-29"),
                    Status = ContractStatus.ServiceProvided
                },
                new()
                {
                    Name = "Внезапно, интерактивные прототипы неоднозначны",
                    Amount = 1_400_000,
                    Client = clients[1],
                    Employee = employees[1],
                    Estate = estates[1],
                    Service = services[1],
                    Date = DateTime.Parse("2021-04-26"),
                    Status = ContractStatus.NotPerformed
                },
                new()
                {
                    Name = "Значимость этих проблем настолько очевидна",
                    Amount = 800_000,
                    Client = clients[2],
                    Employee = employees[2],
                    Estate = estates[2],
                    Service = services[1],
                    Date = DateTime.Parse("2021-05-11"),
                    Status = ContractStatus.NotPerformed
                }
            };
            db.Contracts.AddRange(contracts);

            db.SaveChanges();
        }

        public App()
        {
            FillDatabase(force: false, drop: false);
        }
    }
}
