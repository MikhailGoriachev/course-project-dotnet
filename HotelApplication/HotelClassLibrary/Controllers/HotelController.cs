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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace HotelClassLibrary.Controllers
{
    // Класс Контроллер заполнения таблиц базы данных тестовыми данными
    public static class HotelController
    {
        // модель базы данных
        private static HotelDB _data;

        public static HotelDB Data
        {
            get => _data;
            set => _data = value;
        }


        #region Конструкторы

        // статический конструктор
        static HotelController()
        {
            // установка значений
            _data = new HotelDB();

            _data.Configuration.LazyLoadingEnabled = true;
        }

        #endregion

        #region Методы

        // добавление клиента
        public static void AddClient(Client client)
        {
            // установка статуса добавления
            _data.Entry(client).State = EntityState.Added;

            // добавить клиента
            _data.Clients.AddOrUpdate(client);

            // сохранить в базу данных
            _data.SaveChanges();
        }


        // удаление клиента
        public static void RemoveClient(Client client)
        {
            // установка статуса удаления клиента
            client.IsDeleted = true;

            // сохранить в базу данных
            _data.SaveChanges();
        }


        // добавление города
        public static void AddCity(City city)
        {
            // установка статуса добавления
            _data.Entry(city).State = EntityState.Added;

            // добавить город
            _data.Cities.AddOrUpdate(city);

            // сохранить в базу данных
            _data.SaveChanges();
        }


        // удаление города
        public static void RemoveCity(City city)
        {
            // установка статуса удаления клиента
            city.IsDeleted = true;

            // сохранить в базу данных
            _data.SaveChanges();
        }


        // получить статус номера - свободен или занят (занято - true) в заданную дату
        public static bool RoomIsBusy(HotelRoom room, DateTime date, List<HistoryRegistrationHotel> history = null)
        {
            // текущая дата
            //date = DateTime.Now.Date;

            var result = (history ?? GetHistoryRegistrationHotel()).FirstOrDefault(h => h.Duration != 0 && date.Date >= h.RegistrationDate.Date 
                                                                    && date.Date < h.RegistrationDate.AddDays(h.Duration).Date
                                                                    && h.HotelRoom.Id == room.Id);

            return result != null;
        }

        // получить количество занятых мест номера - в заданную дату
        public static int CountBusyPlace(HotelRoom room, DateTime date) => GetHistoryRegistrationHotel()
                            .Where(h => h.Duration != 0 && date.Date >= h.RegistrationDate.Date && date.Date < h.RegistrationDate.AddDays(h.Duration).Date
                                    && h.HotelRoom.Id == room.Id)
                            .Count();


        // операция над записью таблицы
        public static void ChangeEntity(DbEntityEntry entity, EntityState state, DbSet dbSet)
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


        // сохранить изменения в базе данных
        public static void SaveChanges()
        {
            _data.SaveChanges();
        }


        #region Операции администратора

        // Администратор должен иметь возможность выполнить следующие операции:
        // •	принять на работу или уволить служащего гостиницы
        // •	изменить расписание работы служащего
        // •	поселить или выселить клиента.
        // •	автоматической выдачи клиенту счета за проживание в гостинице.
        // •	отчета о работе гостиницы за указанный квартал текущего года. Такой отчет должен 
        //      содержать следующие сведения: число клиентов за указанный период, сколько дней был занят 
        //      и свободен каждый из номеров гостиницы, общая сумма дохода.


        // принять на работу 
        public static void AddEmployee(Employee employee)
        {
            // установка статуса добавления
            _data.Entry(employee).State = EntityState.Added;

            // добавить служащего
            _data.Employees.AddOrUpdate(employee);

            // сохранить в базу данных
            _data.SaveChanges();

            // корректировка графика уборки
            CorrectCleaningSchedule();
        }

        // уволить служащего гостиницы
        public static void RemoveEmployee(Employee employee)
        {
            // уволить служащего
            employee.IsDeleted = true;

            // сохранить в базу данных
            _data.SaveChanges();

            // корректировка графика уборки
            CorrectCleaningSchedule();
        }


        #region Изменить расписание уборки

        // получить график работника
        public static List<CleaningSchedule> GetSheduleEmployee(Employee employee)
        {
            // получить записи из графика уборки по текущему работнику
            List<CleaningSchedule> cleaningSchedule = GetCleaningSchedule().Where(c => c.Employee.Id == employee.Id).ToList();

            return cleaningSchedule;
        }
        
        
        // установить график работника
        public static void SetSheduleEmployee(Employee employee, List<(int day, int floor)> shedule)
        {
            // получить записи из графика уборки по текущему работнику
            List<CleaningSchedule> cleaningSchedule = GetCleaningSchedule().Where(c => c.Employee.Id == employee.Id).ToList();

            // получить записи которых нет в новом графике
            List<CleaningSchedule> deletedSchedule = cleaningSchedule.Where(c => shedule.IndexOf((c.DayOfWeek.Number, c.Floor.Number)) == -1).ToList();

            // дни недели 
            List<Models.DayOfWeek> days = GetDaysOfWeek();

            // этажи
            List<Floor> floors = GetFloors();

            // получить записи которых есть в новом графике, но нет в старом
            cleaningSchedule = shedule.Where(s => cleaningSchedule.Find(c => (c.DayOfWeek.Number, c.Floor.Number) == (s.day, s.floor)) == null)
                                      .Select(s => new CleaningSchedule
                                      {
                                          DayOfWeek = days.First(d => d.Number == s.day),
                                          Employee = employee,
                                          Floor = floors.First(f => f.Number == s.floor)
                                      }).ToList();

            // изменение расписания
            ChangeSchedule(cleaningSchedule, deletedSchedule);

            // корректировка графика уборки
            CorrectCleaningSchedule(employee);
        }

        // изменить расписание уборки
        // параметры: addDays - новые дни работы, deleteDays - удаляемые дни работы
        public static void ChangeSchedule(List<CleaningSchedule> addDays, List<CleaningSchedule> deleteDays)
        {
            // получить записи, которые нужно удалить
            deleteDays.AddRange(GetCleaningSchedule().Where(c => addDays.FirstOrDefault(d => (d.DayOfWeek.Number, d.Floor.Number) == (c.DayOfWeek.Number, c.Floor.Number)) != null));

            // добавление записей
            addDays.ForEach(d => _data.Entry(d).State = EntityState.Added);
            _data.CleaningSchedule.AddRange(addDays);
            _data.SaveChanges();

            // удаление записей
            deleteDays.ForEach(d => _data.Entry(d).State = EntityState.Deleted);
            _data.CleaningSchedule.RemoveRange(deleteDays);
            _data.SaveChanges();
        }


        // корректировка графика уборки
        public static void CorrectCleaningSchedule(Employee exceptionEmployee = null)
        {
            // список полученных записей
            List<CleaningSchedule> schedule = GetCleaningSchedule();

            // удаление записей уволенных работников
            ChangeSchedule(new List<CleaningSchedule>(), schedule.Where(s => s.Employee.IsDeleted).ToList());

            // цикл перебора дней недели
            foreach (var day in _data.DaysOfWeek)
            {
                // цикл перебора этажей
                foreach (var floor in _data.Floors)
                {
                    // поиск записей для этого этажа и дня недели
                    schedule = _data.CleaningSchedule.Where(c => c.DayOfWeek.Id == day.Id && c.Floor.Id == floor.Id)
                                                 .ToList();

                    // если список пуст - установить работника на этот день, 
                    // у которго меньше всего рабочих дней
                    if (schedule.Count == 0)
                    {
                        CleaningSchedule c = new CleaningSchedule {
                            Employee = GetMinEmployeeEmptyDay(day.Number, exceptionEmployee),
                            DayOfWeek = day,
                            Floor = floor
                        };

                        _data.Entry(c).State = EntityState.Added;
                        _data.CleaningSchedule.Add(c);
                    }

                    // найдено записей больше 1, то все записи кроме последней
                    if (schedule.Count > 1)
                    {
                        // удаление первой записи из списка
                        schedule.Remove(schedule[0]);

                        // удаление оставшихся записей из базы данных
                        schedule.ForEach(c => _data.Entry(c).State = EntityState.Deleted);
                        _data.CleaningSchedule.RemoveRange(schedule);
                    }
                }
            }

            // сохранение в базу данных
            _data.SaveChanges();
        }


        // получить работника, у которого меньше всего рабочих дней, по указанному дню
        public static Employee GetMinEmployeeEmptyDay(int day, Employee exceptionEmployee)
        {
            // список работников, которые не работают в этот день
            List<Employee> employees = GetEmployeesListEmptyDay(day);

            // удаление из списка работника, который указан, как исключенный 
            employees.Remove(exceptionEmployee);

            // получить коллекцию работников с количеством смен
            var em = employees.Where(e => e.IsDeleted == false)
                            .Select(e => new { Employee = e, Count = _data.CleaningSchedule.Count(c => c.Employee.Id == e.Id) })
                            .ToList();

            

            // минимальное значение количества смен
            int min = em.Min(e => e.Count);

            return em.First(e => e.Count == min).Employee;
        }


        // получить работника, у которого меньше всего рабочих дней, по указанному дню
        public static Employee GetMinEmployee()
        {
            // получить коллекцию работников с количеством смен
            var em = _data.Employees.Where(e => e.IsDeleted == false)
                            .Select(e => new { Employee = e, Count = _data.CleaningSchedule.Count(c => c.Employee.Id == e.Id) })
                            .ToList();

            

            // минимальное значение количества смен
            int min = em.Min(e => e.Count);

            return em.First(e => e.Count == min).Employee;
        }

        #endregion


        // разместить клиента
        public static bool PlaceClient(Client client, HotelRoom room, City city, int Duration)
        {
            // если номер заполнен
            if (CountBusyPlace(room, DateTime.Now) == room.TypeHotelRoom.CountPlace)
                return false;

            // запись регистрации
            HistoryRegistrationHotel registration = new HistoryRegistrationHotel {
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

        // разместить клиента
        public static bool PlaceClient(HistoryRegistrationHotel registration)
        {
            // если номер заполнен
            if (CountBusyPlace(registration.HotelRoom, DateTime.Now) == registration.HotelRoom.TypeHotelRoom.CountPlace)
                return false;

            // запись регистрации
            //HistoryRegistrationHotel registration = new HistoryRegistrationHotel {
            //    Client = client,
            //    HotelRoom = room,
            //    Duration = Duration,
            //    City = city
            //};

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
        public static bool EvictClient(Client client, HotelRoom room = null)
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
        public static int GetAccount(Client client, HotelRoom room = null, DateTime dateStart = new DateTime())
        {
            // поиск записей регистрации по данному колиенту и дате
            List<HistoryRegistrationHotel> histories = GetHistoryRegistrationHotel();

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
        public static  Report GetReport(DateTime begin, DateTime end) =>
            new Report(GetHotelRoomsAsync().Result, GetHistoryRegistrationHotelAsync().Result, begin, end);


        #endregion

        #region Запросы

        // 1.	О клиентах, проживающих в заданном номере
        public static  List<Client> Proc1(int roomNum)
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
        public static  List<Client> Proc2(string city) => GetHistoryRegistrationHotelAsync().Result.Where(h => h.City.Name == city)
                                                                                           .Select(h => h.Client)
                                                                                           .Distinct()
                                                                                           .ToList();


        // 3.	О том, кто из служащих убирал номер указанного клиента в заданный день недели
        public static  List<Employee> Proc3(string passport, DateTime date)
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
        public static List<HotelRoom> Proc4()
        {
            // текущая дата
            DateTime date = DateTime.Now;

            return GetHotelRooms().Where(h => !RoomIsBusy(h, date)).ToList();
        }


        #endregion


        #region Получение данных из таблиц


        // получить данные из таблицы График уборки				        (CleaningSchedule)
        public static async Task<List<CleaningSchedule>> GetCleaningScheduleAsync() => await _data.CleaningSchedule.Include(c => c.Employee)
                                                                                                            .Include(c => c.Employee.Person)
                                                                                                            .Include(c => c.Floor)
                                                                                                            .Include(c => c.DayOfWeek)
                                                                                                            .ToListAsync();

        public static List<CleaningSchedule> GetCleaningSchedule() => _data.CleaningSchedule.Include(c => c.Employee)
                                                                                            .Include(c => c.Employee.Person)
                                                                                            .Include(c => c.Floor)
                                                                                            .Include(c => c.DayOfWeek)
                                                                                            .ToList();

        // получение списка привязки
        public static  BindingList<CleaningSchedule> GetCleaningScheduleBindingList()
        {
            // загрузка данных
            _data.CleaningSchedule.Include(c => c.Employee)
                                  .Include(c => c.Employee.Person)
                                  .Include(c => c.Floor)
                                  .Include(c => c.DayOfWeek)
                                  .Load();

            // получаение списка привязки
            return _data.CleaningSchedule.Local.ToBindingList();
        }


        // получить данные из таблицы Дни недели					    (DaysOfWeek)
        public static async Task<List<Models.DayOfWeek>> GetDaysOfWeekAsync() => await _data.DaysOfWeek.ToListAsync();
        public static List<Models.DayOfWeek> GetDaysOfWeek() =>_data.DaysOfWeek.ToList();

        // получение списка привязки
        public static  BindingList<Models.DayOfWeek> GetDaysOfWeekBindingList()
        {
            // загрузка данных
            _data.DaysOfWeek.Load();

            // получаение списка привязки
            return _data.DaysOfWeek.Local.ToBindingList();
        }


        // получить данные из таблицы История фактов уборки		        (CleaningHistory)
        public static async Task<List<CleaningHistory>> GetCleaningHistoryAsync() => await _data.CleaningHistory.Include(h => h.Employee)
                                                                                                         .Include(c => c.Employee.Person)
                                                                                                         .Include(h => h.Floor)
                                                                                                         .ToListAsync();

        public static  List<CleaningHistory> GetCleaningHistory() => _data.CleaningHistory.Include(h => h.Employee)
                                                                                          .Include(c => c.Employee.Person)
                                                                                          .Include(h => h.Floor)
                                                                                          .ToList();

        // получение списка привязки
        public static  BindingList<CleaningHistory> GetCleaningHistoryBindingList()
        {
            // загрузка данных
            _data.CleaningHistory.Include(h => h.Employee)
                                 .Include(c => c.Employee.Person)
                                 .Include(h => h.Floor)
                                 .Load();

            // получаение списка привязки
            return _data.CleaningHistory.Local.ToBindingList();
        }


        // получить данные из таблицы История поселений в гостиницу     (HistoryRegistrationHotel)
        public static async Task<List<HistoryRegistrationHotel>> GetHistoryRegistrationHotelAsync() => await _data.HistoryRegistrationHotel.Include(r => r.Client)
                                                                                                                                           .Include(r => r.Client.Person)
                                                                                                                                           .Include(r => r.City)
                                                                                                                                           .Include(r => r.HotelRoom)
                                                                                                                                           .Include(r => r.HotelRoom.TypeHotelRoom)
                                                                                                                                           .Include(r => r.HotelRoom.Floor)
                                                                                                                                           .ToListAsync();

        public static List<HistoryRegistrationHotel> GetHistoryRegistrationHotel() => _data.HistoryRegistrationHotel.Include(r => r.Client)
                                                                                                                    .Include(r => r.Client.Person)
                                                                                                                    .Include(r => r.City)
                                                                                                                    .Include(r => r.HotelRoom)
                                                                                                                    .Include(r => r.HotelRoom.TypeHotelRoom)
                                                                                                                    .Include(r => r.HotelRoom.Floor)
                                                                                                                    .ToList();


        // получение списка привязки
        public static  BindingList<HistoryRegistrationHotel> GetHistoryRegistrationHotelBindingList()
        {
            // загрузка данных
            _data.HistoryRegistrationHotel.Include(r => r.Client)
                                          .Include(r => r.Client.Person)
                                          .Include(r => r.City)
                                          .Include(r => r.HotelRoom)
                                          .Include(r => r.HotelRoom.TypeHotelRoom)
                                          .Include(r => r.HotelRoom.Floor)
                                          .Load();

            // получаение списка привязки
            return _data.HistoryRegistrationHotel.Local.ToBindingList();
        }


        // получение списка привязки
        public static ObservableCollection<HistoryRegistrationHotel> GetHistoryRegistrationHotelObservable()
        {
            // загрузка данных
            _data.HistoryRegistrationHotel.Include(r => r.Client)
                                          .Include(r => r.Client.Person)
                                          .Include(r => r.City)
                                          .Include(r => r.HotelRoom)
                                          .Include(r => r.HotelRoom.TypeHotelRoom)
                                          .Include(r => r.HotelRoom.Floor)
                                          .Load();

            // получаение списка привязки
            return _data.HistoryRegistrationHotel.Local;
        }


        // получение списка привязки записей, которые относятся к текущему времени
        public static List<HistoryRegistrationHotel> GetCurrentHistoryRegistrationHotel()
        {
            // текущая дата 
            DateTime date = DateTime.Now.Date;

            // загрузка данных
            _data.HistoryRegistrationHotel.Include(r => r.Client)
                                          .Include(r => r.Client.Person)
                                          .Include(r => r.City)
                                          .Include(r => r.HotelRoom)
                                          .Include(r => r.HotelRoom.TypeHotelRoom)
                                          .Include(r => r.HotelRoom.Floor)
                                          .Load();

            var list = _data.HistoryRegistrationHotel.Local;

            // получаение списка привязки
            return list.Where(h => h.Duration != 0 && date >= h.RegistrationDate.Date && date < h.RegistrationDate.AddDays(h.Duration).Date).ToList();
            //return _data.HistoryRegistrationHotel.Local.Where(h => h.RegistrationDate.Date >= date && h.RegistrationDate.AddDays(h.Duration).Date <= date).ToList();
        }


        // получить данные из таблицы Города					        (Cities)
        public static  async Task<List<City>> GetCitiesAsync() => await _data.Cities.ToListAsync();

        // получение списка привязки
        public static  BindingList<City> GetCitiesBindingList()
        {
            // загрузка данных
            _data.Cities.Load();

            // получаение списка привязки
            return _data.Cities.Local.ToBindingList();
        }



        // получить данные из таблицы Номера гостиницы					(HotelRooms)
        public static async Task<List<HotelRoom>> GetHotelRoomsAsync() => await _data.HotelRooms.Include(r => r.TypeHotelRoom)
                                                                                                .Include(r => r.Floor)
                                                                                                .ToListAsync();

        public static  List<HotelRoom> GetHotelRooms() => _data.HotelRooms.Include(r => r.TypeHotelRoom)
                                                                          .Include(r => r.Floor)
                                                                          .ToList();

        // получение списка привязки
        public static  BindingList<HotelRoom> GetHotelRoomsBindingList()
        {
            // загрузка данных
            _data.HotelRooms.Include(r => r.TypeHotelRoom)
                            .Include(r => r.Floor)
                            .Load();

            // получаение списка привязки
            return _data.HotelRooms.Local.ToBindingList();
        }



        // получить данные из таблицы Типы номеров						(TypesHotelRoom)
        public static  async Task<List<TypeHotelRoom>> GetTypesHotelRoomAsync() => await _data.TypesHotelRoom.ToListAsync();

        // получение списка привязки
        public static  BindingList<TypeHotelRoom> GetTypesHotelRoomBindingList()
        {
            // загрузка данных
            _data.TypesHotelRoom.Load();

            // получаение списка привязки
            return _data.TypesHotelRoom.Local.ToBindingList();
        }



        // получить данные из таблицы Этажи						        (Floors)
        public static async Task<List<Floor>> GetFloorsAsync() => await _data.Floors.ToListAsync();
        public static List<Floor> GetFloors() => _data.Floors.ToList();

        // получение списка привязки
        public static BindingList<Floor> GetFloorsBindingList()
        {
            // загрузка данных
            _data.Floors.Load();

            // получаение списка привязки
            return _data.Floors.Local.ToBindingList();
        }


        // получить данные из таблицы Служащие гостиницы				(Employees)
        public static  async Task<List<Employee>> GetEmployeesAsync() => await _data.Employees.Include(e => e.Person).ToListAsync();
        public static  List<Employee> GetEmployees() => _data.Employees.Include(e => e.Person).ToList();

        // получение списка привязки
        //public static  BindingList<Employee> GetEmployeesBindingList() 
        //{ 
        //    // загрузка данных
        //    _data.Employees.Include(e => e.Person).Load();

        //    var list = _data.Employees.Local.ToBindingList();

        //    // получаение списка привязки
        //    return list;
        //} 

        // получение списка привязки
        public static BindingList<Employee> GetEmployeesBindingList()
        {
            // загрузка данных
            _data.Employees.Include(e => e.Person).Load();

            return _data.Employees.Local.ToBindingList();
        }


        // получить данные из таблицы Клиенты							(Clients)
        public static async Task<List<Client>> GetClientsAsync() => await _data.Clients.Include(c => c.Person).ToListAsync();
        public static List<Client> GetClients() => _data.Clients.Include(c => c.Person).ToList();

        // получение списка привязки
        public static BindingList<Client> GetClientsBindingList()
        {
            // загрузка данных
            _data.Clients.Include(c => c.Person).Load();

            // получаение списка привязки
            return _data.Clients.Local.ToBindingList();
        }

        // получение списка привязки клиентов, которые проживают на данный момент в гостиннице
        public static List<Client> GetCurrentClients()
        {
            // загрузка данных
            _data.Clients.Include(c => c.Person).Load();

            // данные о действующих регистрациях
            var reg = GetCurrentHistoryRegistrationHotel();

            // получаение списка привязки
            return reg.Count == 0 ? new List<Client>() : _data.Clients.Local.Where(c => reg.FirstOrDefault(r => r.Id == c.Id) != null).ToList();
        }


        // получить данные из таблицы Персоны							(Persons)
        public static async Task<List<Person>> GetPersonsAsync() => await _data.Persons.ToListAsync();

        // получение списка привязки
        public static BindingList<Person> GetPersonsBindingList()
        {
            // загрузка данных
            _data.Persons.Load();

            // получаение списка привязки
            return _data.Persons.Local.ToBindingList();
        }

        #endregion


        #region Заполнение базы данных

        // генерация данных и заполнение таблиц базы данных 
        public static async Task FillDataBase(DateTime startDate) =>
            await Task.Run(() =>
            {
                // очистка таблиц
                ClearTables();

                // заполнение таблицы Клиенты							(Clients)
                FillClientsTable(Utils.GetRand(300, 500));

                // заполнение таблицы Служащие гостиницы				(Employees)
                FillEmployeesTable(Utils.GetRand(5, 8));

                // заполнение таблицы Типы номеров					    (TypesHotelRoom)
                FillTypesHotelRoomTable();

                // заполнение таблицы Этажи                             (Floors)
                FillFloorsTable(Utils.GetRand(3, 6));

                // заполнение таблицы Номера гостиницы				    (HotelRooms)
                FillHotelRoomsTable(Utils.GetRand(5, 11) * 10);

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
        public static void ClearTables()
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
        public static void RemoveRangeTable<T>(DbSet<T> dbSet) where T: class 
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
        public static void FillPersonsTable(int n = 40)
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
        public static void FillClientsTable(int n = 15)
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
        public static void FillEmployeesTable(int n = 15)
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
        public static void FillTypesHotelRoomTable()
        {
            // добавление записей в таблицу
            _data.TypesHotelRoom.AddRange(Utils.TypesHotelRoom);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Этажи                             (Floors)
        public static  void FillFloorsTable(int countFloors = 4)
        {
            // номер этажа
            int number = 1;

            // добавление записей в таблицу
            _data.Floors.AddRange(Enumerable.Repeat(0, countFloors).Select(f => new Floor { Number = number++ }).ToList());

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Номера гостиницы				    (HotelRooms)
        public static void FillHotelRoomsTable(int countRooms)
        {
            // гостиничные номера
            List<HotelRoom> rooms = new List<HotelRoom>();

            // список этажей
            List<Floor> floors = _data.Floors.ToList();

            // типы номеров
            List<TypeHotelRoom> types = _data.TypesHotelRoom.ToList();

            // среднее количество номеров на этаж
            int avg = countRooms / floors.Count;

            // количество номеров на последнем этаже
            int last = countRooms - avg * (floors.Count - 1);

            int floorsCount = floors.Count;

            // генерация номеров
            for (int i = 0; i < floorsCount; i++)
            {
                // этаж
                Floor floor = floors[i];

                for (int k = 1, number; i < floorsCount - 1 && k <= avg || i == floorsCount - 1 && k % last != 0; k++)
                {
                    // номер комнаты
                    number = (i + 1) * 100 + k;

                    // добавление комнаты
                    rooms.Add(new HotelRoom
                    {
                        Floor = floor,
                        Number = number,
                        TypeHotelRoom = types[Utils.GetRand(0, types.Count)],
                        PhoneNumber = (80000 + number++).ToString()
                    });
                }
            }

            // добавление номеров в таблицу
            _data.HotelRooms.AddRange(rooms);

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы Города							(Cities)
        public static  void FillCitiesTable()
        {
            // заполнение таблицы городов
            _data.Cities.AddRange(Utils.Cities.Select(c => new City { Name = c }));

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы История поселений в гостиницу	    (HistoryRegistrationHotel)
        public static  void FillHistoryRegistrationHotelTable(DateTime startDate, int n = 80)
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
        private static HotelRoom GetRoom()
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
        public static void FillDaysOfWeekTable()
        {
            // номер дня
            int number = 1;

            // заполнение таблицы дней недели
            _data.DaysOfWeek.AddRange(Utils.DaysOfWeek.Select(c => new Models.DayOfWeek { Name = c, Number = number++ }));

            // запись в базу данных
            _data.SaveChanges();
        }


        // заполнение таблицы График уборки					    (CleaningSchedule)
        public static  void FillCleaningSchedule()
        {
            // дни недели
            List<Models.DayOfWeek> days = _data.DaysOfWeek.ToList();

            // график уборки
            List<CleaningSchedule> cleanings = _data.CleaningSchedule.ToList();

            // этажи
            List<Floor> floors = _data.Floors.ToList();

            // заполнение таблицы по дням
            //for (int i = 0, k = 0; i < days.Count; i++, k = 0)
            //    cleanings.AddRange(Enumerable.Repeat(0, _data.Floors.Count())
            //                                 .Select(f => new CleaningSchedule
            //                                 {
            //                                     DayOfWeek = days[i],
            //                                     Floor = floors[k++],
            //                                     Employee = GetEmployeeEmptyDay(i)
            //                                 }));

            // количество этажей
            int count = _data.Floors.Count();

            for (int i = 0, k = 0; i < days.Count; i++, k = 0)
            {
                for (; k < count; k++)
                {
                    // заполнение таблицы графика уборки
                    _data.CleaningSchedule.Add(new CleaningSchedule
                    {
                        DayOfWeek = days[i],
                        Floor = floors[k],
                        Employee = GetEmployeeEmptyDay(days[i].Number)
                    });

                    // запись в базу данных
                    _data.SaveChanges();
                }
            }

            // заполнение таблицы графика уборки
            //_data.CleaningSchedule.AddRange(cleanings);

            // запись в базу данных
            _data.SaveChanges();
        }


        // получить коллекцию рабочих, которые не работают в выбранный день
        public static List<Employee> GetEmployeesListEmptyDay(int day) => 
            _data.Employees.Where(e => !e.IsDeleted)
                           .ToArray()
                           .Where(e => _data.CleaningSchedule.Where(c => c.Employee.Id == e.Id && c.DayOfWeek.Number == day)
                                                             .ToArray().Length == 0)
                           .ToList();
        

        // получить рабочего, который не работает в выбранный день
        public static Employee GetEmployeeEmptyDay(int day)
        {
            List<Employee> employees = GetEmployeesListEmptyDay(day);

            return employees[Utils.GetRand(0, employees.Count)];
        }


        // заполнение таблицы История фактов уборки			    (CleaningHistory)
        public static  void FillCleaningHistoryTable(DateTime startDate)
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
