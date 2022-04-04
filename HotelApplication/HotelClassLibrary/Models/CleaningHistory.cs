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
    public class CleaningHistory
    {
        // первичный ключ
        public int Id { get; set; }


        // этаж
        [Required]
        public Floor Floor { get; set; }


        // дата уборки
        //[Required, Column(TypeName = "date")]
        [Required]
        public DateTime DateCleaning { get; set; }


        // служащий гостиницы
        [Required]
        public Employee Employee { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }

    }
}
