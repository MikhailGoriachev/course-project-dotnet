using HotelClassLibrary.Controllers;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления страницы "Отчёт"
    public class ReportViewModel : ObservableObject
    {
        // начальная дата
        public DateTime StartDate { get; set; }


        // конечная дата
        public DateTime EndDate { get; set; }


        // объёкт отчёта
        public Report Report { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public ReportViewModel() 
        {
            // установка значений
            StartDate = DateTime.Now.AddDays(-20);
            EndDate = DateTime.Now;
            Report = new Report(HotelController.GetHotelRooms(), HotelController.GetHistoryRegistrationHotel(), StartDate, EndDate);
            Report.CalcResult();
        }

        #endregion

        #region Методы

        // сохранение регистрации в базе данных
        private RelayCommand _resultReportCommand;

        public RelayCommand ResultReportCommand => _resultReportCommand ?? (_resultReportCommand = new RelayCommand((o) =>
        {
            // установка данных в поля объекта отчёта
            Report.StartDate = StartDate;
            Report.EndDate = EndDate;

            // вычисление отчёта
            Report.CalcResult();
        }));

        #endregion
    }
}
