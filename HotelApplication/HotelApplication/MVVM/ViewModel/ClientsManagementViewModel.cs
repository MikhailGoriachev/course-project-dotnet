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
    // Класс Модель представления окна "Клиенты"
    public class ClientsManagementViewModel : ObservableObject
    {
        // количество Клиентов
        public int CountClients => Clients.Count;

        // коллекция клиентов
        private BindingList<Client> _clients = HotelController.GetClientsBindingList();
        public BindingList<Client> Clients => _clients;


        // выбранный клиент в таблице
        public Client SelectedClient { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public ClientsManagementViewModel()
        {
            // подписка на события добавления и удаления работника
            Clients.ListChanged += (sender, e) => { OnPropertyChanged("CountClients"); };
        }

        #endregion


        #region Команды

        // добавление клиента
        private RelayCommand _addClientCommand;

        public RelayCommand AddClientCommand => _addClientCommand ?? (_addClientCommand = new RelayCommand((o) =>
        {
            // окно
            ClientWindow window = new ClientWindow();

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // добавление записи
            HotelController.AddClient(window.CurrentClient);

            // сохранение добавленной записи
            HotelController.SaveChanges();
        }));

        // изменение выбранного клиента
        private RelayCommand _editClientCommand;

        public RelayCommand EditClientCommand => _editClientCommand ?? (_editClientCommand = new RelayCommand((o) =>
        {
            // окно
            ClientWindow window = new ClientWindow(SelectedClient);

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // сохранение изменений записи
            HotelController.SaveChanges();
        },
            (o) => SelectedClient != null));

        // удаление выбранного клиента
        private RelayCommand _removeClientCommand;

        public RelayCommand RemoveClientCommand => _removeClientCommand ?? (_removeClientCommand = new RelayCommand((o) =>
        {
            // удаление клиента
            HotelController.RemoveClient(SelectedClient);
        },
            (o) => SelectedClient != null));

        #endregion

    }
}
