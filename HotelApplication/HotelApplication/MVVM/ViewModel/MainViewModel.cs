using HotelApplication.Core;
using HotelApplication.MVVM.View;
using HotelClassLibrary.Context;
using HotelClassLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления главного окна
    public class MainViewModel : ObservableObject
    {
        // модель домашней страницы
        public HomeViewModel HomeVM { get; set; }


        // модель страницы "Служащие гостиницы"
        public EmployeesManagementViewModel EmployeesVM { get; set; }

        // модель страницы "Клиенты"
        public ClientsManagementViewModel ClientsVM { get; set; }


        // модель страницы "Регистрация клиентов"
        public RegistrationHotelManagementViewModel RegistrationHotelVM { get; set; }


        // модель страницы "Номера"
        public RoomsManagementViewModel RoomsHotelVM { get; set; }


        // текущее представление контента
        private object _currentView;

        public object CurrentView { 
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        // окно для работы
        public Window CurrentWindow { get; set; }

        #region Конструкторы

        // конструктор по умолчанию
        public MainViewModel() { }

        // конструктор инициализирующий
        public MainViewModel(Window window)
        {
            // установка значений
            HomeVM = new HomeViewModel();
            EmployeesVM = new EmployeesManagementViewModel();
            ClientsVM = new ClientsManagementViewModel();
            RegistrationHotelVM = new RegistrationHotelManagementViewModel();
            RoomsHotelVM = new RoomsManagementViewModel();

            // установка текущего окна и страницы
            CurrentView = HomeVM;
            CurrentWindow = window;
        }

        #endregion

        #region Команды


        #region Переключение контента

        // главная
        private RelayCommand _homeViewCommand;

        public RelayCommand HomeViewCommand => _homeViewCommand ?? (_homeViewCommand = new RelayCommand(o => CurrentView = HomeVM));


        // работники
        private RelayCommand _emloyeesViewCommand;

        public RelayCommand EmloyeesViewCommand => _emloyeesViewCommand ?? (_emloyeesViewCommand = new RelayCommand(o => CurrentView = EmployeesVM));


        // клиенты
        private RelayCommand _clientsViewCommand;

        public RelayCommand ClientsViewCommand => _clientsViewCommand ?? (_emloyeesViewCommand = new RelayCommand(o => CurrentView = ClientsVM));


        // регистрация клиентов
        private RelayCommand _registrationHotelViewCommand;

        public RelayCommand RegistrationHotelViewCommand => _registrationHotelViewCommand ?? (_registrationHotelViewCommand = new RelayCommand(o => CurrentView = RegistrationHotelVM));


        // номера
        private RelayCommand _roomsHotelViewCommand;

        public RelayCommand RoomsHotelViewCommand => _roomsHotelViewCommand ?? (_roomsHotelViewCommand = new RelayCommand(o => CurrentView = RoomsHotelVM));

        #endregion


        // выход из приложения
        private RelayCommand _exitCommand;

        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(p => Application.Current.Shutdown()));

        // развернуть на весь экран
        private RelayCommand _fullScreenCommand;

        public RelayCommand FullScreenCommand => _fullScreenCommand ?? (_fullScreenCommand = new RelayCommand(p => CurrentWindow.WindowState = CurrentWindow.WindowState == WindowState.Maximized
                                                                                    ? WindowState.Normal
                                                                                    : WindowState.Maximized));

        // свернуть приложение
        private RelayCommand _collapseCommand;

        public RelayCommand CollapseCommand => _collapseCommand ?? (_collapseCommand = new RelayCommand(p => CurrentWindow.WindowState = WindowState.Minimized));

        #endregion
    }
}
