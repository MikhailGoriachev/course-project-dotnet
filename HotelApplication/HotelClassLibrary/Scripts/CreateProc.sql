-- Скрипт создания процедур


-- 1.	О клиентах, проживающих в заданном номере

-- удаление процедры
drop proc if exists Proc1;
go

-- создание процедуры
create or alter proc Proc1 
	@number int
as
	select distinct
		HistoryRegistrationHotelView.Client
		, HistoryRegistrationHotelView.Passport
	from
		HistoryRegistrationHotelView
	where
		HistoryRegistrationHotelView.RoomNumber = @number and convert(date, getdate()) between HistoryRegistrationHotelView.RegistrationDate 
														  and DATEADD(day, HistoryRegistrationHotelView.Duration, HistoryRegistrationHotelView.RegistrationDate);
go

exec Proc1 27;
go


-- 2.	О клиентах, прибывших из заданного города

-- удаление процедуры
drop proc if exists Proc2;
go

-- создание процедуры
create or alter proc Proc2 
	@city nvarchar(60)
as
	select distinct
		HistoryRegistrationHotelView.Client
		, HistoryRegistrationHotelView.Passport
	from
		HistoryRegistrationHotelView
	where
		HistoryRegistrationHotelView.City = @city;
go

exec Proc2 'Донецк';
go


-- 3.	О том, кто из служащих убирал номер указанного клиента в заданный день недели

-- удаление процедуры
--drop proc if exists Proc3;
--go

---- создание процедуры
--create or alter proc Proc3
--	@client int,
--	@date date
--as
--	-- 


--declare @passport nvarchar(15) = '524678654', @numberDay int = 1; 

---- дата поселения и выселения клиента 
--declare @dateStart date = (select HistoryRegistrationHotelView.RegistrationDate from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

---- выбираем уборки в номере клиента за указанный день
--declare @numRoom int = (select HistoryRegistrationHotelView.RoomNumber from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

--select	
	

-- 4.	Есть ли в гостинице свободные места и свободные номера и, если есть, то сколько и какие именно номера свободны.

-- удаление процедуры
drop proc if exists Proc4;
go

-- создание процедуры
create or alter proc Proc4
as
	select
		*
	from
		HotelRoomsView
	where
		[State] = 0;
go

exec Proc4;
go