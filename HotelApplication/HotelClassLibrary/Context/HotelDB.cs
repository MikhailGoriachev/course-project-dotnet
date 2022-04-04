using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

using HotelClassLibrary.Models;                         // ������
using HotelClassLibrary.Context.Configuration;          // ������������

namespace HotelClassLibrary.Context
{
    // ����� �������� ���� ������ �����
    public partial class HotelDB : DbContext
    {
        // �������
        public virtual DbSet<Person> Persons { get; set; }


        // �������
        public virtual DbSet<Client> Clients { get; set; }


        // ������� ��������� � ���������
        public virtual DbSet<HistoryRegistrationHotel> HistoryRegistrationHotel { get; set; }


        // ������
        public virtual DbSet<City> Cities { get; set; }


        // ������ ���������
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }


        // ���� �������
        public virtual DbSet<TypeHotelRoom> TypesHotelRoom { get; set; }


        // �����
        public virtual DbSet<Floor> Floors { get; set; }


        // c������� ���������
        public virtual DbSet<Employee> Employees { get; set; }


        // ������� ������ ������
        public virtual DbSet<CleaningHistory> CleaningHistory { get; set; }


        // ������ ������
        public virtual DbSet<CleaningSchedule> CleaningSchedule { get; set; }


        // ��� ������
        public virtual DbSet<Models.DayOfWeek> DaysOfWeek { get; set; }


        #region ������������


        // ����������� �����������
        static HotelDB()
        {
            // ��������� ������� �������������
            Database.SetInitializer(new HotelInitializer());
        }


        // ����������� �� ���������
        public HotelDB()
            : base("name=HotelDB")
        {
            // ������������� ���� ������
            Database.Initialize(false);
        }


        #endregion


        #region ������

        #endregion
    }
}
