   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5      I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5      L HHotelClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null^Aclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibraryHotelClassLibrary.Models 5	      Q-clr-namespace:HotelApplication.MVVM.ViewModelHotelApplication.MVVM.ViewModel  5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      +    %HotelApplication.MVVM.View.ReportView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:HotelApplication.MVVM.View     5      :conv)clr-namespace:HotelApplication.Converters     5      SmodelAclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibrary     5	      <vm-clr-namespace:HotelApplication.MVVM.ViewModel     5
      6sys&clr-namespace:System;assembly=mscorlib     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        � DataContext  5      5   /HotelApplication.MVVM.ViewModel.ReportViewModel  5   
    � 	Resources 5      /   )HotelApplication.Converters.DateConverter   DateConverter �� 
TargetType �� Property �� Value��%Q  &	        '��      '�� �     '�� �      5   
   6   �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 16(�6)   �� 5       � ��$ 0q�6'   �� 5        � ��$ Center=�69   5!      �� 5$   
   " ��6   .�5%      ��  � ��
 �����6+   �� 5&       � ��$ 14(�6)   �� 5'       � ��$
 20 0q�6'   �� 5(       � ��$ 30��6'   �� 5)       � ��$ Stretch=�64   �� 5*       � ��$ Center=�69   �� 5+       � ��
 �   �6+   5,      �� 5/   
   " ��6   .�50      ��  � ��
 ���� 6+   �� 51       � ��$ 1q�60   �� 52       � ��$ 30��6'   �� 53       � ��$
 20 0q�6'   �� 54       � ��
 �����6+   �� 55       � ��$ Center=�69   �� 56       � ��$ 5 0 0 0q�6(   57      59      .��5<      �� .��5=   
   �  � RowDefinitions 5>      �� 5?      $
��auto �6    �� 5@      $	��250 �6    �� 5A      $
��auto �6    �� 5B      $
��auto �6    �� 5C      $
��auto �6     � ColumnDefinitions 5F      �� 5G      �� 5H      .+�5L      �� $��
Отчёт��6   
�������$��28(�5N      $
��Left=�5O      $��0 0 0 20q�5P      �� 5S      $��0��6   $��1��6)   $��3��66    �� Orientation$ Vertical=�5T      $��Center=�5U      .+�5X      �� $!��Начальная дата��6   $��Center=�5Y       $��18(�5Z       &   System.Windows.Controls.Calendar  5]         SelectedDate 6   �� *	StartDate+5_      �� 5b      $��1��6   $��1��6)   $��3��66   $ Vertical=�5c      $��Center=�5d      .+�5g      �� $��Конечная дата��6   $��Center=�5h      $��18(�5i        5l       6   �� *	EndDate+5n      �� 5q      -   5        $��BtnOK��5q   6   $��2��6   $��2��6"   $!��Получить отчёт��6C   ���#4EFF91$��20q�5s      ��5t      �� *ResultReportCommand+�� 5x      $��3��6   $��2��6%   $��Данные��69   
�������$��28(�5z      $��Center=�5{      $��0 0 0 20q�5|      �� 5      $��2��6   $��4��6-   .+�5�      �� $ 
Horizontal=�6   $
��Left=�5�      .+�5�      �� $"��Начальная дата:��6   �� 5�      ��6   �� *Report.StartDate+	 �� 	Converter#	 [  5�      �� 5�      $ 
Horizontal=�6   $
��Left=�5�      .+�5�      �� $ ��Конечная дата:��6   �� 5�      ��6   �� *Report.EndDate+#	 [  5�      �� 5�      $ 
Horizontal=�6   $
��Left=�5�      .+�5�      �� $"��Число клиентов:��6   �� 5�      ��6   �� *Report.CountClients+5�      �� 5�      $ 
Horizontal=�6   $
��Left=�5�      .+�5�      �� $)��#Общая сумма дохода:��6   �� 5�      ��6   �� *Report.Account+5�      �� 5�      $)��#Статистика номеров��6   $��18(�5�      $��Center=�5�      &   System.Windows.Controls.DataGrid  5�      $��20q�6   
   AutoGenerateColumns
 .   �� AlternationCount$ 2��5�      $	��400��5�         RowHeaderWidth$ 0Q�5�         
IsReadOnly . $
��Left=�5�         SelectionMode$ Single=�5�      
������    CanUserSortColumns .  ��5�   '   �� *Report.ResultRooms+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Этаж��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 20* 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *Room.Floor.Number+6a     5�      $ Номер комнаты��6-   $	 20* 6d    6D   �� *Room.Number+6d     5�      $ Тип номера��6-   $	 20* 6m    6A   �� *Room.TypeHotelRoom.Name+6m     5�      $% Стоимость (сутки)��6-   $	 20* 6u    6H   �� *Room.TypeHotelRoom.Price+6u     5�      $! Номер телефона��6-   $	 20* 6j    6E   �� *Room.PhoneNumber+6j     5�      $ Занят (дней)��6-   $	 20* 6`    6C   �� *
BusyDays+6`     5�      $! Свободен (дней)��6-   $	 20* 6d    6F   �� *	EmptyDays+6d   5�      5�      5�      5�      5�      