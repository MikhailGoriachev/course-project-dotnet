using HotelClassLibrary.Controllers;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления окна "Получение счёта за проживание"
    public class CostRegistrationViewModel : ObservableObject
    {

        // записи регистрации
        public List<HistoryRegistrationHotel> RegistrationHotelList { get; private set; } = HotelController.GetHistoryRegistrationHotel();


        // выбранные записи регистрации по клиенту
        private List<HistoryRegistrationHotel> _selectedRegistrationList;

        public List<HistoryRegistrationHotel> SelectedRegistrationList
        {
            get => _selectedRegistrationList;
            set
            {
                _selectedRegistrationList = value;
                OnPropertyChanged("SelectedRegistrationList");
            }
        }




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


        #region Конструкторы


        // конструктор инициализирующий
        public CostRegistrationViewModel()
        {
            // установка значений
            Clients = RegistrationHotelList.Select(r => r.Client).Distinct().ToList();
            SelectedClientsList = Clients.ToList();
            SelectedClient = Clients.FirstOrDefault();
            SelectionClientRegistrationCommand.Execute(null);
        }


        #endregion


        #region Команды


        // выборка записей гостиницы по клиенту
        private RelayCommand _selectionClientRegistrationCommand;

        public RelayCommand SelectionClientRegistrationCommand => (_selectionClientRegistrationCommand ?? (_selectionClientRegistrationCommand = new RelayCommand((o) => {

            // записи регистрации клиента
            SelectedRegistrationList = RegistrationHotelList.Where(r => r.Client.Id == SelectedClient.Id).ToList();
        })));


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


        #endregion

    }
}
