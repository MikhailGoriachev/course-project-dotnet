using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelClassLibrary.Models
{
    // Класс Клиент
    public class Client : ObservableObject
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


        // персона
        private Person _person;

        [Required]
        public Person Person
        {
            get => _person;
            set {
                _person = value;
                OnPropertyChanged("Person");
            }
        }


        // номер паспорта
        //[Required, Column(TypeName = "nvarchar(10)")]
        private string _passport;

        [Required]
        public string Passport
        {
            get => _passport;
            set { 
                _passport = value;
                OnPropertyChanged("Passport");
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


        // строковое представление объекта
        public override string ToString()
        {
            return $"{_person.Surname} {_person.Name} {_person.Patronymic} {_passport}";
        }
    }
}
