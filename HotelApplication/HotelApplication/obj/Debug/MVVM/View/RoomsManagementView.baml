   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5      I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5      Q-clr-namespace:HotelApplication.MVVM.ViewModelHotelApplication.MVVM.ViewModel  5	      4    .HotelApplication.MVVM.View.RoomsManagementView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:HotelApplication.MVVM.View     5      :conv)clr-namespace:HotelApplication.Converters     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      9model-clr-namespace:HotelApplication.MVVM.ViewModel  5	      -   5        $��ControlUser��5        � DataContext  5      >   8HotelApplication.MVVM.ViewModel.RoomsManagementViewModel  5   
    � 	Resources 5      <   6HotelApplication.Converters.DoubleSubtractionConverter   DoubleSubtractionConverter8   2HotelApplication.Converters.HotelRoomPlaceToString  HotelRoomPlaceToString �� 
TargetType �� Property �� Value��%$  &	        &	      '�� &       5   
   6*     5   
   6&   �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5        � ��$
 18 0q�6'   �� 5!       � ��$ Center=�69   �� 5"       � ��$ 30��6'   5#      5%      .��5)      �� .+�5+   
   �  � ColumnDefinitions 5,      �� 5-      �� 5.      .+�53      �� $+��%Управление номерами��6   
�������$��28(�55      $
��Left=�56      $��0 0 0 20q�57      �� 5:      -   5        $��CbxIsSelectionByCity��5:   '   $��1��6   $3��-Только свободные номера:��5;      $��16(�5<       d� 	IsChecked 5=      �� *IsSelectionByEmpty+.  (System.Windows.Interactivity.Interaction   Triggers 5@      /  )System.Windows.Interactivity.EventTrigger  5A         	EventName$ Click��6%   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5B      6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 60   �� *SelectionByEmptyCommand+5C      5F      5H      �� 5K   
    �� Orientation$ 
Horizontal=�6   .+�5L      �� $	��250��6    �� CornerRadius$ 10��5N      ��5Q      �� 5R       �� 
StartPoint$	 0 0&�6*    �� EndPoint$	 1 2&�6;   .��5S      �  � Color$ #5bc3ff��6'    � Offset$ 0Q�67   � 5T      $ #3aa0ff��6'   $	 1.1Q�67   5U      ._�5Z      �� $ 
Horizontal=�6   $��Center=�5[      $
��20 0q�5\      .+�5^      �� $+��%Количество номеров: ��6    
�������$��18(�5`       �� 5b      
�������$��18(�5d       ��5b       �� *
CountRooms+5d       5e      5g      �� 5j      $%��Добавить клиента��6   $	��150��60   $��	20 0 10 0q�5l      ���#4EFF91��5k      �� *AddClientRoomCommand+5m      �� 5p      $!��Клиенты номера��6   $	��150��6.   $
��10 0q�5r      ���#ECFF4E��5q      �� *EvictClientRoomCommand+5s      �� 5v      $,��&Информация об уборке��6   $	��150��64   $
��10 0q�5x      ���#ECFF4E��5w      �� *CleaningRoomInfoCommand+5y      5{      &   System.Windows.Controls.DataGrid  5   
   $
��0 20q�6      AutoGenerateColumns .   �� AlternationCount$ 2��5�         RowHeaderWidth$ 0Q�5�         
IsReadOnly . $
��Left=�5�         SelectionMode$ Single=�5�      
������    CanUserSortColumns .  ��5   !   �� *SelectedRoomsList+��5�      ��  �� ElementName$ ControlUser�� �� Path$ ActualHeight� �� 	Converter# [   �� ConverterParameter$	 150�� �� SelectedItem 5�      �� *SelectedRoom+ �� Mode$ TwoWay=�   Columns 5�      0	  *System.Windows.Controls.DataGridTextColumn	  5�      ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Этаж��6%    
  Width5  /System.Windows.Controls.DataGridLengthConverter$	 20* 6T   1  +System.Windows.Controls.DataGridBoundColumn    Binding  63   �� *Floor.Number+6T   	  5�      $ Номер комнаты��6%   $	 20* 6W     6<   �� *Number+6W   	  5�      $ Тип номера��6%   $	 20* 6`     69   �� *TypeHotelRoom.Name+6`   	  5�      $* $Места (занято/всего)��6%   $	 20* 6�     6C   �� *Id+# [ 6�   	  5�      $% Стоимость (сутки)��6%   $	 20* 6h     6@   �� *TypeHotelRoom.Price+6h   	  5�      $! Номер телефона��6%   $	 20* 6]     6=   �� *PhoneNumber+6]   ! � ContextMenu! 5�      �� 5�      .@�5�      m� $%��Добавить клиента��6   " m� Command" 5�      �� *AddClientRoomCommand+m� 5�      $!��Клиенты номера��6   " 5�      �� *EvictClientRoomCommand+5�      5�      5�      5�      