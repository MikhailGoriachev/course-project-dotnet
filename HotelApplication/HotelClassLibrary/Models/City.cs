using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelClassLibrary.Models
{
    // Класс Город
    public class City
    {
        // первичный ключ
        public int Id { get; set; }


        // наименование города
        // [Required, Column(TypeName = "nvarchar(60)")]
        [Required]
        public string Name { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }
    }
}
