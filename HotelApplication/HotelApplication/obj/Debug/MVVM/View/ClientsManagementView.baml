   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5      Q-clr-namespace:HotelApplication.MVVM.ViewModelHotelApplication.MVVM.ViewModel  5      I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5      6    0HotelApplication.MVVM.View.ClientsManagementView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:HotelApplication.MVVM.View     5      ?model-clr-namespace:HotelApplication.MVVM.ViewModel     5      :conv)clr-namespace:HotelApplication.Converters     5      -   5        $��ControlUser��5        � DataContext  5      @   :HotelApplication.MVVM.ViewModel.ClientsManagementViewModel  5   
    � 	Resources 5      <   6HotelApplication.Converters.DoubleSubtractionConverter   DoubleSubtractionConverter4   .HotelApplication.Converters.IsDeletedConverter  IsDeletedConverter��%:   &	        &	        5   
   6*     5   
   6"   5      .��5      �� .+�5    
   �� $��Клиенты��6   
�������$��28(�5"      $
��Left=�5#      $��0 0 0 20q�5$      �� 5'   
    �� Orientation$ 
Horizontal=�6   .+�5(      �� $	��250��6   $��50��5)       �� CornerRadius$ 10��5*      ��5-      �� 5.       �� 
StartPoint$	 0 0&�6*    �� EndPoint$	 1 2&�6;   .��5/      �  � Color$ #5bc3ff��6'    � Offset$ 0Q�67   � 50      $ #3aa0ff��6'   $	 1.1Q�67   51      ._�56      �� $ 
Horizontal=�6   $��Center=�57   !   $
��20 0q�58   !   .+�5:      �� $#��Всего клиентов: ��6    
�������$��18(�5<   $   �� 5>      
�������$��18(�5@   $   ��5>       �� *CountClients+5@   $   5A      5C      �� 5G      $%��Добавить клиента��6   $	��150��60   $��	20 0 10 0q�5J      ���#4EFF91��5H      �� *AddClientCommand+��5I      �� *SelectedClient+5K      �� 5N      $%��Изменить клиента��6   $	��150��60   $
��10 0q�5Q      ���#ECFF4E��5O      �� *EditClientCommand+��5P      �� *SelectedClient+5R      �� 5U      $#��Удалить клиента��6   $	��150��6/   $
��10 0q�5X      ���#844EFF��5V      �� *RemoveClientCommand+��5W      �� *SelectedClient+5Y      5[      &   System.Windows.Controls.DataGrid  5_   
   -   5        $��
DgdClients��5_      $
��0 20q�6%      AutoGenerateColumns .  	 �� AlternationCount$	 2��5a      
   RowHeaderWidth$
 0Q�5b         
IsReadOnly . $
��Left=�5e         SelectionMode$ Single=�5f      
������    CanUserSortColumns .  ��5_   3   �� *	Clients+��5c      ��  �� ElementName$ ControlUser�� �� Path$ ActualHeight� �� 	Converter# [   �� ConverterParameter$	 150�� �� SelectedItem 5h      �� *SelectedClient+ �� Mode$ TwoWay=�   Columns 5l      0  *System.Windows.Controls.DataGridTextColumn  5m      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 20* 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *Person.Surname+6Y     5n      $ Имя��6%   $	 20* 6R    62   �� *Person.Name+6R     5o      $ Отчество��6%   $	 20* 6]    67   �� *Person.Patronymic+6]     5p      $ Паспорт��6%   $	 20* 6S    66   �� *
Passport+6S     5q      $ Статус��6%   $	 20* 6�    65   �� *	IsDeleted+# [ 6�    � ContextMenu 5v      �� 5w      .@�5y      m� $%��Добавить клиента��6    m� Command 5z   #   �� *AddClientCommand+m� 5|      $/��)Редактировать клиента��6    5}   #   �� *EditClientCommand+m� 5      $#��Удалить клиента��6    5�   #   �� *RemoveClientCommand+5�      5�      5�      5�      