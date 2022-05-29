using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelClassLibrary.Models
{
    // Класс Номер отеля для отёта
    public class HotelRoomReport
    {
        // номер
        public HotelRoom Room { get; set; }

        // количество свободных дней
        public int EmptyDays { get; set; }

        // количество занятых дней
        public int BusyDays { get; set; }
    }
}
