using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelClassLibrary.Models;         // модели
using HotelClassLibrary.Utilities;      // утилиты
using HotelClassLibrary.Context;        // базы данных

namespace HotelClassLibrary.Controllers
{
    // Класс Контроллер заполнения таблиц базы данных тестовыми данными
    public class HotelController
    {
        // модель базы данных
        private HotelDB _data;

        public HotelDB Data
        {
            get => _data;
            set => _data = value;
        }


        #region Конструкторы

        // конструктор по умолчанию
        public HotelController() : this(new HotelDB()) { }


        // конструктор инициализирующий
        public HotelController(HotelDB hotelDb)
        {
            // установка значений
            _data = hotelDb;

            _data.Configuration.LazyLoadingEnabled = true;
        }

        #endregion

        #region Методы

        // получить статус номера - свободен или занят (занято - true) в заданную дату
        public bool RoomIsBusy(HotelRoom room, DateTime date) => GetHistoryRegistrationHotelAsync().Result
                            .FirstOrDefault(h => date.Date >= h.RegistrationDate.Date && date <= h.RegistrationDate.AddDays(h.Duration).Date
                                                        && h.HotelRoom.Id == room.Id) != null;


        // получить количество занятых мест номера - в заданную дату
        public int CountBusyPlace(HotelRoom room, DateTime date) => GetHistoryRegistrationHotelAsync().Result
                            .Where(h => date.Date >= h.RegistrationDate.Date && date <= h.RegistrationDate.AddDays(h.Duration).Date 
                                    && h.HotelRoom.Id == room.Id)
                            .Count();


        // операция над записью таблицы
        public void ChangeEntity(DbEntityEntry entity, EntityState state, DbSet dbSet)
        {
            // установка состояния
            _data.Entry(entity).State = state;

            // исполнение функции
            switch (state)
            {
                case EntityState.Added:
                    dbSet.Add(entity);
                    break;
                case EntityState.Deleted:
                    dbSet.Remove(entity);
                    break;
                default:
                    break;
            }

            // сохранение в базе данных
            _data.SaveChanges();
        }




        #region Функции администратора

        /*
         * Администратор должен иметь возможность выполнить следующие операции:
         *  •	принять на работу или уволить служащего гостиницы
         *  •	изменить расписание работы служащего
         *  •	поселить или выселить клиента.
        */

        // принять на работу или уволить служащего гостиницы
        public void AddEmployee(Employee employee)
        {
            // добавить служащего
            _data.Employees.AddOrUpdate(employee);

            // сохранить в базу данных
            _data.SaveChanges();

            // корректировка графика уборки
            CorrectCleaningSchedule();
        }


        // изменить расписание работы служащего


        // поселить или выселить клиента

        #endregion



        #region Операции администратора

        // Администратор должен иметь возможность выполнить следующие операции:
        // •	принять на работу или уволить служащего гостиницы
        // •	изменить расписание работы служащего
        // •	поселить или выселить клиента.
        // •	автоматической выдачи клиенту счета за проживание в гостинице.
        // •	отчета о работе гостиницы за указанный квартал текущего года. Такой отчет должен 
        //      содержать следующие сведения: число клиентов за указанный период, сколько дней был занят 
        //      и свободен каждый из номеров гостиницы, общая сумма дохода.


        // принять на работу или уволить служащего гостиницы
        public async Task AddEmployeeAsync(Employee employee) =>
            await Task.Run(() =>
            {
                // добавить служащего
                _data.Employees.Add(employee);

                // установка статуса добавления
                _data.Entry(employee).State = EntityState.Added;

                // асинхронное добавление
                _data.SaveChanges();

                // корректировка графика уборки
                CorrectCleaningSchedule();
            });


        #region Изменить расписание уборки

        // изменить расписание уборки
        // параметры: addDays - новые дни работы, deleteDays - удаляемые дни работы
        public void ChangeSchedule(List<CleaningSchedule> addDays, List<CleaningSchedule> deleteDays)
        {
            // добавление записей
            addDays.ForEach(d => _data.Entry(d).State = EntityState.Added);
            _data.CleaningSchedule.AddRange(addDays);
            _data.SaveChanges();

            // удаление записей
            deleteDays.ForEach(d => _data.Entry(deleteDays).State = EntityState.Deleted);
            _data.CleaningSchedule.RemoveRange(deleteDays);
            _data.SaveChanges();

            // корректировка графика уборки
            CorrectCleaningSchedule();
        }


        // корректировка графика уборки
        public void CorrectCleaningSchedule()
        {
            // список полученных записей
            List<CleaningSchedule> list = new List<CleaningSchedule>();

            // цикл перебора дней недели
            foreach (var day in _data.DaysOfWeek)
            {
                // цикл перебора этажей
                foreach (var floor in _data.Floors)
                {
                    // поиск записей для этого этажа и дня недели
                    list = _data.CleaningSchedule.Where(c => c.DayOfWeek == day && c.Floor == floor)
                                                 .ToList();
                    
                    // если список пуст - установить работника на этот день, 
                    // у которго меньше всего рабочих дней
                    if (list.Count == 0)
                    {
                        CleaningSchedule c = new CleaningSchedule{
                            Employee = GetMinEmployee(),
                            DayOfWeek = day,
                            Floor = floor
                        };

                        _data.Entry(c).State = EntityState.Added;
                        _data.CleaningSchedule.Add(c);
                    }
                        
                    // найдено записей больше 1, то все записи кроме последней
                    if (list.Count > 1)
                    {
                        // удаление первой записи из списка
                        list.Remove(list[0]);

                        // удаление оставшихся записей из базы данных
                        list.ForEach(c => _data.Entry(c).State = EntityState.Deleted);
                        _data.CleaningSchedule.RemoveRange(list);
                    }
                }
            }

            // сохранение в базу данных
            _data.SaveChanges();
        }


        // получить работника, у которого меньше всего рабочих дней
        public Employee GetMinEmployee() 
        {
            // получить коллекцию работников с количеством смен
            var em = _data.Employees.Where(e => e.IsDeleted == false)
                            .Select(e => new {Employee = e, Count =_data.CleaningSchedule.Count(c => c.Employee.Id == e.Id)})
                            .ToList();

            // минимальное значение количества смен
            int min = em.Min(e => e.Count);

            return em.First(e => e.Count == min).Employee;
        }

        #endregion


        // разместить клиента
        public bool PlaceClient(Client client, HotelRoom room, City city, int Duration)
        {
            // если номер заполнен
            if(CountBusyPlace(room, DateTime.Now) == room.TypeHotelRoom.CountPlace)
                return false;

            // запись регистрации
            HistoryRegistrationHotel registration = new HistoryRegistrationHotel{
                                                            Client = client,
                                                            HotelRoom = room,
                                                            Duration = Duration,
                                                            City = city
                                                        };

            // установить статус
            _data.Entry(registration).State = EntityState.Added;

            // добавить в коллекцию
            _data.HistoryRegistrationHotel.Add(registration);

            // сохранить изменения в базе данных
            _data.SaveChanges();

            return true;
        }


        // выселить клиента 
        // параметр room - для того, чтоб выселить клиента из конкретного номера, так как 
        // один клиент может жить в нескольких номерах, по умолчанию null
        public bool EvictClient(Client client, HotelRoom room = null)
        {
            // текущая дата
            DateTime now = DateTime.Now.Date;

            // записи о регистрации клиента по текущей дате
            List<HistoryRegistrationHotel> list = _data.HistoryRegistrationHotel.ToList()
                                                    .Where(h => h.Client == client 
                                                            && now >= h.RegistrationDate.Date
                                                            && now <= h.RegistrationDate.AddDays(h.Duration).Date)
                                                    .ToList();

            // текущая дата в днях
            int nowDays = now.TimeOfDay.Days;

            // если требуется выселить из определенного номера,
            // то урезать количество дней проживания по текущую дату
            if (room != null)
            {
                // поиск записи по номеру проживания
                HistoryRegistrationHotel elem = list.First(h => h.HotelRoom.Id == room.Id);
                
                // если записей по указанному номеру не найдено
                if (elem == null)
                    return false;

                // урезание количества дней проживания по текущей дате
                elem.Duration = nowDays - elem.RegistrationDate.Date.TimeOfDay.Days;

                return true;
            }


            // выселить из всех номеров выбранного клиента
            list.ForEach(e => e.Duration = nowDays - e.RegistrationDate.Date.TimeOfDay.Days);

            return true;
        }


        #endregion


        #region Отчёт

        /*
         *  Необходимо предусмотреть также возможность автоматической выдачи клиенту 
         *  счета за проживание в гостинице и получения отчета о работе гостиницы за 
         *  указанный квартал текущего года. 
         *  
         *  Такой отчет должен содержать следующие сведения: число клиентов за указанный 
         *  период, сколько дней был занят и свободен каждый из номеров гостиницы, общая 
         *  сумма дохода.
        */


        // получение счёта за проживание клиента
        public int GetAccount(Client client, HotelRoom room = null, DateTime dateStart = new DateTime())
        {
            // поиск записей регистрации по данному колиенту и дате
            List<HistoryRegistrationHotel> histories = GetHistoryRegistrationHotelAsync().Result;

            // если текущая дата равна нулевой дате, то получить последнюю запись клиента
            HistoryRegistrationHotel elem = dateStart.Year == 0 
                                                            ? histories.FirstOrDefault(h => h.Client == client && h.RegistrationDate.Date == dateStart.Date 
                                                                                                               && h.HotelRoom.Id == (room ?? h.HotelRoom).Id)
                                                            : histories.FirstOrDefault(h => h.Client == client && h.HotelRoom.Id == (room ?? h.HotelRoom).Id
                                                                                                               && h.HotelRoom.Id == (room ?? h.HotelRoom).Id);

            // получение стоимости
            return elem.Duration * elem.HotelRoom.TypeHotelRoom.Price;
        }


        // получение отчёта за указанный квартал
        public Report GetReport(DateTime begin, DateTime end) => 
            new Report(GetHotelRoomsAsync().Result, GetHistoryRegistrationHotelAsync().Result, begin, end);


        #endregion

        #region Запросы

        // 1.	О клиентах, проживающих в заданном номере
        public List<Client> Proc1(int roomNum)
        {
            // текущая дата
            DateTime date = DateTime.Now;

            // гостичный номер
            HotelRoom room = _data.HotelRooms.FirstOrDefault(r => r.Number == roomNum);

            // если номер свободен
            if (room == null && RoomIsBusy(room, date))
                return new List<Client>();


            // выборка клиентов проживающих в заданном номере
            return GetHistoryRegistrationHotelAsync().Result.Where(h => h.HotelRoom.Number == roomNum && date.Date >= h.RegistrationDate.Date 
                                                                                          && date <= h.RegistrationDate.AddDays(h.Duration).Date)
                                                            .Select(h => h.Client)
                                                            .Distinct()
                                                            .ToList();
        }


        // 2.	О клиентах, прибывших из заданного города
        public List<Client> Proc2(string city) => GetHistoryRegistrationHotelAsync().Result.Where(h => h.City.Name == city)
                                                                                           .Select(h => h.Client)
                                                                                           .Distinct()
                                                                                           .ToList();


        // 3.	О том, кто из служащих убирал номер указанного клиента в заданный день недели
        public List<Employee> Proc3(string passport, DateTime date)
        {
            // комната
            HotelRoom room = GetHistoryRegistrationHotelAsync().Result.First(h => h.Client.Passport == passport && RoomIsBusy(h.HotelRoom, date))?.HotelRoom;

            // если такой записи не существует
            if (room == null)
                return new List<Employee>();

            // этаж комнаты
            int floor = room.Floor.Number;

            // выборка работника
            return GetCleaningHistoryAsync().Result.Where(c => c.Floor.Number == floor && c.DateCleaning.Date == date.Date).Select(c => c.Employee).ToList();
        }


        // 4.	Есть ли в гостинице свободные места и свободные номера и, если есть, то сколько и какие именно номера свободны.
        public List<HotelRoom> Proc4()
        {
            // текущая дата
            DateTime date = DateTime.Now;

            return GetHotelRoomsAsync().Result.Where(h => !RoomIsBusy(h, date)).ToList();
        }


        #endregion


        #region Получение данных из таблиц


        // получить данные из таблицы График уборки				        (CleaningSchedule)
        public async Task<List<CleaningSchedule>> GetCleaningScheduleAsync() => await _data.CleaningSchedule.Include(c => c.Employee)
                                                                                                       .Include(c => c.Employee.Person)
                                                                                                       .Include(c => c.Floor)
                                                                                                       .Include(c => c.DayOfWeek)
                                                                                                       .ToListAsync();


        // получить данные из таблицы Дни недели					    (DaysOfWeek)
        public async Task<List<Models.DayOfWeek>> GetDaysOfWeekAsync() => await _data.DaysOfWeek.ToListAsync();


        // получить данные из таблицы История фактов уборки		        (CleaningHistory)
        public async Task<List<CleaningHistory>> GetCleaningHistoryAsync() => await _data.CleaningHistory.Include(h => h.Employee)
                                                                                                         .Include(c => c.Employee.Person)
                                                                                                         .Include(h => h.Floor)
                                                                                                         .ToListAsync();


        // получить данные из таблицы История поселений в гостиницу     (HistoryRegistrationHotel)
        public async Task<List<HistoryRegistrationHotel>> GetHistoryRegistrationHotelAsync() => await _data.HistoryRegistrationHotel.Include(r => r.Client)
                                                                                                                                    .Include(r => r.Client.Person)
                                                                                                                                    .Include(r => r.City)
                                                                                                                                    .Include(r => r.HotelRoom)
                                                                                                                                    .Include(r => r.HotelRoom.TypeHotelRoom)
                                                                                                                                    .Include(r => r.HotelRoom.Floor)
                                                                                                                                    .ToListAsync();


        // получить данные из таблицы Города					        (Cities)
        public async Task<List<City>> GetCitiesAsync() => await _data.Cities.ToListAsync();


        // получить данные из таблицы Номера гостиницы					(HotelRooms)
        public async Task<List<HotelRoom>> GetHotelRoomsAsync() => await _data.HotelRooms.Include(r => r.TypeHotelRoom)
                                                                                         .Include(r => r.Floor)
                                                                                         .ToListAsync();


        // получить данные из таблицы Типы номеров						(TypesHotelRoom)
        public async Task<List<TypeHotelRoom>> GetTypesHotelRoomAsync() => await _data.TypesHotelRoom.ToListAsync();


        // получить данные из таблицы Этажи						        (Floors)
        public async Task<List<Floor>> GetFloorsAsync() => await _data.Floors.ToListAsync();


        // получить данные из таблицы Служащие гостиницы				(Employees)
        public async Task<List<Employee>> GetEmployeesAsync() => await _data.Employees.Include(e => e.Person).ToListAsync();


        // получить данные из таблицы Клиенты							(Clients)
        public async Task<List<Client>> GetClientsAsync() => await _data.Clients.Include(c => c.Person).ToListAsync();


        // получить данные из таблицы Персоны							(Persons)
        public async Task<List<Person>> GetPersonsAsync() => await _data.Persons.ToListAsync();


        #endregion


        #region Заполнение базы данных

        // генерация данных и заполнение таблиц базы данных 
        public async Task FillDataBase(DateTime startDate) =>
            await Task.Run(() =>
            {
                // очистка таблиц
                ClearTables();

                // заполнение таблицы Клиенты							(Clients)
                FillClientsTable(Utils.GetRand(300, 500));

                // заполнение таблицы Служащие гостиницы				(Employees)
                FillEmployeesTable(Utils.GetRand(10, 20));

                // заполнение таблицы Типы номеров					    (TypesHotelRoom)
                FillTypesHotelRoomTable();

                // заполнение таблицы Этажи                             (Floors)
                FillFloorsTable(Utils.GetRand(3, 6));

                // заполнение таблицы Номера гостиницы				    (HotelRooms)
                FillHotelRoomsTable(Utils.GetRand(10, 20), Utils.GetRand(20, 40), Utils.GetRand(10, 40));

                // заполнение таблицы Города							(Cities)
                FillCitiesTable();

                // заполнение таблицы История поселений в гостиницу	    (HistoryRegistrationHotel)
                FillHistoryRegistrationHotelTable(startDate, Utils.GetRand(500, 600));

                // заполнение таблицы Дни недели						(DaysOfWeek)
                FillDaysOfWeekTable();

                // заполнение таблицы График уборки					    (CleaningSchedule)
                FillCleaningSchedule();

                // заполнение таблицы История фактов уборки			    (CleaningHistory)
                FillCleaningHistoryTable(startDate);
            });


        // очистка таблиц
        public void ClearTables()
        {
            // удаление записей
            RemoveRangeTable(_data.CleaningSchedule);
            RemoveRangeTable(_data.DaysOfWeek);
            RemoveRangeTable(_data.CleaningHistory);
            RemoveRangeTable(_data.HistoryRegistrationHotel);
            RemoveRangeTable(_data.Cities);
            RemoveRangeTable(_data.HotelRooms);
            RemoveRangeTable(_data.TypesHotelRoom);
            RemoveRangeTable(_data.Floors);
            RemoveRangeTable(_data.Employees);
            RemoveRangeTable(_data.Clients);
            RemoveRangeTable(_data.Persons);
        }

        // удаление записей из таблицы
        public void RemoveRangeTable<T>(DbSet<T> dbSet) where T: class 
        {
            // удаление данных из таблиц
            var list = dbSet.ToList();

            // установка фалага для удаления записей
            list.ForEach(d => {
                _data.Entry(d).State = EntityState.Deleted;
                dbSet.Remove(d);
            });

            // удаление записей
            dbSet.RemoveRange(list);
            _data.SaveChanges();
        }


        // заполнение таблицы Персоны							(Persons)
        public void FillPersonsTable(int n = 40)
        {
            // генерация списка персон
            List<Person> persons = Enumerable.Repeat(new Person(), n)
                                             .Select(p => Utils.GetPerson())
                                             .ToList();

            // запись в таблицу базы данных
            _data.Persons.AddRange(persons);

            // сохранение данных в БД
            _data.SaveChanges();
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
            _data.Clients.AddRange(clients);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Служащие гостиницы				(Employees)
        public void FillEmployeesTable(int n = 15)
        {
            // генерация списка служащих
            List<Employee> employees = Enumerable.Repeat(new Employee(), n)
                                             .Select(p => new Employee
                                             {
                                                 Person = Utils.GetPerson()
                                             })
                                             .ToList();

            // добавление записей в таблицу
            _data.Employees.AddRange(employees);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Типы номеров					    (TypesHotelRoom)
        public void FillTypesHotelRoomTable()
        {
            // добавление записей в таблицу
            _data.TypesHotelRoom.AddRange(Utils.TypesHotelRoom);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Этажи                             (Floors)
        public void FillFloorsTable(int countFloors = 4)
        {
            // номер этажа
            int number = 1;

            // добавление записей в таблицу
            _data.Floors.AddRange(Enumerable.Repeat(0, countFloors).Select(f => new Floor { Number = number++ }).ToList());

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Номера гостиницы				    (HotelRooms)
        public void FillHotelRoomsTable(int singleRooms, int doubleRooms, int tripleRooms)
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
            TypeHotelRoom type;

            // генерация номеров
            for (int i = 0; i < countRooms.Length; i++)
            {
                type = _data.TypesHotelRoom.ToList().ElementAt(i);

                // генерация одноместных номеров
                rooms.AddRange(Enumerable.Repeat(0, countRooms[i]).Select(h => new HotelRoom
                {
                    Floor = floors[Utils.GetRand(0, floors.Count)],
                    Number = number,
                    TypeHotelRoom = type,
                    PhoneNumber = (80000 + number++).ToString()
                }));
            }

            // добавление номеров в таблицу
            _data.HotelRooms.AddRange(rooms);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Города							(Cities)
        public void FillCitiesTable()
        {
            // заполнение таблицы городов
            _data.Cities.AddRange(Utils.Cities.Select(c => new City { Name = c }));

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы История поселений в гостиницу	    (HistoryRegistrationHotel)
        public void FillHistoryRegistrationHotelTable(DateTime startDate, int n = 80)
        {
            // список клиентов в таблице клиентов
            List<Client> clients = _data.Clients.ToList();

            // города
            List<City> cities = _data.Cities.ToList();

            // текущая дата
            DateTime date = DateTime.Now;

            // разница в днях между текущей датой и стартовой
            int diff = (int)(startDate - DateTime.Now).TotalDays;

            // генерация файтов поселения
            List<HistoryRegistrationHotel> history = Enumerable.Repeat(0, n)
                                                               .Select(h => new HistoryRegistrationHotel
                                                               {
                                                                   Client           = clients[Utils.GetRand(0, clients.Count)],
                                                                   HotelRoom        = GetRoom(),
                                                                   City             = cities[Utils.GetRand(0, cities.Count)],
                                                                   RegistrationDate = date.AddDays(Utils.GetRand(diff, 0)),
                                                                   Duration         = Utils.GetRand(1, 7)
                                                               })
                                                               .ToList();

            // заполнение таблицы дней недели
            _data.HistoryRegistrationHotel.AddRange(history);

            // запись в базу данных
            _data.SaveChanges();
        }


        // получить свободную комнату
        private HotelRoom GetRoom()
        {
            // комнаты
            List<HotelRoom> list = _data.HotelRooms.ToList();

            // текущая дата
            DateTime date = DateTime.Now;

            // комната
            HotelRoom room;

            do
            {
                room = list[Utils.GetRand(0, list.Count)];
            } while (RoomIsBusy(room, date));

            return room;
        }


        // заполнение таблицы Дни недели						(DaysOfWeek)
        public void FillDaysOfWeekTable()
        {
            // номер дня
            int number = 1;

            // заполнение таблицы дней недели
            _data.DaysOfWeek.AddRange(Utils.DaysOfWeek.Select(c => new Models.DayOfWeek { Name = c, Number = number++ }));

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы График уборки					    (CleaningSchedule)
        public void FillCleaningSchedule()
        {
            // дни недели
            List<Models.DayOfWeek> days = _data.DaysOfWeek.ToList();

            // график уборки
            List<CleaningSchedule> cleanings = _data.CleaningSchedule.ToList();

            // этажи
            List<Floor> floors = _data.Floors.ToList();

            // рабочие
            List<Employee> employees = _data.Employees.ToList();

            // заполнение таблицы по дням
            for (int i = 0, k = 0; i < days.Count; i++, k = 0)
                cleanings.AddRange(Enumerable.Repeat(0, _data.Floors.Count())
                                             .Select(f => new CleaningSchedule
                                             {
                                                 DayOfWeek = days[i],
                                                 Floor = floors[k++],
                                                 Employee = employees[Utils.GetRand(0, employees.Count)]
                                             }));

            // заполнение таблицы графика уборки
            _data.CleaningSchedule.AddRange(cleanings);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы История фактов уборки			    (CleaningHistory)
        public void FillCleaningHistoryTable(DateTime startDate)
        {
            // список графика уборки
            List<CleaningSchedule> schedules = _data.CleaningSchedule.ToList();

            // список фактов уборки
            List<CleaningHistory> history = new List<CleaningHistory>();

            // заполнение списка фактов уборки
            while (startDate <= DateTime.Now)
            {
                // выбрать записи из графика по дню недели м добавить записи по уборке
                schedules.Where(c => startDate.DayOfWeek == (System.DayOfWeek)c.DayOfWeek.Number - 1)
                         .ToList()
                         .ForEach(c => history.Add(new CleaningHistory { Floor = c.Floor, DateCleaning = startDate, Employee = c.Employee }));

                // увеличение даты на день
                startDate = startDate.AddDays(1);
            }

            // заполнение таблицы истрии фактов уборки
            _data.CleaningHistory.AddRange(history);

            // запись в базу данных
            _data.SaveChanges();
        }

        #endregion

        #endregion
    }
}
