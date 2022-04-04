using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Клиент
    public class Client
    {
        // первичный ключ
        public int Id { get; set; }


        // персона
        [Required]
        public Person Person { get; set; }


        // номер паспорта
        //[Required, Column(TypeName = "nvarchar(10)")]
        [Required]
        public string Passport { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }


    }
}
