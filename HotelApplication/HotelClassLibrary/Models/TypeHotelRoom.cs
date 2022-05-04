using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Тип гостинечного номера
    public class TypeHotelRoom
    {
        // первичный ключ
        public int Id { get; set; }


        // название типа
        // [Required, Column(TypeName = "nvarchar(40)")]
        [Required]
        public string Name { get; set; }


        // количество мест
        [Required]
        public int CountPlace { get; set; }


        // стоимость в сутки
        [Required]
        public int Price { get; set; }


    }
}
