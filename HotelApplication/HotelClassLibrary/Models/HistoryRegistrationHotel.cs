using HotelApplication.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Запись истории поселений в гостиницу
    public class HistoryRegistrationHotel : ObservableObject
    {
        // первичный ключ
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        // клиент
        private Client _client;

        [Required]
        public Client Client
        {
            get => _client;
            set { 
                _client = value;
                OnPropertyChanged("Client");
            }
        }


        // гостиничный номер
        private HotelRoom hotelRoom;

        [Required]
        public HotelRoom HotelRoom
        {
            get => hotelRoom;
            set { 
                hotelRoom = value;
                OnPropertyChanged("HotelRoom");
            }
        }


        // город, из которого прибыл клиент
        private City _city;

        [Required]
        public City City
        {
            get => _city;
            set { 
                _city = value;
                OnPropertyChanged("City");
            }
        }


        // дата поселения
        // [Required, Column(TypeName = "date")]
        private DateTime _registrationDate;

        [Required]
        public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = value; }
        }


        // длительность проживания
        private int _duration;

        [Required]
        public int Duration
        {
            get => _duration;
            set { 
                _duration = value; 
                OnPropertyChanged("Duration");
            }
        }


        // статус удаления
        private bool _isDeleted;

        [Required]
        public bool IsDeleted
        {
            get => _isDeleted;
            set { 
                _isDeleted = value;
                OnPropertyChanged("IsDeleted");
            }
        }

    }
}
