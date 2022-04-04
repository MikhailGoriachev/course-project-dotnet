using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Клсс Этаж
    public class Floor
    {
        // первичный ключ
        public int Id { get; set; }


        // номер этажа
        [Required]
        public int Number { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }
    }
}
