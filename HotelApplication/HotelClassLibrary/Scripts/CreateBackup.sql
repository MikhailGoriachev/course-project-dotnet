use Hotel;

-- удаление логического устройства
exec sp_dropdevice 'Hotel_backup';

-- создание логического устройства
exec sp_addumpdevice 'disk', 'Hotel_backup', 'D:\01. Programming\00. Курсовые проекты\01. Курсовой проект .NET Framework\Hotel_backup.bak';

-- установка типа резервирования
alter database Hotel set recovery full;
go

-- создание полной резервной копии базы данных Hotel
backup database Hotel to Hotel_backup;
go

-- проверка резервной копии на целостность
restore verifyonly from Hotel_backup;
go

use master;

-- закрытие соединения с базой данных Hotel
alter database Hotel set offline

-- удаление базы данных Hotel
drop database if exists Hotel;
go

-- восстановление базы данных Hotel из резервной копии
restore database Hotel from Hotel_backup;
go