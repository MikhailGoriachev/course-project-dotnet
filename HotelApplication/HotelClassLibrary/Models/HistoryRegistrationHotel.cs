using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Запись истории поселений в гостиницу
    public class HistoryRegistrationHotel
    {
        // первичный ключ
        public int Id { get; set; }


        // клиент
        [Required]
        public Client Client { get; set; }


        // гостиничный номер
        [Required]
        public HotelRoom HotelRoom { get; set; }


        // город, из которого прибыл клиент
        [Required]
        public City City { get; set; }


        // дата поселения
        // [Required, Column(TypeName = "date")]
        [Required]
        public DateTime RegistrationDate { get; set; }


        // длительность проживания
        [Required]
        public int Duration { get; set; }


        // статус удаления
        [Required]
        public bool IsDeleted { get; set; }


    }
}
