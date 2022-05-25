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
    // Класс День недели
    public class DayOfWeek : ObservableObject
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


        // название дня недели
        // [Required, Column(TypeName = "nvarchar(20)")]
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


        // номер дня недели
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

    }
}
