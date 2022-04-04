using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Запись графика уборки
    public class CleaningSchedule
    {
        // первичный ключ
        public int Id { get; set; }


        // день недели
        [Required]
        public DayOfWeek DayOfWeek { get; set; }


        // служащий гостиницы
        [Required]
        public Employee Employee { get; set; }


        // этаж
        [Required]
        public Floor Floor { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }


    }
}
