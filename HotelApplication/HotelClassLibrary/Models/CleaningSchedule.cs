using HotelApplication.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Запись графика уборки
    public class CleaningSchedule : ObservableObject
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


        // день недели
        private DayOfWeek _dayOfWeek;

        [Required]
        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set { 
                _dayOfWeek = value;
                OnPropertyChanged("DayOfWeek");
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
