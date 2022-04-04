using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelApplicationWPF.Models
{
    // команды окна
    public class WindowCommands
    {

        // команда - выхода из приложения
        public static RoutedUICommand Exit { get; set; }

        // команда - заполнение таблиц данными
        public static RoutedUICommand FillTables { get; set; }


        #region Комнады для задания на 24.03.2022

        // команда - Клиенты проживающие в заданном номере
        public static RoutedUICommand Query1 { get; set; }

        // команда - Клиенты прибывшие из заданного города
        public static RoutedUICommand Query2 { get; set; }

        // команда - Работник убиравший выбранный номер в заданную дату
        public static RoutedUICommand Query3 { get; set; }

        // команда - Свободные места в гостинице
        public static RoutedUICommand Query4 { get; set; }

        #endregion


        #region Команды демонстрации таблиц

        // команда - демонстрация таблицы График уборки
        public static RoutedUICommand ShowTableCleaningSchedule { get; set; }

        // команда - демонстрация таблицы Дни недели
        public static RoutedUICommand ShowTableDaysOfWeek { get; set; }

        // команда - демонстрация таблицы История фактов уборки
        public static RoutedUICommand ShowTableCleaningHistory { get; set; }

        // команда - демонстрация таблицы История поселений в гостиницу
        public static RoutedUICommand ShowTableHistoryRegistrationHotel { get; set; }

        // команда - демонстрация таблицы Города
        public static RoutedUICommand ShowTableCities { get; set; }

        // команда - демонстрация таблицы Номера гостиницы
        public static RoutedUICommand ShowTableHotelRooms { get; set; }

        // команда - демонстрация таблицы Типы номеров
        public static RoutedUICommand ShowTableTypesHotelRoom { get; set; }

        // команда - демонстрация таблицы Этажи
        public static RoutedUICommand ShowTableFloors { get; set; }

        // команда - демонстрация таблицы Служащие гостиницы
        public static RoutedUICommand ShowTableEmployees { get; set; }

        // команда - демонстрация таблицы Клиенты
        public static RoutedUICommand ShowTableClients { get; set; }

        // команда - демонстрация таблицы Персоны
        public static RoutedUICommand ShowTablePersons { get; set; }

        #endregion

        #region Конструкторы

        // статический конструктор
        static WindowCommands()
        {
            // установка значений

            Exit = new RoutedUICommand("Выход", "Exit", typeof(WindowCommands), new InputGestureCollection { new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt+F4") });


            #region Комнады для задания на 31.03.2022

            // команда - Клиенты проживающие в заданном номере
            Query1  = new RoutedUICommand("Клиенты проживающие в заданном номере", "Query1", typeof(WindowCommands));

            // команда - Клиенты прибывшие из заданного города
            Query2  = new RoutedUICommand("Клиенты прибывши из заданного города", "Query2", typeof(WindowCommands));

            // команда - Работник убиравший выбранный номер в заданную дату
            Query3  = new RoutedUICommand("Работник убиравший выбранный номер в заданную дату", "Query3", typeof(WindowCommands));

            // команда - Свободные места в гостинице
            Query4  = new RoutedUICommand("Свободные места в гостинице", "Query4", typeof(WindowCommands));

            #endregion


            #region Команды демонстрации таблиц

            // команда - демонстрация таблицы График уборки
            FillTables                          = new RoutedUICommand("Заполнение таблиц данными", "FillTables", typeof(WindowCommands));

            // команда - демонстрация таблицы График уборки
            ShowTableCleaningSchedule           = new RoutedUICommand("График уборки", "ShowTableCleaningSchedule", typeof(WindowCommands));

            // команда - демонстрация таблицы Дни недели
            ShowTableDaysOfWeek                 = new RoutedUICommand("Дни недели", "ShowTableDaysOfWeek", typeof(WindowCommands));

            // команда - демонстрация таблицы История фактов уборки
            ShowTableCleaningHistory            = new RoutedUICommand("История фактов уборки", "ShowTableCleaningHistory", typeof(WindowCommands));

            // команда - демонстрация таблицы История поселений в гостиницу
            ShowTableHistoryRegistrationHotel   = new RoutedUICommand("История поселений в гостиницу", "ShowTableHistoryRegistrationHotel", typeof(WindowCommands));

            // команда - демонстрация таблицы Города
            ShowTableCities                     = new RoutedUICommand("Города", "ShowTableCities", typeof(WindowCommands));

            // команда - демонстрация таблицы Номера гостиницы
            ShowTableHotelRooms                 = new RoutedUICommand("Номера гостиницы", "ShowTableHotelRooms", typeof(WindowCommands));

            // команда - демонстрация таблицы Типы номеров
            ShowTableTypesHotelRoom             = new RoutedUICommand("Типы номеров", "ShowTableTypesHotelRoom", typeof(WindowCommands));

            // команда - демонстрация таблицы Этажи
            ShowTableFloors                     = new RoutedUICommand("Этажи", "ShowTableFloors", typeof(WindowCommands));

            // команда - демонстрация таблицы Служащие гостиницы
            ShowTableEmployees                  = new RoutedUICommand("Служащие гостиницы", "ShowTableEmployees", typeof(WindowCommands));

            // команда - демонстрация таблицы Клиенты
            ShowTableClients                    = new RoutedUICommand("Клиенты", "ShowTableClients", typeof(WindowCommands));

            // команда - демонстрация таблицы Персоны
            ShowTablePersons                    = new RoutedUICommand("Персоны", "ShowTablePersons", typeof(WindowCommands));

            #endregion
        }

        #endregion
    }
}
