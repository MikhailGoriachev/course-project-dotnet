using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelApplication.Core;

namespace HotelClassLibrary.Models
{
    // Класс Город
    public class City : ObservableObject
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


        // наименование города
        // [Required, Column(TypeName = "nvarchar(60)")]
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
            return _name;
        }
    }
}
