using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelClassLibrary.Controllers;        // контроллер
using HotelClassLibrary.Models;             // модели
using HotelApplication.Core;                // базовые классы
using HotelApplication.MVVM.View;


namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления окна "Регистрация клиентов"
    public class RegistrationHotelManagementViewModel : ObservableObject
    {
        // количество записей
        public int CountRegistration => RegistrationHotel.Count;

        // коллекция записей
        private BindingList<HistoryRegistrationHotel> _registrationHoel = HotelController.GetHistoryRegistrationHotelBindingList();
        public BindingList<HistoryRegistrationHotel> RegistrationHotel => _registrationHoel;


        // выбранная запись в таблице
        public HistoryRegistrationHotel SelectedRegistration { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public RegistrationHotelManagementViewModel()
        {
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

        #endregion

    }
}
