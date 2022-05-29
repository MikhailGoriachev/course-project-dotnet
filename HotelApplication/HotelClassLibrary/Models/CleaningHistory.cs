using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelClassLibrary.Models
{
    // Класс Запись история фактов уборки
    public class CleaningHistory : ObservableObject
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


        // дата уборки
        //[Required, Column(TypeName = "date")]
        private DateTime _dateCleaning;

        [Required]
        public DateTime DateCleaning
        {
            get => _dateCleaning;
            set { 
                _dateCleaning = value; 
                OnPropertyChanged("DateCleaning");
            }
        }


        // служащий гостиницы
        private Employee _employee;

        [Required]
        public Employee Employee
        {
            get => _employee;
            set { 
                _employee = value;
                OnPropertyChanged("Employee");
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
