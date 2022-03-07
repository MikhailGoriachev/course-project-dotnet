-- создание представлений таблиц для базы данных проекта "Гостиница"

use Hotel
go

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
create view dbo.PersonsView 
as
	select
		Id			
		, Surname		
		, [Name]		
		, Patronymic	
	from
		Persons
go


-- создание представления таблицы Клиенты							(Clients)
create view dbo.ClientsView 
as
	select
		Clients.Id			
		, Surname		
		, [Name]		
		, Patronymic
		, Passport
	from
		Clients inner join Persons on Clients.IdPerson = Persons.Id;
go


-- создание представления таблицы Служащие гостиницы				(Employees)
create view dbo.EmployeesView
as
	select
		Employees.Id			
		, Surname		
		, [Name]		
		, Patronymic
		, Employees.WorkState
	from
		Employees inner join Persons on Employees.IdPerson = Persons.Id;
go


-- создание представления таблицы Типы номеров						(TypesHotelRoom)
create view dbo.TypesHotelRoomView
as
	select
		TypesHotelRoom.Id
		, TypesHotelRoom.[Name]
		, TypesHotelRoom.CountRooms
		, TypesHotelRoom.Price
	from
		TypesHotelRoom;
go


-- создание представления таблицы Этажи								(Floors)
create view dbo.FloorsView
as
	select
		Id
		, Number
	from
		Floors;
go


-- создание представления таблицы Номера гостиницы					(HotelRooms)
create view dbo.HotelRoomsView
as
	select
		HotelRooms.Id
		, TypesHotelRoom.[Name] as TypeRoom
		, TypesHotelRoom.CountRooms
		, TypesHotelRoom.Price
		, Floors.Number as FloorNumber
		, HotelRooms.Number
		, HotelRooms.[State]
	from
		HotelRooms inner join TypesHotelRoom on HotelRooms.IdTypeHotelRoom = TypesHotelRoom.Id
				   inner join Floors		 on HotelRooms.IdFloor		   = Floors.Id;
go


-- создание представления таблицы Города							(Cities)
create view dbo.CitiesView
as
	select
		Id
		, [Name]
	from
		Cities;
go


-- создание представления таблицы История поселений в гостиницу		(HistoryRegistrationHotel)
create view dbo.HistoryRegistrationHotelView
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
	from
		HistoryRegistrationHotel inner join ClientsView on HistoryRegistrationHotel.IdClient = ClientsView.Id
								 inner join HotelRoomsView on HistoryRegistrationHotel.IdHotelRoom = HotelRoomsView.Id
								 inner join CitiesView on HistoryRegistrationHotel.IdCity = CitiesView.Id;
go


-- создание представления таблицы История фактов уборки				(CleaningHistory)
create view dbo.CleaningHistoryView
as
	select
		CleaningHistory.Id
		, FloorsView.Number
		, DateCleaning
		, EmployeesView.Surname + N' ' + substring(EmployeesView.[Name], 1, 1) + N'. ' + substring(EmployeesView.Patronymic, 1, 1) + N'.' as Employee
	from
		CleaningHistory inner join FloorsView on CleaningHistory.IdFloor = FloorsView.Id
						inner join EmployeesView on CleaningHistory.IdEmployee = EmployeesView.Id;
go


-- создание представления таблицы Дни недели						(DaysOfWeek)
create view dbo.DaysOfWeekView
as
	select
		DaysOfWeek.Id
		, [Name]
		, Number
	from
		DaysOfWeek;
go


-- создание представления таблицы График уборки						(CleaningSchedule)
create view dbo.CleaningScheduleView
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
