use Hotel

-- 1.	О клиентах, проживающих в заданном номере
declare @number int = 4;

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
declare @passport nvarchar(15) = '524678654', @numberDay int = 1; 

-- дата поселения и выселения клиента 
declare @dateStart date = (select HistoryRegistrationHotelView.RegistrationDate from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

-- выбираем уборки в номере клиента за указанный день
declare @numRoom int = (select HistoryRegistrationHotelView.RoomNumber from HistoryRegistrationHotelView where HistoryRegistrationHotelView.Passport = @passport);

select	
	

-- 4.	Есть ли в гостинице свободные места и свободные номера и, если есть, то сколько и какие именно номера свободны.
-- •	принять на работу или уволить служащего гостиницы
-- •	изменить расписание работы служащего
-- •	поселить или выселить клиента.
-- •	автоматической выдачи клиенту счета за проживание в гостинице.
-- •	отчета о работе гостиницы за указанный квартал текущего года. Такой отчет должен содержать следующие сведения: число клиентов за указанный период, сколько дней был занят и свободен каждый из номеров гостиницы, общая сумма дохода.