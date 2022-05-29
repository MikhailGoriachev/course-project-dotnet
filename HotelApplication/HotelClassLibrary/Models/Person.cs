using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelClassLibrary.Models
{
    // Класс Персона
    public class Person : ObservableObject
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


        // фамилия
        // [Required, Column(TypeName = "nvarchar(60)")]
        private string _surname;

        [Required]
        public string Surname
        {
            get => _surname;
            set { 
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }


        // имя
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


        // отчество
        // [Required, Column(TypeName = "nvarchar(60)")]
        private string _patronymic;

        [Required]
        public string Patronymic
        {
            get => _patronymic;
            set { 
                _patronymic = value;
                OnPropertyChanged("Patronymic");
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
