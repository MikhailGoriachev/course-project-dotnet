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
using System.Windows.Threading;

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
        public MainWindow() : this(new HotelController()) { }


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


        // демнострация таблицы Персоны
        private async void FillTables_Executed(object sender, ExecutedRoutedEventArgs e) => _controller.FillDataBase(new DateTime(2022, 02, 01));


        // демнострация таблицы График уборки
        private async void ShowTableCleaningSchedule_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetCleaningSchedule));

            // вывод наименование таблицы
            GbxTable.Header = "График уборки";
        }


        // демнострация таблицы Дни недели
        private async void ShowTableDaysOfWeek_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetDaysOfWeek));

            // вывод наименование таблицы
            GbxTable.Header = "Дни недели";
        }



        // демнострация таблицы История фактов уборки
        private async void ShowTableCleaningHistory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetCleaningHistory));

            // вывод наименование таблицы
            GbxTable.Header = "История фактов уборки";
        }



        // демнострация таблицы История поселений в гостиницу
        private async void ShowTableHistoryRegistrationHotel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetHistoryRegistrationHotel));

            // вывод наименование таблицы
            GbxTable.Header = "История поселений в гостиницу";
        }



        // демнострация таблицы Города
        private async void ShowTableCities_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetCities));

            // вывод наименование таблицы
            GbxTable.Header = "Города";
        }



        // демнострация таблицы Номера гостиницы
        private async void ShowTableHotelRooms_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetHotelRooms));

            // вывод наименование таблицы
            GbxTable.Header = "Номера гостиницы";
        }



        // демнострация таблицы Типы номеров
        private async void ShowTableTypesHotelRoom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetTypesHotelRoom));

            // вывод наименование таблицы
            GbxTable.Header = "Типы номеров";
        }



        // демнострация таблицы Этажи
        private async void ShowTableFloors_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetFloors));

            // вывод наименование таблицы
            GbxTable.Header = "Этажи";
        }



        // демнострация таблицы Служащие гостиницы
        private async void ShowTableEmployees_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetEmployees));

            // вывод наименование таблицы
            GbxTable.Header = "Служащие гостиницы";
        }



        // демнострация таблицы Клиенты
        private async void ShowTableClients_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetClients));

            // вывод наименование таблицы
            GbxTable.Header = "Клиенты";
        }



        // демнострация таблицы Персоны
        private async void ShowTablePersons_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // вывод наименование таблицы
            GbxTable.Header = "Загрузка...";

            // заполнение DataGrid
            UpdateBinding(DgdTableData, await Task.Run(_controller.GetPersons));

            // вывод наименование таблицы
            GbxTable.Header = "Персоны";
        }


        #endregion


        #region Методы


        // заполнение базы данных данными
        //private void FillDataBase_Click(object sender, RoutedEventArgs e) => _controller.FillDataBase(DateTime.Now.AddDays(-80));
        private void FillDataBase_Click(object sender, RoutedEventArgs e) => _controller.FillDataBase(new DateTime(2022, 02, 01));


        #region Общие методы

        // обновление привязки в DataGrid
        public void UpdateBinding(DataGrid grid, IEnumerable collection)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                grid.ItemsSource = null;
                grid.ItemsSource = collection;
            }));
        }

        #endregion

        #endregion

    }
}
