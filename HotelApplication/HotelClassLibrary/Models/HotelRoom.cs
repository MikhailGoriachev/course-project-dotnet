using HotelApplication.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Номер отеля
    public class HotelRoom : ObservableObject
    {
        // первичный ключ
        private int _id;

        public int Id
        {
            get => _id;
            set { 
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        // тип номера
        private TypeHotelRoom _typeHotelRoom;

        [Required]
        public TypeHotelRoom TypeHotelRoom
        {
            get => _typeHotelRoom;
            set { 
                _typeHotelRoom = value;
                OnPropertyChanged("TypeHotelRoom");
            }
        }


        // этаж
        private Floor _floor;

        [Required]
        public Floor Floor
        {
            get => _floor;
            set { 
                _floor = value;
                OnPropertyChanged("Floor");
            }
        }


        // номер гостиничного номера
        private int _number;

        [Required]
        public int Number
        {
            get => _number;
            set { 
                _number = value;
                OnPropertyChanged("Number");
            }
        }


        // номер телефона
        private string _phoneNumber;

        [Required]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { 
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
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


        // строковое представление
        public override string ToString()
        {
            return _number.ToString();
        }
    }
}
