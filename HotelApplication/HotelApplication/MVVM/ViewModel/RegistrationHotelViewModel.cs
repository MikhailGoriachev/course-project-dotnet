using HotelApplication.Models;
using HotelApplication.MVVM.View;
using HotelClassLibrary.Controllers;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления окна "Добавить/Изменить регистрацию"
    public class RegistrationHotelViewModel : ObservableObject
    {
        // статус запуска окна
        public WindowState CurrentWindowState { get; set; }


        // запись регистрации
        public HistoryRegistrationHotel CurrentRegistration { get; set; }


        // исходная запись
        public HistoryRegistrationHotel SourceRegistration { get; set; }


        // список клиентов
        public BindingList<Client> Clients => HotelController.GetClientsBindingList();


        // выбранные клиенты (при поиске)
        private List<Client> _currentClientsList;
        public List<Client> CurrentClientsList { get => _currentClientsList; set { _currentClientsList = value; OnPropertyChanged("CurrentClientsList"); } }


        // список городов
        public BindingList<City> Cities => HotelController.GetCitiesBindingList();


        // список номеров
        public List<HotelRoom> Rooms { get; set; } 


        // список этажей
        public BindingList<Floor> Floors => HotelController.GetFloorsBindingList();


        // список выбранных номеров по этажу
        private List<HotelRoom> _floorRooms;
        public List<HotelRoom> FloorRooms { get => _floorRooms; set { _floorRooms = value; OnPropertyChanged("FloorRooms"); } }


        // выбранный этаж
        public Floor CurrentFloor { get; set; }


        // продолжительность проживания
        public int Duration { get; set; }


        // дата регистрации
        public DateTime RegistrationDate { get; set; }

        #region Конструкторы


        // конструктор инициализирующий
        public RegistrationHotelViewModel()
        {
            // установка значений
            CurrentRegistration = new HistoryRegistrationHotel { 
                RegistrationDate = DateTime.Now
            };

            // установка состояния запуска окна
            CurrentWindowState = WindowState.CreateState;

            // клиенты
            CurrentClientsList = Clients.ToList();

            // заполнение списка номеров номерами, которые не заполнены до конца
            Rooms = HotelController.GetHotelRoomsBindingList().Where(r => HotelController.CountBusyPlace(r, DateTime.Now.Date) < r.TypeHotelRoom.CountPlace).ToList();
        }


        // конструктор инициализирующий
        public RegistrationHotelViewModel(HistoryRegistrationHotel registration)
        {
            // установка значений
            SourceRegistration = registration;

            // клиенты
            CurrentClientsList = Clients.ToList();

            CurrentRegistration = new HistoryRegistrationHotel
            {
                City                = registration.City,
                Client              = registration.Client,
                HotelRoom           = registration.HotelRoom,
                Duration            = registration.Duration,
                RegistrationDate    = registration.RegistrationDate,
            };

            CurrentFloor = CurrentRegistration.HotelRoom.Floor;

            // заполнение списка номеров номерами, которые не заполнены до конца, также добавляется запись текущего клиента
            Rooms = HotelController.GetHotelRoomsBindingList().Where(r => CurrentRegistration.HotelRoom.Id == r.Id || HotelController.CountBusyPlace(r, DateTime.Now.Date) < r.TypeHotelRoom.CountPlace).ToList();

            // выборка номеров гостиницы по этажу
            FloorRooms = Rooms.Where(r => r.Floor.Id == CurrentRegistration.HotelRoom.Floor.Id).ToList();

            // установка состояния запуска окна
            CurrentWindowState = WindowState.EditState;
        }


        #endregion


        #region Команды

        // выборка номеров гостиницы по этажу
        private RelayCommand _selectionFloorRooms;

        public RelayCommand SelectionFloorRooms => (_selectionFloorRooms ?? (_selectionFloorRooms = new RelayCommand((o) => {

            // выборка номеров гостиницы по этажу
            //FloorRooms = Rooms.Where(r => r.Floor.Id == CurrentFloor.Id).ToList();
            List<HotelRoom> rooms = Rooms.Where(r => r.Floor.Id == CurrentFloor.Id).ToList();

            // установка первой записи в модель
            CurrentRegistration.HotelRoom = rooms[0];

            // установка комнат этажа
            FloorRooms = rooms;
        })));



        // добавление клиента
        private RelayCommand _addClient;

        public RelayCommand AddClient => _addClient ?? (_addClient = new RelayCommand((o) => {

            // добавление клиента
            ClientWindow window = new ClientWindow();

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // добавление клиента
            HotelController.AddClient(window.CurrentClient);

        }));


        // поиск клиента
        private RelayCommand _searchClients;

        public RelayCommand SearchClients => _searchClients ?? (_searchClients = new RelayCommand((o) =>
        {
            // токен для поиска
            string token = o as string;

            // если строка пуста
            if (String.IsNullOrWhiteSpace(token))
            {
                CurrentClientsList = Clients.ToList();

                return;
            }

            // поиск по токену среди полей
            List<Client> result = Clients.Where(c => c.Person.Name.StartsWith(token)
                                                || c.Person.Surname.StartsWith(token)
                                                || c.Person.Patronymic.StartsWith(token)
                                                || c.Passport.StartsWith(token))
                                         .ToList();

            // присваивание списка результата поиска
            CurrentClientsList = result.Count == 0 ? CurrentClientsList : result;

            // если список не пуст, то установить первую запись в модель
            CurrentRegistration.Client = CurrentClientsList.First();
        }));


        // поиск / добавление города


        // добавление города
        private RelayCommand _addCity;

        public RelayCommand AddCity => _addCity ?? (_addCity = new RelayCommand((o) =>
        {
            // окно
            CityWindow window = new CityWindow();

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // добавление записи
            HotelController.AddCity(window.CurrentCity);

        }));


        // сохранение регистрации в базе данных
        private RelayCommand _saveRegistration;

        public RelayCommand SaveRegistration => _saveRegistration ?? (_saveRegistration = new RelayCommand((o) =>
        {
            // установка значений
            if (SourceRegistration != null)
            {
                SourceRegistration.City             = CurrentRegistration.City;
                SourceRegistration.Client           = CurrentRegistration.Client;
                SourceRegistration.HotelRoom        = CurrentRegistration.HotelRoom;
                SourceRegistration.Duration         = CurrentRegistration.Duration;
                SourceRegistration.RegistrationDate = CurrentRegistration.RegistrationDate;
            }

            switch (CurrentWindowState) {

                // режим создания
                case WindowState.CreateState:
                    // разместить клиента
                    HotelController.PlaceClient(CurrentRegistration);
                    break;

                // режим редактирования
                case WindowState.EditState:
                    HotelController.SaveChanges();
                    break;
            }
            
        }));


        #endregion

    }
}
