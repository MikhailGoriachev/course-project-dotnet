using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Отчёт за указанный квартал года

    /*
     *  Такой отчет должен содержать следующие сведения: число клиентов за указанный 
     *  период, сколько дней был занят и свободен каждый из номеров гостиницы, общая 
     *  сумма дохода.
    */
    public class Report
    {
        // число клиентов
        public int CountClients { get; set; }


        // номера гостиницы и количество дней занято/свободно
        public List<(HotelRoom room, int emptyDays, int busyDays)> Rooms { get; set; }


        // общая сумма дохода
        public int Account { get; set; }


        #region Конструктор

        // конструктор инициализирующий
        public Report(List<HotelRoom> rooms, List<HistoryRegistrationHotel> history, DateTime begin, DateTime end)
        {
            // записи за заданный период
            List<HistoryRegistrationHotel> currentHistory = history.Where(hr => hr.RegistrationDate.Date >= begin.Date
                                                                   && hr.RegistrationDate.Date <= end.Date)
                                                      .ToList();

            // количество клиентов
            CountClients = currentHistory.Select(h => h.Client)
                                         .Distinct()
                                         .Count();

            // количество дней
            int countDays = (int)(begin - end).TotalDays;

            // получение сведений о номерах гостиницы
            Rooms = rooms.Select(r => { 

                // получение записей регистрации
                List<HistoryRegistrationHotel> h = currentHistory.Where(hr => hr.HotelRoom.Id == r.Id)
                                                                 .ToList();

                // сумма количества дней
                int sum = h.Sum(s => s.Duration);

                return (r, countDays - sum, sum);

            }).ToList();

            // сумма дохода
            Account = Rooms.Sum(r => r.busyDays * r.room.TypeHotelRoom.Price);
        }

        #endregion
    }
}
