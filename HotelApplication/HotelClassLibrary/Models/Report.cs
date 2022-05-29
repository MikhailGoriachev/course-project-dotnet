using HotelClassLibrary.Controllers;
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
    public class Report : ObservableObject
    {
        // список записей регистрации
        public List<HistoryRegistrationHotel> History { get; set; }


        // список выбранных записей
        public List<HistoryRegistrationHotel> CurrentHistory { get; set; }


        // число клиентов
        private int _countClients;

        public int CountClients
        {
            get => _countClients;
            set
            {
                _countClients = value;
                OnPropertyChanged("CountClients");
            }
        }


        // номера гостиницы и количество дней занято/свободно
        public List<HotelRoom> Rooms { get; set; }


        // номера гостиницы и количество дней занято/свободно
        private List<HotelRoomReport> _resultRooms;
        public List<HotelRoomReport> ResultRooms
        {
            get => _resultRooms;
            set
            {
                _resultRooms = value;
                OnPropertyChanged("ResultRooms");
            }
        }

        // общая сумма дохода
        private int _account;
        public int Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged("Account");
            }
        }


        // начальная дата
        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }



        // конечная дата
        private DateTime _endDate;

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }



        #region Конструктор

        // конструктор инициализирующий
        public Report(List<HotelRoom> rooms, List<HistoryRegistrationHotel> history, DateTime begin, DateTime end)
        {
            // установка значений
            Rooms = rooms;
            History = history;
            StartDate = begin;
            EndDate = end;
        }

        #endregion

        #region Методы

        // получить отчёт
        public void CalcResult()
        {
            // записи за заданный период
            CurrentHistory = History.Where(hr => hr.RegistrationDate.Date >= _startDate.Date
                                                                   && hr.RegistrationDate.Date <= _endDate.Date)
                                                      .ToList();

            // количество клиентов
            CountClients = CurrentHistory.Select(h => h.Client)
                                         .Distinct()
                                         .Count();

            // количество дней
            int countDays = (int)(_endDate.DayOfYear - _startDate.DayOfYear) + 1;

            // получение сведений о номерах гостиницы
            ResultRooms = Rooms.Select(r => {

                // получение записей регистрации
                //List<HistoryRegistrationHotel> h = CurrentHistory.Where(hr => hr.HotelRoom.Id == r.Id)
                //                                                 .ToList();

                // количество занятых дней
                int busy = 0;

                for (DateTime date = StartDate.Date; date <= EndDate.Date; date = date.AddDays(1).Date)
                {
                     busy += HotelController.RoomIsBusy(r, date, CurrentHistory) ? 1 : 0;
                }


                return new HotelRoomReport { Room = r, EmptyDays = countDays - busy, BusyDays = busy };

            }).ToList();

            // сумма дохода
            Account = ResultRooms.Sum(r => r.BusyDays * r.Room.TypeHotelRoom.Price);

        }

        #endregion
    }
}
