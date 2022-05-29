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
    // Информация об уборке номера по дате и клиенту
    public class CleaningRoomClientDateViewModel : ObservableObject
    {
        // список клиентов
        public BindingList<Client> Clients { get; set; }


        // список регистраций в гостинице
        public List<HistoryRegistrationHotel> Registrations { get; set; }


        // список истории уборки в гостинице
        public List<CleaningHistory> Cleanings { get; set; }


        // список выбранных записей истории уборки в гостинице
        private List<CleaningHistory> _selectedCleanings;
        public List<CleaningHistory> SelectedCleanings { get => _selectedCleanings; set { _selectedCleanings = value; OnPropertyChanged("SelectedCleanings"); } }


        // выбранные клиенты (при поиске)
        private List<Client> _selectedClientsList;
        public List<Client> SelectedClientsList { get => _selectedClientsList; set { _selectedClientsList = value; OnPropertyChanged("SelectedClientsList"); } }


        // выбранный клиент
        public Client SelectedClient { get; set; }


        // дата борки
        public DateTime CleaningDate { get; set; }


        #region Конструкторы


        // конструктор инициализирующий
        public CleaningRoomClientDateViewModel()
        {
            // установка значений
            Clients = HotelController.GetClientsBindingList();
            SelectedClientsList = Clients.ToList();
            Registrations = HotelController.GetHistoryRegistrationHotel();
            Cleanings = HotelController.GetCleaningHistory();
        }


        #endregion


        #region Команды


        // поиск клиента
        private RelayCommand _searchClientsCommand;

        public RelayCommand SearchClientsCommand => _searchClientsCommand ?? (_searchClientsCommand = new RelayCommand((o) =>
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


        // поиск служащего, который убирал номер клиента
        private RelayCommand _getResultCommand;

        public RelayCommand GetResultCommand => _getResultCommand ?? (_getResultCommand = new RelayCommand((o) => {

            // текущая дата 
            DateTime now = DateTime.Now.Date;

            List<HistoryRegistrationHotel> res = Registrations.Where(r => r.Duration != 0 && now >= r.RegistrationDate.Date && now < r.RegistrationDate.Date.AddDays(r.Duration)
                                                                             && r.Client.Id == SelectedClient.Id).ToList();

            // выборка этажей на которых находятся номера клиента по дате проживания
            List<Floor> floors = res
                                                 .Select(r => r.HotelRoom.Floor)
                                                 .Distinct()
                                                 .ToList();

            // поиск записей уборки за заданную дату
            SelectedCleanings = Cleanings.Where(c => c.DateCleaning.Date == CleaningDate.Date && floors.Contains(c.Floor)).ToList();

        },
            (o) => SelectedClient != null));


        #endregion

    }
}
