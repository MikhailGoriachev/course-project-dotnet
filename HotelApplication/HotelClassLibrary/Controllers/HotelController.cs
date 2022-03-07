using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelClassLibrary.Models;         // модели
using HotelClassLibrary.Utilities;      // утилиты

namespace HotelClassLibrary.Controllers
{
    // Класс Контроллер заполнения таблиц базы данных тестовыми данными
    public class HotelController
    {
        // модель базы данных
        private HotelDataContext _data;

        public HotelDataContext Data
        {
            get => _data;
            set => _data = value;
        }

        #region Конструкторы

        // конструктор по умолчанию
        public HotelController() : this(new HotelDataContext()) { }


        // конструктор инициализирующий
        public HotelController(HotelDataContext hotelDataContext)
        {
            // установка значений
            _data = hotelDataContext;
        }

        #endregion

        #region Методы

        #region Получение данных из таблиц

        // получить данные из таблицы График уборки				        (CleaningSchedule)
        public IEnumerable GetCleaningSchedule() => _data.CleaningScheduleViews;


        // получить данные из таблицы Дни недели					    (DaysOfWeek)
        public IEnumerable GetDaysOfWeek() => _data.DaysOfWeekViews;


        // получить данные из таблицы История фактов уборки		        (CleaningHistory)
        public IEnumerable GetCleaningHistory() => _data.CleaningHistoryViews;


        // получить данные из таблицы История поселений в гостиницу     (HistoryRegistrationHotel)
        public IEnumerable GetHistoryRegistrationHotel() => _data.HistoryRegistrationHotelViews;


        // получить данные из таблицы Города					        (Cities)
        public IEnumerable GetCities() => _data.CitiesViews;


        // получить данные из таблицы Номера гостиницы					(HotelRooms)
        public IEnumerable GetHotelRooms() => _data.HotelRoomsViews;


        // получить данные из таблицы Типы номеров						(TypesHotelRoom)
        public IEnumerable GetTypesHotelRoom() => _data.TypesHotelRoomViews;


        // получить данные из таблицы Этажи						        (Floors)
        public IEnumerable GetFloors() => _data.FloorsViews;


        // получить данные из таблицы Служащие гостиницы				(Employees)
        public IEnumerable GetEmployees() => _data.EmployeesViews;


        // получить данные из таблицы Клиенты							(Clients)
        public IEnumerable GetClients() => _data.ClientsViews;


        // получить данные из таблицы Персоны							(Persons)
        public IEnumerable GetPersons() => _data.PersonsViews;


        #endregion


        #region Заполнение базы данных

        // генерация данных и заполнение таблиц базы данных 
        public void FillDataBase(DateTime startDate)
        {
            // очистка таблиц
            ClearTables();

            // заполнение таблицы Клиенты							(Clients)
            FillClientsTable();

            // заполнение таблицы Служащие гостиницы				(Employees)
            FillEmployeesTable();

            // заполнение таблицы Типы номеров					    (TypesHotelRoom)
            FillTypesHotelRoomTable();

            // заполнение таблицы Этажи                             (Floors)
            //FillFloorsTable(Utils.GetRand(3, 6));
            FillFloorsTable(4);

            // заполнение таблицы Номера гостиницы				    (HotelRooms)
            FillHotelRoomsTable(Utils.GetRand(10, 15), Utils.GetRand(10, 15), Utils.GetRand(10, 15), Utils.GetRand(2, 4));

            // заполнение таблицы Города							(Cities)
            FillCitiesTable();

            // заполнение таблицы История поселений в гостиницу	    (HistoryRegistrationHotel)
            FillHistoryRegistrationHotelTable();

            // заполнение таблицы Дни недели						(DaysOfWeek)
            FillDaysOfWeekTable();

            // заполнение таблицы График уборки					    (CleaningSchedule)
            FillCleaningSchedule();

            // заполнение таблицы История фактов уборки			    (CleaningHistory)
            FillCleaningHistoryTable(startDate);
        }


        // очистка таблиц
        public void ClearTables()
        {
            // удаление данных из таблиц
            _data.CleaningSchedules.DeleteAllOnSubmit(_data.CleaningSchedules.ToList());
            _data.DaysOfWeeks.DeleteAllOnSubmit(_data.DaysOfWeeks.ToList());
            _data.CleaningHistories.DeleteAllOnSubmit(_data.CleaningHistories.ToList());
            _data.HistoryRegistrationHotels.DeleteAllOnSubmit(_data.HistoryRegistrationHotels.ToList());
            _data.Cities.DeleteAllOnSubmit(_data.Cities.ToList());
            _data.HotelRooms.DeleteAllOnSubmit(_data.HotelRooms.ToList());
            _data.TypesHotelRooms.DeleteAllOnSubmit(_data.TypesHotelRooms.ToList());
            _data.Floors.DeleteAllOnSubmit(_data.Floors.ToList());
            _data.Employees.DeleteAllOnSubmit(_data.Employees.ToList());
            _data.Clients.DeleteAllOnSubmit(_data.Clients.ToList());
            _data.Persons.DeleteAllOnSubmit(_data.Persons.ToList());

            _data.SubmitChanges();
        }


        // заполнение таблицы Персоны							(Persons)
        public void FillPersonsTable(int n = 40)
        {
            // генерация списка персон
            List<Person> persons = Enumerable.Repeat(new Person(), n)
                                             .Select(p => Utils.GetPerson())
                                             .ToList();

            // запись в таблицу базы данных
            _data.Persons.InsertAllOnSubmit(persons);

            // сохранение данных в БД
            _data.SubmitChanges();
        }


        // заполнение таблицы Клиенты							(Clients)
        public void FillClientsTable(int n = 15)
        {
            // генерация списка клиентов
            List<Client> clients = Enumerable.Repeat(new Client(), n)
                                             .Select(p => new Client
                                             {
                                                 Person = Utils.GetPerson(),
                                                 Passport = Utils.GetPassport()
                                             })
                                             .ToList();

            // добавление записей в таблицу
            _data.Clients.InsertAllOnSubmit(clients);

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы Служащие гостиницы				(Employees)
        public void FillEmployeesTable(int n = 15)
        {
            // генерация списка служащих
            List<Employee> employees = Enumerable.Repeat(new Employee(), n)
                                             .Select(p => new Employee
                                             {
                                                 Person = Utils.GetPerson(),
                                                 WorkState = (Utils.rand.Next() & 1) == 0,
                                             })
                                             .ToList();

            // добавление записей в таблицу
            _data.Employees.InsertAllOnSubmit(employees);

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы Типы номеров					    (TypesHotelRoom)
        public void FillTypesHotelRoomTable()
        {
            // добавление записей в таблицу
            _data.TypesHotelRooms.InsertAllOnSubmit(Utils.TypesHotelRoom);

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы Этажи                             (Floors)
        public void FillFloorsTable(int countFloors = 4)
        {
            // номер этажа
            int number = 1;

            // добавление записей в таблицу
            _data.Floors.InsertAllOnSubmit(Enumerable.Repeat(0, countFloors).Select(f => new Floor { Number = number++ }).ToList());

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы Номера гостиницы				    (HotelRooms)
        public void FillHotelRoomsTable(int singleRooms, int doubleRooms, int tripleRooms, int countFloors)
        {
            // гостиничные номера
            List<HotelRoom> rooms = new List<HotelRoom>();

            // номер гостиничного номера
            int number = 1;

            // массив с количеством номеров
            int[] countRooms = new[] { singleRooms, doubleRooms, tripleRooms };

            // список этажей
            List<Floor> floors = _data.Floors.ToList();

            // тип номера
            TypesHotelRoom type;

            // генерация номеров
            for (int i = 0; i < countRooms.Length; i++)
            {
                type = _data.TypesHotelRooms.ToList().ElementAt(i);

                // генерация одноместных номеров
                rooms.AddRange(Enumerable.Repeat(0, countRooms[i]).Select(h => new HotelRoom
                {
                    Floor = floors[Utils.GetRand(0, floors.Count)],
                    Number = number++,
                    State = (Utils.rand.Next() & 1) == 0,
                    TypesHotelRoom = type
                }));
            }

            // добавление номеров в таблицу
            _data.HotelRooms.InsertAllOnSubmit(rooms);

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы Города							(Cities)
        public void FillCitiesTable()
        {
            // заполнение таблицы городов
            _data.Cities.InsertAllOnSubmit(Utils.Cities.Select(c => new City { Name = c }));

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы История поселений в гостиницу	    (HistoryRegistrationHotel)
        public void FillHistoryRegistrationHotelTable(int n = 15)
        {
            // список клиентов в таблице клиентов
            List<Client> clients = _data.Clients.ToList();

            // гостиничные номера
            List<HotelRoom> rooms = _data.HotelRooms.ToList();

            // города
            List<City> cities = _data.Cities.ToList();

            // текущая дата
            DateTime date = DateTime.Now;

            // генерация файтов поселения
            List<HistoryRegistrationHotel> history = Enumerable.Repeat(0, n)
                                                               .Select(h => new HistoryRegistrationHotel
                                                               {
                                                                   Client           = clients[Utils.GetRand(0, clients.Count)],
                                                                   HotelRoom        = rooms[Utils.GetRand(0, rooms.Count)],
                                                                   City             = cities[Utils.GetRand(0, cities.Count)],
                                                                   RegistrationDate = new DateTime(date.Year, date.Month, Utils.GetRand(1, date.Day)),
                                                                   Duration         = Utils.GetRand(1, 7)
                                                               })
                                                               .ToList();
                                                                
        }


        // заполнение таблицы Дни недели						(DaysOfWeek)
        public void FillDaysOfWeekTable()
        {
            // номер дня
            int number = 1;

            // заполнение таблицы дней недели
            _data.DaysOfWeeks.InsertAllOnSubmit(Utils.DaysOfWeek.Select(c => new DaysOfWeek { Name = c, Number = number++ }));

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы График уборки					    (CleaningSchedule)
        public void FillCleaningSchedule()
        {
            // дни недели
            List<DaysOfWeek> days = _data.DaysOfWeeks.ToList();

            // график уборки
            List<CleaningSchedule> cleanings = _data.CleaningSchedules.ToList();

            // этажи
            List<Floor> floors = _data.Floors.ToList();

            // рабочие
            List<Employee> employees = _data.Employees.ToList();

            // заполнение таблицы по дням
            for (int i = 0, k = 0; i < days.Count; i++, k = 0)
                cleanings.AddRange(Enumerable.Repeat(0, _data.Floors.Count())
                                             .Select(f => new CleaningSchedule
                                             {
                                                 DaysOfWeek = days[i],
                                                 Floor = floors[k++],
                                                 Employee = employees[Utils.GetRand(0, employees.Count)]
                                             }));

            // заполнение таблицы графика уборки
            _data.CleaningSchedules.InsertAllOnSubmit(cleanings);

            // запись в базу данных
            _data.SubmitChanges();
        }


        // заполнение таблицы История фактов уборки			    (CleaningHistory)
        public void FillCleaningHistoryTable(DateTime startDate)
        {
            // список графика уборки
            List<CleaningSchedule> schedules = _data.CleaningSchedules.ToList();

            // список фактов уборки
            List<CleaningHistory> history = new List<CleaningHistory>();

            // заполнение списка фактов уборки
            while (startDate <= DateTime.Now)
            {
                // выбрать записи из графика по дню недели м добавить записи по уборке
                schedules.Where(c => startDate.DayOfWeek == (DayOfWeek)c.DaysOfWeek.Number - 1)
                         .ToList()
                         .ForEach(c => history.Add(new CleaningHistory { Floor = c.Floor, DateCleaning = startDate, Employee = c.Employee }));

                // увеличение даты на день
                startDate = startDate.AddDays(1);
            }

            // заполнение таблицы истрии фактов уборки
            _data.CleaningHistories.InsertAllOnSubmit(history);

            // запись в базу данных
            _data.SubmitChanges();
        }

        #endregion

        #endregion
    }
}
