using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Номер отеля
    public class HotelRoom
    {
        // первичный ключ
        public int Id { get; set; }


        // тип номера
        [Required]
        public TypeHotelRoom TypeHotelRoom { get; set; }


        // этаж
        [Required]
        public Floor Floor { get; set; }


        // номер гостиничного номера
        [Required]
        public int Number { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }

    }
}
