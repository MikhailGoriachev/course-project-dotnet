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
    public class EvictClientViewModel : ObservableObject
    {

        // запись регистрации
        public HistoryRegistrationHotel CurrentRegistration { get; set; }


        // запись регистрации
        public List<HistoryRegistrationHotel> RegistrationHotelList { get; private set; } = HotelController.GetCurrentHistoryRegistrationHotel();


        // список клиентов
        public List<Client> Clients { get; set; }


        // выбранные клиенты (при поиске)
        private List<Client> _selectedClientsList;
        public List<Client> SelectedClientsList
        {
            get => _selectedClientsList; 
            set { 
                _selectedClientsList = value; 
                OnPropertyChanged("SelectedClientsList"); 
            }
        }


        // выбранный клиент
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient; 
            set { 
                _selectedClient = value; 
                OnPropertyChanged("SelectedClient"); 
            }
        }


        // список номеров
        public List<HotelRoom> _rooms = HotelController.GetHotelRooms();
        public List<HotelRoom> Rooms { get => _rooms; set { _rooms = value; OnPropertyChanged("Rooms"); } }


        // список выбранных номеров по этажу
        private List<HotelRoom> _selectedRoomsList;
        public List<HotelRoom> SelectedRoomsList { get => _selectedRoomsList; set { _selectedRoomsList = value; OnPropertyChanged("SelectedRoomsList"); } }


        // выбранный номер
        private HotelRoom _selectedRoom;

        public HotelRoom SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }


        #region Конструкторы


        // конструктор инициализирующий
        public EvictClientViewModel()
        {
            // установка значений
            Clients = RegistrationHotelList.Select(r => r.Client).Distinct().ToList();
            SelectedClientsList = Clients.ToList();
            SelectedClient = Clients.FirstOrDefault();
            SelectionClientRooms.Execute(null);
        }


        #endregion


        #region Команды


        // выборка номеров гостиницы клиенту
        private RelayCommand _selectionClientRooms;

        public RelayCommand SelectionClientRooms => (_selectionClientRooms ?? (_selectionClientRooms = new RelayCommand((o) => {

            // записи регистрации клиента
            List<HistoryRegistrationHotel> histories = RegistrationHotelList.Where(r => r.Client.Id == SelectedClient.Id).ToList();

            // выборка номеров
            SelectedRoomsList = Rooms;

            // выборка номеров
            List<HotelRoom> rooms = histories.Select(r => r.HotelRoom).Distinct().ToList();

            // установка первой записи в модель
            SelectedRoom = rooms[0];

            // установка выборки номеров
            SelectedRoomsList = rooms;

        })));


        // выборка клиентов гостиницы по номеру
        private RelayCommand _selectionRoom;

        public RelayCommand SelectionRoom => (_selectionRoom ?? (_selectionRoom = new RelayCommand((o) => {

            // выборка клиентов гостиницы по номеру
            //FloorRooms = Rooms.Where(r => r.Floor.Id == CurrentFloor.Id).ToList();
            List<Client> clients = RegistrationHotelList.Where(r => r.HotelRoom.Id == SelectedRoom.Id)
                                                        .Select(r => r.Client)
                                                        .Distinct()
                                                        .ToList();

            // установка первой записи
            SelectedClient = clients[0];

            // установка клиентов
            SelectedClientsList = clients;
        })));


        // поиск клиента
        private RelayCommand _searchClients;

        public RelayCommand SearchClients => _searchClients ?? (_searchClients = new RelayCommand((o) =>
        {
            // токен для поиска
            string token = o as string;

            // если строка пуста
            if (String.IsNullOrWhiteSpace(token))
            {
                SelectedClientsList = Clients.ToList();

                return;
            }

            // поиск по токену среди полей
            List<Client> result = Clients.Where(c => c.Person.Name.StartsWith(token)
                                                || c.Person.Surname.StartsWith(token)
                                                || c.Person.Patronymic.StartsWith(token)
                                                || c.Passport.StartsWith(token))
                                         .ToList();

            if (result.Count == 0)
                return;

            // если список не пуст, то установить первую запись в модель
            SelectedClient = result.FirstOrDefault();

            // присваивание списка результата поиска
            SelectedClientsList = result;

        }));


        // сохранение регистрации в базе данных
        private RelayCommand _saveRegistration;

        public RelayCommand SaveRegistration => _saveRegistration ?? (_saveRegistration = new RelayCommand((o) =>
        {
            // выбранная запись
            CurrentRegistration = HotelController.GetHistoryRegistrationHotel().FirstOrDefault(r => r.Client.Id == SelectedClient.Id && r.HotelRoom.Id == SelectedRoom.Id);

            // установка длительности
            CurrentRegistration.Duration -= new DateTime(DateTime.Now.Date.Ticks - CurrentRegistration.RegistrationDate.Date.Ticks).DayOfYear;

            HotelController.SaveChanges();
        }));


        #endregion

    }
}
