use Hotel;

-- �������� ����������� ����������
exec sp_dropdevice 'Hotel_backup';

-- �������� ����������� ����������
exec sp_addumpdevice 'disk', 'Hotel_backup', 'D:\01. Programming\00. �������� �������\01. �������� ������ .NET Framework\Hotel_backup.bak';

-- ��������� ���� ��������������
alter database Hotel set recovery full;
go

-- �������� ������ ��������� ����� ���� ������ Hotel
backup database Hotel to Hotel_backup;
go

-- �������� ��������� ����� �� �����������
restore verifyonly from Hotel_backup;
go

use master;

-- �������� ���������� � ����� ������ Hotel
alter database Hotel set offline

-- �������� ���� ������ Hotel
drop database if exists Hotel;
go

-- �������������� ���� ������ Hotel �� ��������� �����
restore database Hotel from Hotel_backup;
go