using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Служащий гостиницы
    public class Employee
    {
        // первичный ключ
        public int Id { get; set; }


        // персона
        [Required]
        public Person Person { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }


    }
}
