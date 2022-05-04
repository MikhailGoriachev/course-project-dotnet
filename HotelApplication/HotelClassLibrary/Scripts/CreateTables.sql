-- создание таблиц для базы данных проекта "Гостиница"

--set noexec off
--go

--use master


---- создание базы данных Hotel
--if db_id('Hotel') is null begin
--	create database Hotel
--	print 'База данных Hotel успешно создана'
--end
--go


-- use Hotel

-- удаление таблиц
drop table if exists CleaningSchedule				-- таблица График уборки					(CleaningSchedule)
drop table if exists DaysOfWeek						-- таблица Дни недели						(DaysOfWeek)
drop table if exists CleaningHistory				-- таблица История фактов уборки			(CleaningHistory)
drop table if exists HistoryRegistrationHotel		-- таблица История поселений в гостиницу	(HistoryRegistrationHotel)
drop table if exists Cities							-- таблица Города							(Cities)
drop table if exists HotelRooms						-- таблица Номера гостиницы					(HotelRooms)
drop table if exists TypesHotelRoom					-- таблица Типы номеров						(TypesHotelRoom)
drop table if exists Floors							-- таблица Этажи							(Floors)
drop table if exists Employees						-- таблица Служащие гостиницы				(Employees)
drop table if exists Clients						-- таблица Клиенты							(Clients)
drop table if exists Persons						-- таблица Персоны							(Persons)
go


-- создание таблицы Персоны							(Persons)
create table Persons (
	Id			int					not null	primary key identity,
	Surname		nvarchar(60)		not null,	-- Фамилия
	[Name]		nvarchar(40)		not null,	-- Имя
	Patronymic	nvarchar(60)		not null,	-- Отчество
	IsDeleted			bit			not null	-- Статус удаления
);
go


-- создание таблицы Клиенты							(Clients)
create table Clients (
	Id			int					not null	primary key identity,
	IdPerson	int					not null	foreign key references Persons(Id),	-- Персона
	Passport	nvarchar(15)		not null,	-- Номер паспорта
	IsDeleted	bit					not null	-- Статус удаления
);
go


-- создание таблицы Служащие гостиницы				(Employees)
create table Employees (
	Id			int					not null	primary key identity,
	IdPerson	int					not null	foreign key references Persons(Id),	-- Персона
	IsDeleted	bit					not null	-- Статус удаления
);
go

-- создание таблицы Типы номеров					(TypesHotelRoom)
create table TypesHotelRoom (
	Id			int					not null	primary key identity,
	[Name]		nvarchar(40)		not null,	-- Название типа
	CountPlace	int					not null,	-- Количество мест
	Price		int					not null,	-- Стоимость в сутки
	IsDeleted	bit					not null	-- Статус удаления
);
go


-- создание таблицы Этажи							(Floors)
create table Floors (
	Id			int					not null	primary key identity,
	Number		int					not null,	-- Номер этажа
	IsDeleted	bit					not null,	-- Статус удаления
);
go


-- создание таблицы Номера гостиницы				(HotelRooms)
create table HotelRooms (
	Id					int				not null	primary key identity,
	IdTypeHotelRoom		int				not null	foreign key references TypesHotelRoom(Id),		-- Тип номера
	IdFloor				int				not null	foreign key references Floors(Id),				-- Этаж	
	Number				int				not null,	-- Номер гостиничного номера
	PhoneNumber			nvarchar(20)	not null,	-- Номер телефона
	IsDeleted			bit				not null	-- Статус удаления
);
go


-- создание таблицы Города							(Cities)
create table Cities (
	Id					int				not null	primary key identity,
	[Name]				nvarchar(60)	not null,	-- Наименование города
	IsDeleted			bit				not null	-- Статус удаления
);
go


-- создание таблицы История поселений в гостиницу	(HistoryRegistrationHotel)
create table HistoryRegistrationHotel (
	Id					int			not null	primary key identity,
	IdClient			int			not null	foreign key references Clients(Id),		-- Клиент
	IdHotelRoom			int			not null	foreign key references HotelRooms(Id),	-- Гостиничный номер
	IdCity				int			not null	foreign key references Cities(Id),		-- Город, из которого прибыл клиент
	RegistrationDate	date		not null,	-- Дата поселения
	Duration			int			not null,	-- Длительность проживания 
	IsDeleted			bit			not null	-- Статус удаления
);
go


-- создание таблицы История фактов уборки			(CleaningHistory)
create table CleaningHistory (
	Id					int			not null	primary key identity,
	IdFloor				int			not null	foreign key references Floors(Id),		-- Этаж
	DateCleaning		date		not null,											-- Дата уборки
	IdEmployee			int			not null	foreign key references Employees(Id),	-- Служащий гостиницы
	IsDeleted			bit			not null	-- Статус удаления
);
go


-- создание таблицы Дни недели						(DaysOfWeek)
create table DaysOfWeek (
	Id				int				not null	primary key identity,
	[Name]			nvarchar(20)	not null,	-- Название дня недели
	Number			int				not null	-- Номер дня недели
);
go


-- создание таблицы График уборки					(CleaningSchedule)
create table CleaningSchedule (
	Id					int			not null	primary key identity,
	IdDayOfWeek			int			not null	foreign key references DaysOfWeek(Id),		-- День недели
	IdEmployee			int			not null	foreign key references Employees(Id),		-- Служащий гостиницы
	IdFloor				int			not null	foreign key references Floors(Id),			-- Этаж
);
go
