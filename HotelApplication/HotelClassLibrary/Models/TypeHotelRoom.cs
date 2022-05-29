using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelClassLibrary.Models
{
    // Класс Тип гостинечного номера
    public class TypeHotelRoom : ObservableObject
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


        // название типа
        // [Required, Column(TypeName = "nvarchar(40)")]
        private string _name;

        [Required]
        public string Name
        {
            get => _name;
            set { 
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        // количество мест
        private int _countPlace;

        [Required]
        public int CountPlace
        {
            get => _countPlace;
            set { 
                _countPlace = value;
                OnPropertyChanged("CountPlace");
            }
        }


        // стоимость в сутки
        private int _price;

        [Required]
        public int Price
        {
            get => _price;
            set { 
                _price = value;
                OnPropertyChanged("Price");
            }
        }


        // статус удаления
        private bool _isDeleted;

        [Required]
        public bool isDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                OnPropertyChanged("IsDeleted");
            }
        }

    }
}
