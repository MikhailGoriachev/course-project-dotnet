using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelClassLibrary.Controllers;        // контроллер
using HotelClassLibrary.Models;             // модели
using HotelApplication.MVVM.View;


namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления окна "Регистрация клиентов"
    public class RegistrationHotelManagementViewModel : ObservableObject
    {
        // количество записей
        public int CountRegistration => SelectedRegistrationList.Count;

        // коллекция записей
        private BindingList<HistoryRegistrationHotel> _registrationHotel { get; set; } = HotelController.GetHistoryRegistrationHotelBindingList();
        public BindingList<HistoryRegistrationHotel> RegistrationHotel
        {
            get => _registrationHotel; 
            set {
                _registrationHotel = value; 
                OnPropertyChanged("RegistrationHotel");
            }
        }

        // коллекция текущих записей
        public List<HistoryRegistrationHotel> _selectedRegistrationList;

        public List<HistoryRegistrationHotel> SelectedRegistrationList
        {
            get => _selectedRegistrationList;
            set
            {
                _selectedRegistrationList = value;
                OnPropertyChanged("SelectedRegistrationList");
            }
        }



        // выбранная запись в таблице
        public HistoryRegistrationHotel SelectedRegistration { get; set; }


        // список городов
        public BindingList<City> Cities { get; set; } = HotelController.GetCitiesBindingList();


        // выбранный город
        private City _selectedCity;
        public City SelectedCity { get => _selectedCity; set { _selectedCity = value; OnPropertyChanged("SelectedCity"); } }

        // активность выборки записей по городу
        public bool IsSelectionByCity { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public RegistrationHotelManagementViewModel()
        {
            // установка значений
            SelectedRegistrationList = RegistrationHotel.ToList();

            // подписка на события добавления и удаления работника
            RegistrationHotel.ListChanged += (sender, e) => { OnPropertyChanged("CountRegistration"); };
        }

        #endregion


        #region Команды

        // добавление записи
        private RelayCommand _addRegistrationCommand;

        public RelayCommand AddRegistrationCommand => _addRegistrationCommand ?? (_addRegistrationCommand = new RelayCommand((o) =>
        {
            // запуск окна
            new RegistrationHotelWindow().ShowDialog();
        }));


        // изменение выбранной записи
        private RelayCommand _editRegistrationCommand;

        public RelayCommand EditRegistrationCommand => _editRegistrationCommand ?? (_editRegistrationCommand = new RelayCommand((o) =>
        {
            // запуск окна
            new RegistrationHotelWindow(SelectedRegistration).ShowDialog();

        },
            (o) => SelectedRegistration != null));


        // удаление выбранной записи
        private RelayCommand _removeRegistrationCommand;

        public RelayCommand RemoveRegistrationCommand => _removeRegistrationCommand ?? (_removeRegistrationCommand = new RelayCommand((o) =>
        {
            // пометить запись, как удалённую
            SelectedRegistration.IsDeleted = true;

            // сохранение данных
            HotelController.SaveChanges();
        },
            (o) => SelectedRegistration != null));


        // выселить клиента
        private RelayCommand _evictRegistrationCommand;

        public RelayCommand EvictRegistrationCommand => _evictRegistrationCommand ?? (_evictRegistrationCommand = new RelayCommand((o) =>
        {
            // запуск окна
            new EvictClientView().ShowDialog();
        }));


        // выбрать записи по городу
        private RelayCommand _selectionByCityCommand;

        public RelayCommand SelectionByCityCommand => _selectionByCityCommand ?? (_selectionByCityCommand = new RelayCommand((o) =>
        {
            // если выборка записей по городу не активна или город не выбран
            if (!IsSelectionByCity || SelectedCity == null)
            {
                SelectedRegistrationList = RegistrationHotel.ToList();
                return;
            }

            // выборка по установленному городу
            SelectedRegistrationList = RegistrationHotel.Where(r => r.City.Id == SelectedCity.Id).ToList();
        }));


        // получить счёт за проживание
        private RelayCommand _costRegistrationCommand;

        public RelayCommand CostRegistrationCommand => _costRegistrationCommand ?? (_costRegistrationCommand = new RelayCommand((o) =>
        {
            // вызов окна для вычисления счёта за проживание
            new CostRegistrationView().ShowDialog();
        },
             (o) => SelectedRegistration != null));

        #endregion

    }
}
