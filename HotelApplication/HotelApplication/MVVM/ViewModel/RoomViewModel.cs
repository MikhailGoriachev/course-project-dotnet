using HotelApplication.Core;
using HotelApplication.Models;
using HotelClassLibrary.Controllers;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.MVVM.ViewModel
{
    public class RoomViewModel : ObservableObject
    {
        // заголовок окна
        public string Header { get; set; }


        // запись регистрации
        public HistoryRegistrationHotel CurrentRegistration { get; set; }


        // список записей регистрации
        public List<HistoryRegistrationHotel> RegistrationHotelList { get; private set; } = HotelController.GetCurrentHistoryRegistrationHotel();


        // список клиентов
        public List<Client> Clients { get; set; }


        // выбранные клиенты (при поиске)
        private List<Client> _selectedClientsList;
        public List<Client> SelectedClientsList
        {
            get => _selectedClientsList;
            set
            {
                _selectedClientsList = value;
                OnPropertyChanged("SelectedClientsList");
            }
        }


        // выбранный клиент
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }


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


        // конструктор по умолчанию
        public RoomViewModel() { }


        // конструктор инициализирующий
        public RoomViewModel(HotelRoom room, WindowState state)
        {
            // установка значений
            SelectedRoom = room;
            Clients = RegistrationHotelList.Where(r => r.HotelRoom.Id == SelectedRoom.Id).Select(r => r.Client).Distinct().ToList();

            SelectedClientsList = Clients.ToList();
            SelectedClient = Clients.FirstOrDefault();

            Header = $"Выселение клиента. Номер: {SelectedRoom.Number}";
        }


        #endregion


        #region Команды


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

            DateTime now = DateTime.Now.Date.AddDays(1);

            int day = new DateTime(DateTime.Now.Date.Ticks - CurrentRegistration.RegistrationDate.Date.Ticks).DayOfYear - 1;

            // установка длительности
            CurrentRegistration.Duration -= day > 0 ? day : CurrentRegistration.Duration;

            HotelController.SaveChanges();
        }));


        #endregion

    }
}
