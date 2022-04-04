use Hotel

--------------------------------------------------------------------------------------


-- Задание на 10.03.2022.	Разработайте запросы к трем таблицам БД Вашего проекта на выборку данных
--							при помощи курсора. 

-- запросы на выборку всех данных из трёх таблиц по заданию на 10.03.2022


-- 1. Выборка данных из таблицы Clients

-- переменные для чтения
declare @id int, @surname nvarchar(60), @name nvarchar(40), @patronymic nvarchar(60), @passport nvarchar(15);

-- создание курсора
declare ClientsCursor cursor
for 
	select
		*
	from
		ClientsView;

-- открытие курсора
open ClientsCursor;

-- чтение первой записи
fetch next from ClientsCursor into @id, @surname, @name, @patronymic, @passport;

-- чтение и вывод записей
while @@FETCH_STATUS = 0 begin
	print '| ' + str(@id, 5) + ' | ' + @surname + ' | ' + @name + ' | ' + @patronymic + ' | ' + @passport + ' |';
	fetch next from ClientsCursor into @id, @surname, @name, @patronymic, @passport;
end;

-- закрытие курсора
close ClientsCursor;

-- удаление курсора
deallocate ClientsCursor;

go



-- 2. Выборка данных из таблицы HotelRooms

-- переменные для чтения
declare @id int, @typeRoom nvarchar(40), @countRooms int, @price int, @floorNumber int, @number int, @state bit;

-- создание курсора
declare HotelRoomsCursor cursor
for 
	select
		*
	from
		HotelRoomsView;

-- открытие курсора
open HotelRoomsCursor;

-- чтение первой записи
fetch next from HotelRoomsCursor into @id, @typeRoom, @countRooms, @price, @floorNumber, @number, @state;

-- чтение и вывод записей
while @@FETCH_STATUS = 0 begin
	print '| ' + str(@id, 5) + ' | ' + @typeRoom + ' | ' + str(@countRooms, 5) + ' | ' + str(@price, 5) + ' | '+ str(@floorNumber, 5) + 
		  ' | '+ str(@number, 5) + ' | '+ str(@state, 3) + ' |';
	fetch next from HotelRoomsCursor into @id, @typeRoom, @countRooms, @price, @floorNumber, @number, @state;
end;

-- закрытие курсора
close HotelRoomsCursor;

-- удаление курсора
deallocate HotelRoomsCursor;

go


-- 3. Выборка данных из таблицы EmployeesView

-- переменные для чтения
declare @id int, @surname nvarchar(60), @name nvarchar(40), @patronymic nvarchar(60), @workState bit;

declare EmployeesViewCursos cursor
for 
	select
		*
	from
		EmployeesView;

-- открытие курсора
open EmployeesViewCursos;

-- чтение первой записи
fetch next from EmployeesViewCursos into @id, @surname, @name, @patronymic, @workState;

-- чтение и вывод записей
while @@FETCH_STATUS = 0 begin
	print '| ' + str(@id, 5) + ' | ' + @surname + ' | ' + @name + ' | ' + @patronymic + ' | ' + str(@workState, 3) + ' |';
	fetch next from EmployeesViewCursos into @id, @surname, @name, @patronymic, @workState;
end;

-- закрытие курсора
close EmployeesViewCursos;

-- удаление курсора
deallocate EmployeesViewCursos;

go


--------------------------------------------------------------------------------------


-- 1.	О клиентах, проживающих в заданном номере
declare @number int = 3;

select distinct
	HistoryRegistrationHotelView.Client
	, HistoryRegistrationHotelView.Passport
from
	HistoryRegistrationHotelView
where
	HistoryRegistrationHotelView.FloorNumber = @number;
go


-- 2.	О клиентах, прибывших из заданного города
declare @city nvarchar(60) = 'Донецк';

select distinct
	HistoryRegistrationHotelView.Client
	, HistoryRegistrationHotelView.Passport
from
	HistoryRegistrationHotelView
where
	HistoryRegistrationHotelView.City = @city;
go


-- 3.	О том, кто из служащих убирал номер указанного клиента в заданный день недели
--declare @passport nvarchar(15) = '524678654', @numberDay int = 1; 

---- дата поселения и выселения клиента 
--declare @dateStart date = (select HistoryRegistrationHotelView.RegistrationDate from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

---- выбираем уборки в номере клиента за указанный день
--declare @numRoom int = (select HistoryRegistrationHotelView.RoomNumber from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

--select	
	

-- 4.	Есть ли в гостинице свободные места и свободные номера и, если есть, то сколько и какие именно номера свободны.

