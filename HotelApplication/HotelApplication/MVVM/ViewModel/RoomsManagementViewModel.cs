using HotelApplication.Core;
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
    public class RoomsManagementViewModel : ObservableObject
    {
        // количество записей
        public int CountRooms => Rooms.Count;

        // коллекция записей
        private BindingList<HotelRoom> _rooms = HotelController.GetHotelRoomsBindingList();
        public BindingList<HotelRoom> Rooms => _rooms;


        // выбранная запись в таблице
        public HotelRoom SelectedRoom { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public RoomsManagementViewModel()
        {

        }

        #endregion


        #region Команды

        // добавление клента в номер
        private RelayCommand _addClientRoomCommand;

        public RelayCommand AddClientRoomCommand => _addClientRoomCommand ?? (_addClientRoomCommand = new RelayCommand((o) =>
        {
            // запуск окна управления номером
            //new RoomView(SelectedRoom, WindowState.AddState).ShowDialog();

            new RegistrationHotelWindow().ShowDialog();

            // для того, чтоб сработало событие обновления данных
            SelectedRoom.Id = SelectedRoom.Id;
        }));


        // выселить клиента из номера
        private RelayCommand _evictClientRoomCommand;

        public RelayCommand EvictClientRoomCommand => _evictClientRoomCommand ?? (_evictClientRoomCommand = new RelayCommand((o) =>
        {
            // запуск окна управления номером
            new RoomView(SelectedRoom, WindowState.RemoveState).ShowDialog();

            // для того, чтоб сработало событие обновления данных
            SelectedRoom.Id = SelectedRoom.Id;
        },
            (o) => SelectedRoom != null && HotelController.CountBusyPlace(SelectedRoom, DateTime.Now) != 0));


        // информация об уборке по дате и клиенту
        private RelayCommand _cleaningRoomInfoCommand;

        public RelayCommand CleaningRoomInfoCommand => _cleaningRoomInfoCommand ?? (_cleaningRoomInfoCommand = new RelayCommand((o) =>
        {
            // запуск окна управления номером
            new CleaningRoomClientDateView().ShowDialog();
        }));

        #endregion

    }
}
