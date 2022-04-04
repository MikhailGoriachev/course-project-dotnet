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
    public class DayOfWeek
    {
        // первичный ключ
        [Required]
        public int Id { get; set; }


        // название дня недели
        // [Required, Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set; }


        // номер дня недели
        [Required]
        public int Number { get; set; }

    }
}
