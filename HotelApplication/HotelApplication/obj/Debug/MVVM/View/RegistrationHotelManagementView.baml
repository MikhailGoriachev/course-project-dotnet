   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5      I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5      Q-clr-namespace:HotelApplication.MVVM.ViewModelHotelApplication.MVVM.ViewModel  5      6    0HotelApplication.MVVM.View.RegistrationHotelView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:HotelApplication.MVVM.View     5      :conv)clr-namespace:HotelApplication.Converters     5      ?model-clr-namespace:HotelApplication.MVVM.ViewModel     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      -   5        $��ControlUser��5        � DataContext  5      J   DHotelApplication.MVVM.ViewModel.RegistrationHotelManagementViewModel  5   
    � 	Resources 5      <   6HotelApplication.Converters.EmployeeIsDeletedConverter   EmployeeIsDeletedConverter/   )HotelApplication.Converters.DateConverter  DateConverter<   6HotelApplication.Converters.DoubleSubtractionConverter  DoubleSubtractionConverter �� 
TargetType �� Property �� Value��%@  &	        &	      &	 &     '�� 9     '�� �     '�� $      5   
   6*     5   
   6     5   
   6*   �� 5    
   " ��6   .�5!      ��  � ��
 �����6+   �� 5"       � ��$ 14(�6)   �� 5#       � ��$
 20 0q�6'   �� 5$       � ��$ Center=�69   5%      �� 5(   
   " ��6   .�5)      ��  � ��
 �����6+   �� 5*       � ��$ 14(�6)   �� 5+       � ��$
 20 0q�6'   �� 5,       � ��$ 30��6'   �� 5-       � ��$ Stretch=�64   �� 5.       � ��$ Center=�69   �� 5/       � ��
 �   �6+   50      �� 53   
   " ��6   .�54      ��  � ��
 �����6+   �� 55       � ��$ 14(�6)   �� 56       � ��$
 18 0q�6'   �� 57       � ��$ Center=�69   �� 58       � ��$ 30��6'   59      5;      .��5?      �� .+�5A   
   �  � ColumnDefinitions 5B      �� 5C      �� 5D      .+�5H      �� $-��'Регистрация клиентов��6   
�������$��28(�5J      $
��Left=�5K      $��0 0 0 20q�5L      �� 5O      $��2��6    �� Orientation$ Vertical=�6)   $��Right=�5P      .+�5R      �� -   5        $��CbxIsSelectionByCity��5R      $'��!Выборка по городу:��67    d� 	IsChecked 5S      �� *IsSelectionByCity+.  (System.Windows.Interactivity.Interaction   Triggers 5V      /  )System.Windows.Interactivity.EventTrigger  5W      	   	EventName$	 Click��6)   .  (System.Windows.Interactivity.TriggerBase
   Actions.
 5X      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *SelectionByCityCommand+5Y      5[      �� 5^      $	��280��6   ��5_      �� *Cities+ �� SelectedItem 5`      �� *SelectedCity+{�5a      ��  �� ElementName$ CbxIsSelectionByCity�� �� Path$ 	IsChecked� 5d        5e      $	 SelectionChanged��6)   .
 5f         64   �� *SelectionByCityCommand+5g      5j      5k      5n      �� 5q   
   $ 
Horizontal=�6   $��0 10 0 0q�5r      .+�5s      �� $	��250��6   $��50��5t       �� CornerRadius$ 10��5u      ��5x      �� 5y       �� 
StartPoint$	 0 0&�6*    �� EndPoint$	 1 2&�6;   .��5z      �  � Color$ #5bc3ff��6'    � Offset$ 0Q�67   � 5{      $ #3aa0ff��6'   $	 1.1Q�67   5|      ._�5�      �� $ 
Horizontal=�6   $��Center=�5�   !   $
��20 0q�5�   !   .+�5�      �� $!��Всего записей: ��6    
�������$��18(�5�   (   �� 5�      
�������$��18(�5�   (   ��5�       �� *CountRegistration+5�   (   5�      5�      �� 5�      $-��'Добавить регистрацию��6   $	��150��64   $��	20 0 10 0q�5�      ���#4EFF91��5�      �� *AddRegistrationCommand+��5�      �� *SelectedRegistration+5�      �� 5�      $-��'Изменить регистрацию��6   $	��150��64   $
��10 0q�5�      ���#ECFF4E��5�      �� *EditRegistrationCommand+��5�      �� *SelectedRegistration+5�      �� 5�      $%��Выселить клиента��6   $	��150��60   $
��10 0q�5�      ���#844EFF��5�      �� *EvictRegistrationCommand+��5�      �� *SelectedRegistration+5�      5�      &	   System.Windows.Controls.DataGrid	  5�   
   $
��0 20q�6    	  AutoGenerateColumns .   �� AlternationCount$ 2��5�       	  RowHeaderWidth$ 0Q�5�       	  
IsReadOnly . $
��Left=�5�       	  SelectionMode$ Single=�5�      
������  	  CanUserSortColumns .  ��5�   !   �� *SelectedRegistrationList+��5�      �� $ ControlUser��$ ActualHeight� �� 	Converter# [  �� ConverterParameter$	 150�� 5�      �� *SelectedRegistration+ �� Mode$ TwoWay=� 	  Columns 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 20* 6`   1  +System.Windows.Controls.DataGridBoundColumn    Binding  66   �� *Client.Person.Surname+6`   
  5�      $ Имя��6%   $	 20* 6Y     62   �� *Client.Person.Name+6Y   
  5�      $ Отчество��6%   $	 20* 6d     67   �� *Client.Person.Patronymic+6d   
  5�      $ Паспорт��6%   $	 20* 6Z     66   �� *Client.Passport+6Z   
  5�      $ Номер комнаты��6%   $	 20* 6a     6<   �� *HotelRoom.Number+6a   
  5�      $ Этаж��6%   $	 20* 6^     63   �� *HotelRoom.Floor.Number+6^   
  5�      $ 
Город��6%   $	 20* 6R     64   �� *	City.Name+6R   
  5�      $3 -Длительность проживания��6%   $	 25* 6c     6F   �� *
Duration+6c   
  5�      $% Дата регистрации��6%   $	 30* 6�     6?   �� *RegistrationDate+# [ 6�   ! � ContextMenu! 5�      �� 5�      .@�5�      m� $-��'Добавить регистрацию��6   " m� Command" 5�   #   �� *AddRegistrationCommand+m� 5�      $7��1Редактировать регистрацию��6   " 5�   #   �� *EditRegistrationCommand+m� 5�      $%��Выселить клиента��6   " 5�   #   �� *EvictRegistrationCommand+�� 5�      m� 5�      $9��3Получить счёт за проживение��6   " 5�      �� *CostRegistrationCommand+5�      5�      5�      5�      