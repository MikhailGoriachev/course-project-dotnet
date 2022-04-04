using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HotelClassLibrary.Controllers;        // контроллеры
using HotelClassLibrary.Utilities;          // утилиты
using HotelClassLibrary.Models;             // модели
using HotelClassLibrary.Context;            // базы данных


using System.Windows.Threading;
using System.Configuration;

namespace HotelApplicationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // контроллер заполнения данными БД
        private HotelController _controller;

        public HotelController Controller
        {
            get => _controller;
            set => _controller = value;
        }


        #region Конструкторы

        // конструктор по умолчанию
        public MainWindow() : this(new HotelController(new HotelDB())) { }


        // конструктор инициализирующий
        public MainWindow(HotelController fillDataController)
        {
            InitializeComponent();

            // установка значений
            _controller = fillDataController;
        }

        #endregion

        #region Команды

        // выход из приложения
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e) => Application.Current.Shutdown();


        // заполнение таблиц
        private async void FillTables_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // индикация загрузки
            UpdateGroupBox(GbxTable, $"Заполнение...");

            // очистка DataGrid
            ClearGrid(DgdTableData);

            // заполнение базы данных
            await _controller.FillDataBase(DateTime.Now.AddDays(-Utils.GetRand(40, 60)));

            // индикация загрузки
            UpdateGroupBox(GbxTable, $"Заполнение окончено!");
        }


        #region Комнады по заданию на 31.03.2022


        // команда - Клиенты проживающие в заданном номере
        private async void Query1_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // индикация загрузки
                UpdateGroupBox(GbxTable, $"Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // текущая дата
                DateTime date = DateTime.Now;

                // список занятых комнат
                List<HotelRoom> rooms = _controller.GetHotelRoomsAsync().Result.Where(h => _controller.RoomIsBusy(h, date)).ToList();

                // номер комнаты
                int number = rooms[Utils.GetRand(1, rooms.Count)].Number;

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.Proc1(number)
                                                       .Select(c => new {
                                                           c.Id,
                                                           c.Person.Surname,
                                                           c.Person.Name,
                                                           c.Person.Patronymic,
                                                           c.Passport,
                                                           c.IsDeleted
                                                       }));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, $"Запрос 1. Клиенты проживающие в заданном номере. Выбранный номер комнаты № {number}");
            });


        // команда - Клиенты прибывши из заданного города
        private async void Query2_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // индикация загрузки
                UpdateGroupBox(GbxTable, $"Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // города
                List<City> cities = _controller.GetCitiesAsync().Result;

                // выбранный город
                string city = cities[Utils.GetRand(0, cities.Count)].Name;

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.Proc2(city)
                                                       .Select(c => new {
                                                           c.Id,
                                                           c.Person.Surname,
                                                           c.Person.Name,
                                                           c.Person.Patronymic,
                                                           c.Passport,
                                                           c.IsDeleted
                                                       }));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, $"Запрос 2. Клиенты прибывши из заданного города. Выбранный город {city}");
            });


        // команда - Работник убиравший выбранный номер в заданную дату
        private async void Query3_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // индикация загрузки
                UpdateGroupBox(GbxTable, $"Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // список регистраций в отеле
                List<HistoryRegistrationHotel> list = _controller.GetHistoryRegistrationHotelAsync().Result;

                // запись уборки
                HistoryRegistrationHotel elem = list[Utils.GetRand(0, list.Count)];

                // дата уборки
                DateTime date = elem.RegistrationDate.AddDays(Utils.GetRand(0, elem.Duration));

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.Proc3(elem.Client.Passport, date)
                                                       .Select(em => new {
                                                           em.Id,
                                                           em.Person.Surname,
                                                           em.Person.Name,
                                                           em.Person.Patronymic,
                                                           em.IsDeleted
                                                       }));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, $"Запрос 3. Работник 6убиравший выбранный номер в заданную дату. Выбранные " +
                    $"клиент и дата: {elem.Client.Person.Surname} {elem.Client.Person.Name[0]}. {elem.Client.Person.Patronymic[0]}." +
                    $" ({elem.Client.Passport}) | {date:d}");
            });


        // команда - Свободные места в гостинице
        private async void Query4_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // индикация загрузки
                UpdateGroupBox(GbxTable, $"Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // текущая дата
                DateTime date = DateTime.Now;

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.Proc4()
                                                       .Select(r => new {
                                                           r.Id,
                                                           TypeRoom = r.TypeHotelRoom.Name,
                                                           r.TypeHotelRoom.CountRooms,
                                                           r.TypeHotelRoom.Price,
                                                           Floor = r.Floor.Number,
                                                           RoomNubmer = r.Number,
                                                           IsBusy = _controller.RoomIsBusy(r, date)
                                                       })
                                                       .ToList());

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Запрос 4. Свободные места в гостинице");

            });


        #endregion


        #region Демонстрация данных таблиц

        // демнострация таблицы График уборки
        private async void ShowTableCleaningSchedule_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, $"Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.GetCleaningScheduleAsync().Result
                                                       .Select(c => new
                                                       {
                                                           c.Id,
                                                           DayOfWeek = c.DayOfWeek.Name,
                                                           Employee = $"{c.Employee.Person.Surname} {c.Employee.Person.Name[0]}. " +
                                                                       $"{c.Employee.Person.Patronymic[0]}.",
                                                           Floor = c.Floor.Number
                                                       }));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, $"График уборки");
            });


        // демнострация таблицы Дни недели
        private async void ShowTableDaysOfWeek_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetDaysOfWeekAsync().Result));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Дни недели");
            });



        // демнострация таблицы История фактов уборки
        private async void ShowTableCleaningHistory_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetCleaningHistoryAsync().Result
                                                                      .Select(c => new
                                                                      {
                                                                          c.Id,
                                                                          Floor = c.Floor.Number,
                                                                          DateCleaning = $"{c.DateCleaning:d}",
                                                                          Employee = $"{c.Employee.Person.Surname} {c.Employee.Person.Name[0]}. " +
                                                                                     $"{c.Employee.Person.Patronymic[0]}.",
                                                                          c.IsDeleted
                                                                      })));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "История фактов уборки");
            });



        // демнострация таблицы История поселений в гостиницу
        private async void ShowTableHistoryRegistrationHotel_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetHistoryRegistrationHotelAsync().Result
                                                                      .Select(h => new
                                                                      {
                                                                          h.Id,
                                                                          Client = $"{h.Client.Person.Surname} {h.Client.Person.Name[0]}. " +
                                                                                     $"{h.Client.Person.Patronymic[0]}.",
                                                                          h.Client.Passport,
                                                                          Floor = h.HotelRoom.Floor.Number,
                                                                          Room = h.HotelRoom.Number,
                                                                          City = h.City.Name,
                                                                          RegistrationDate = $"{h.RegistrationDate:d}",
                                                                          h.Duration,
                                                                          h.IsDeleted
                                                                      })));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "История поселений в гостиницу");
            });



        // демнострация таблицы Города
        private async void ShowTableCities_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetCitiesAsync().Result));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Города");
            });



        // демнострация таблицы Номера гостиницы
        private async void ShowTableHotelRooms_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // текущая дата
                DateTime date = DateTime.Now;

                // заполнение DataGrid
                UpdateBinding(DgdTableData, _controller.GetHotelRoomsAsync().Result
                                                       .Select(r => new
                                                       {
                                                           r.Id,
                                                           TypeRoom = r.TypeHotelRoom.Name,
                                                           r.TypeHotelRoom.CountRooms,
                                                           r.TypeHotelRoom.Price,
                                                           Floor = r.Floor.Number,
                                                           RoomNubmer = r.Number,
                                                           IsBusy = _controller.RoomIsBusy(r, date)
                                                       })
                                                       .ToList());

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Номера гостиницы");
            });


        // демнострация таблицы Типы номеров
        private async void ShowTableTypesHotelRoom_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetTypesHotelRoomAsync().Result));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Типы номеров");
            });


        // демнострация таблицы Этажи
        private async void ShowTableFloors_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetFloorsAsync().Result));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Этажи");
            });


        // демнострация таблицы Служащие гостиницы
        private async void ShowTableEmployees_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetEmployeesAsync().Result
                                                                      .Select(em => new
                                                                      {
                                                                          em.Id,
                                                                          em.Person.Surname,
                                                                          em.Person.Name,
                                                                          em.Person.Patronymic,
                                                                          em.IsDeleted
                                                                      })));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Служащие гостиницы");
            });


        // демнострация таблицы Клиенты
        private async void ShowTableClients_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetClientsAsync().Result
                                                                      .Select(c => new
                                                                      {
                                                                          c.Id,
                                                                          c.Person.Surname,
                                                                          c.Person.Name,
                                                                          c.Person.Patronymic,
                                                                          c.Passport,
                                                                          c.IsDeleted
                                                                      })));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Клиенты");
            });



        // демнострация таблицы Персоны
        private async void ShowTablePersons_Executed(object sender, ExecutedRoutedEventArgs e) =>
            await Task.Run(() =>
            {
                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Загрузка...");

                // очистка DataGrid
                ClearGrid(DgdTableData);

                // заполнение DataGrid
                Task.Run(() => UpdateBinding(DgdTableData, _controller.GetPersonsAsync().Result));

                // вывод наименование таблицы
                UpdateGroupBox(GbxTable, "Персоны");
            });


        #endregion

        #endregion


        #region Методы


        #region Общие методы

        // обновление привязки в DataGrid
        public void UpdateBinding(DataGrid grid, IEnumerable collection)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                grid.ItemsSource = null;
                grid.ItemsSource = collection;

                // вывод количества элементов
                ShowCountElem(grid.Items.Count);
            }));
        }


        // очистка DataGrid
        public void ClearGrid(DataGrid grid)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                grid.ItemsSource = null;

                // вывод количества элементов
                ShowCountElem(-1);
            }));
        }


        // обновить текстовку на GroupBox
        public void UpdateGroupBox(GroupBox groupBox, string header)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                groupBox.Header = header;
            }));
        }


        // вывод количество элементов в информационные блоки
        // если count == -1, то вывод "——"
        public void ShowCountElem(int count)
        {
            LblCount.Content = $"Количество элементов: {(count == -1 ? "——" : $"{count}")}";
        }

        #endregion

        #endregion

    }
}
