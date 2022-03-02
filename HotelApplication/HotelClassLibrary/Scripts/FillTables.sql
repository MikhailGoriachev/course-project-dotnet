-- заполнение таблиц для базы данных проекта "Гостиница"

-- удаление таблиц
drop table if exists CleaningSchedule				-- таблица Персоны							(Persons)
drop table if exists DaysOfWeek						-- таблица Клиенты							(Clients)
drop table if exists CleaningHistory				-- таблица Служащие гостиницы				(Employees)
drop table if exists HistoryRegistrationHotel		-- таблица Типы номеров						(TypesHotelRoom)
drop table if exists Cities							-- таблица Номера гостиницы					(HotelRooms)
drop table if exists HotelRooms						-- таблица Города							(Cities)
drop table if exists TypesHotelRoom					-- таблица История поселений в гостиницу	(HistoryRegistrationHotel)
drop table if exists Employees						-- таблица История фактов уборки			(CleaningHistory)
drop table if exists Clients						-- таблица Дни недели						(DaysOfWeek)
drop table if exists Persons						-- таблица График уборки					(CleaningSchedule)
go

-- заполнение таблицы Персоны							(Persons)
create table Persons (
	Id			int					not null	primary key identity,
	Surname		nvarchar(60)		not null,	-- Фамилия
	[Name]		nvarchar(40)		not null,	-- Имя
	Patronymic	nvarchar(60)		not null	-- Отчество
);
go


-- заполнение таблицы Клиенты							(Clients)
create table Clients (
	Id			int					not null	primary key identity,
	IdPerson	int					not null	foreign key references Persons(Id),	-- Персона
	Passport	nvarchar(10)		not null	-- Номер паспорта
);
go


-- заполнение таблицы Служащие гостиницы				(Employees)
create table Employees (
	Id			int					not null	primary key identity,
	IdPerson	int					not null	foreign key references Persons(Id),	-- Персона
	WorkState	bit					not null	-- Статус работы: уволен/работает
);
go

-- заполнение таблицы Типы номеров					(TypesHotelRoom)
create table TypesHotelRoom (
	Id			int					not null	primary key identity,
	[Name]		nvarchar(40)		not null,	-- Название типа
	CountRooms	int					not null	-- Количество комнат
);


-- заполнение таблицы Номера гостиницы				(HotelRooms)
create table HotelRooms (
	Id					int			not null	primary key identity,
	IdTypeHotelRoom		int			not null	foreign key references TypesHotelRoom(Id),		-- Тип номера
	Number				int			not null,	-- Номер гостиничного номера
	[Level]				int			not null,	-- Этаж		
	[State]				bit			not null	-- Статус (занят/свободен)
);
go


-- заполнение таблицы Города							(Cities)
create table Cities (
	Id					int				not null	primary key identity,
	[Name]				nvarchar(60)	not null	-- Наименование города
);
go


-- заполнение таблицы История поселений в гостиницу	(HistoryRegistrationHotel)
create table HistoryRegistrationHotel (
	Id					int			not null	primary key identity,
	IdClient			int			not null	foreign key references Clients(Id),		-- Клиент
	IdHotelRoom			int			not null	foreign key references HotelRooms(Id),	-- Гостиничный номер
	IdCity				int			not null	foreign key references Cities(Id),		-- Город, из которого прибыл клиент
	RegistrationDate	date		not null,	-- Дата поселения
	Duration			int			not null	-- Длительность проживания 
);
go


-- заполнение таблицы История фактов уборки			(CleaningHistory)
create table CleaningHistory (
	Id					int			not null	primary key identity,
	[Level]				int			not null,	-- Этаж
	DateCleaning		date		not null,	-- Дата уборки
	IdEmployee			int			not null	foreign key references Employees(Id)	-- Служащий гостиницы
);
go


-- заполнение таблицы Дни недели						(DaysOfWeek)
create table DaysOfWeek (
	Id				int				not null	primary key identity,
	[Name]			nvarchar(20)	not null,	-- Название дня недели
);
go


-- заполнение таблицы График уборки					(CleaningSchedule)
create table CleaningSchedule (
	Id					int			not null	primary key identity,
	IdDayOfWeek			int			not null	foreign key references DaysOfWeek(Id),		-- День недели
	IdEmployee			int			not null	foreign key references Employees(Id)		-- Служащий гостиницы
);
go
