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
    public class Person
    {
        // первичный ключ
        public int Id { get; set; }


        // фамилия
        // [Required, Column(TypeName = "nvarchar(60)")]
        [Required]
        public string Surname { get; set; }


        // имя
        // [Required, Column(TypeName = "nvarchar(40)")]
        [Required]
        public string Name { get; set; }


        // отчество
        // [Required, Column(TypeName = "nvarchar(60)")]
        [Required]
        public string Patronymic { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }


    }
}
