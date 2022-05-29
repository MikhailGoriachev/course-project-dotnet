using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelClassLibrary.Models
{
    // Клсс Этаж
    public class Floor : ObservableObject
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


        // номер этажа
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
