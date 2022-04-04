-- создание представлений таблиц для базы данных проекта "Гостиница"

--use Hotel
--go

-- удаление представлений
drop view if exists CleaningScheduleView				-- представление таблицы График уборки						(CleaningSchedule)			
drop view if exists DaysOfWeekView						-- представление таблицы Дни недели							(DaysOfWeek)
drop view if exists CleaningHistoryView					-- представление таблицы История фактов уборки				(CleaningHistory)
drop view if exists HistoryRegistrationHotelView		-- представление таблицы История поселений в гостиницу		(HistoryRegistrationHotel)			
drop view if exists CitiesView							-- представление таблицы Города								(Cities)
drop view if exists HotelRoomsView						-- представление таблицы Номера гостиницы					(HotelRooms)
drop view if exists TypesHotelRoomView					-- представление таблицы Типы номеров						(TypesHotelRoom)
drop view if exists FloorsView							-- представление таблицы Этажи								(Floors)
drop view if exists EmployeesView						-- представление таблицы Служащие гостиницы					(Employees)
drop view if exists ClientsView							-- представление таблицы Клиенты							(Clients)
drop view if exists PersonsView							-- представление таблицы Персоны							(Persons)
go


-- создание представления таблицы Персоны							(Persons)
create or alter view dbo.PersonsView 
as
	select
		Id			
		, Surname		
		, [Name]		
		, Patronymic	
		, IsDeleted
	from
		Persons
go


-- создание представления таблицы Клиенты							(Clients)
create or alter view dbo.ClientsView 
as
	select
		Clients.Id			
		, Surname		
		, [Name]		
		, Patronymic
		, Passport
		, Clients.IsDeleted
	from
		Clients inner join Persons on Clients.IdPerson = Persons.Id;
go


-- создание представления таблицы Служащие гостиницы				(Employees)
create or alter view dbo.EmployeesView
as
	select
		Employees.Id			
		, Surname		
		, [Name]		
		, Patronymic
		, Employees.IsDeleted
	from
		Employees inner join Persons on Employees.IdPerson = Persons.Id;
go


-- создание представления таблицы Типы номеров						(TypesHotelRoom)
create or alter view dbo.TypesHotelRoomView
as
	select
		TypesHotelRoom.Id
		, TypesHotelRoom.[Name]
		, TypesHotelRoom.CountRooms
		, TypesHotelRoom.Price
		, TypesHotelRoom.IsDeleted
	from
		TypesHotelRoom;
go


-- создание представления таблицы Этажи								(Floors)
create or alter view dbo.FloorsView
as
	select
		Id
		, Number
		, IsDeleted
	from
		Floors;
go


-- создание представления таблицы Номера гостиницы					(HotelRooms)
create or alter view dbo.HotelRoomsView
as
	select
		HotelRooms.Id
		, TypesHotelRoom.[Name] as TypeRoom
		, TypesHotelRoom.CountRooms
		, TypesHotelRoom.Price
		, Floors.Number as FloorNumber
		, HotelRooms.Number
		, Convert(Bit, IIF(IsNull(reg.Id, 0) = 0, 0, 1)) as [State]
	from
		HotelRooms inner join TypesHotelRoom on HotelRooms.IdTypeHotelRoom = TypesHotelRoom.Id
				   inner join Floors		 on HotelRooms.IdFloor		   = Floors.Id
				   left join  (select * from HistoryRegistrationHotel
								where convert(date, getdate()) between HistoryRegistrationHotel.RegistrationDate 
												and DATEADD(day, HistoryRegistrationHotel.Duration, HistoryRegistrationHotel.RegistrationDate)) as reg
							on reg.IdHotelRoom = HotelRooms.Id;
go


-- создание представления таблицы Города							(Cities)
create or alter view dbo.CitiesView
as
	select
		Id
		, [Name]
		, IsDeleted
	from
		Cities;
go


-- создание представления таблицы История поселений в гостиницу		(HistoryRegistrationHotel)
create or alter view dbo.HistoryRegistrationHotelView
as
	select
		HistoryRegistrationHotel.Id
		, ClientsView.Surname + N' ' + substring(ClientsView.[Name], 1, 1) + N'. ' + substring(ClientsView.Patronymic, 1, 1) + N'.' as Client
		, ClientsView.Passport
		, HotelRoomsView.FloorNumber
		, HotelRoomsView.Number as RoomNumber
		, CitiesView.[Name] as City
		, HistoryRegistrationHotel.RegistrationDate
		, HistoryRegistrationHotel.Duration
		, HistoryRegistrationHotel.IsDeleted
	from
		HistoryRegistrationHotel inner join ClientsView on HistoryRegistrationHotel.IdClient = ClientsView.Id
								 inner join HotelRoomsView on HistoryRegistrationHotel.IdHotelRoom = HotelRoomsView.Id
								 inner join CitiesView on HistoryRegistrationHotel.IdCity = CitiesView.Id;
go


-- создание представления таблицы История фактов уборки				(CleaningHistory)
create or alter view dbo.CleaningHistoryView
as
	select
		CleaningHistory.Id
		, FloorsView.Number
		, DateCleaning
		, EmployeesView.Surname + N' ' + substring(EmployeesView.[Name], 1, 1) + N'. ' + substring(EmployeesView.Patronymic, 1, 1) + N'.' as Employee
		, CleaningHistory.IsDeleted
	from
		CleaningHistory inner join FloorsView on CleaningHistory.IdFloor = FloorsView.Id
						inner join EmployeesView on CleaningHistory.IdEmployee = EmployeesView.Id;
go


-- создание представления таблицы Дни недели						(DaysOfWeek)
create or alter view dbo.DaysOfWeekView
as
	select
		Id
		, [Name]
		, Number
	from
		DaysOfWeek;
go


-- создание представления таблицы График уборки						(CleaningSchedule)
create or alter view dbo.CleaningScheduleView
as
	select
		CleaningSchedule.Id
		, DaysOfWeekView.[Name] as [DayOfWeek]
		, EmployeesView.Surname + N' ' + substring(EmployeesView.[Name], 1, 1) + N'. ' + substring(EmployeesView.Patronymic, 1, 1) + N'.' as Employee
		, FloorsView.Number
	from
		CleaningSchedule inner join DaysOfWeekView on CleaningSchedule.IdDayOfWeek = DaysOfWeekView.Id
						 inner join EmployeesView on CleaningSchedule.IdEmployee = EmployeesView.Id
						 inner join FloorsView on CleaningSchedule.IdFloor = FloorsView.Id;
go
