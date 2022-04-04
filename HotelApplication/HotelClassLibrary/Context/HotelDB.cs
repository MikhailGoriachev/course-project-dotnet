using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

using HotelClassLibrary.Models;                         // модели
using HotelClassLibrary.Context.Configuration;          // конфигурации

namespace HotelClassLibrary.Context
{
    // Класс Контекст базы данных Отель
    public partial class HotelDB : DbContext
    {
        // персоны
        public virtual DbSet<Person> Persons { get; set; }


        // клиенты
        public virtual DbSet<Client> Clients { get; set; }


        // история поселений в гостиницу
        public virtual DbSet<HistoryRegistrationHotel> HistoryRegistrationHotel { get; set; }


        // города
        public virtual DbSet<City> Cities { get; set; }


        // номера гостиницы
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }


        // типы номеров
        public virtual DbSet<TypeHotelRoom> TypesHotelRoom { get; set; }


        // этажи
        public virtual DbSet<Floor> Floors { get; set; }


        // cлужащие гостиницы
        public virtual DbSet<Employee> Employees { get; set; }


        // история фактов уборки
        public virtual DbSet<CleaningHistory> CleaningHistory { get; set; }


        // график уборки
        public virtual DbSet<CleaningSchedule> CleaningSchedule { get; set; }


        // дни недели
        public virtual DbSet<Models.DayOfWeek> DaysOfWeek { get; set; }


        #region Конструкотры


        // конструктор статический
        static HotelDB()
        {
            // установка объекта инициализации
            Database.SetInitializer(new HotelInitializer());
        }


        // конструктор по умолчанию
        public HotelDB()
            : base("name=HotelDB")
        {
            // инициализация базы данных
            Database.Initialize(false);
        }


        #endregion


        #region Методы

        #endregion
    }
}
