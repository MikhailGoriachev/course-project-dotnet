   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5   	   L HHotelClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null^Aclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibraryHotelClassLibrary.Models 5   	   Q-clr-namespace:HotelApplication.MVVM.ViewModelHotelApplication.MVVM.ViewModel  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	   	   I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5   	   5    /HotelApplication.MVVM.View.CostRegistrationView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:HotelApplication.MVVM.View     5   	   SmodelAclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibrary     5   	   <vm-clr-namespace:HotelApplication.MVVM.ViewModel     5   	   6sys&clr-namespace:System;assembly=mscorlib     5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   4conv)clr-namespace:HotelApplication.Converters  5   	     � Title$  EvictClient��5   	    � SizeToContent$ Height=�6   $
��1000��64    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ 
�������$��12(�5   	    � DataContext 5      ?   9HotelApplication.MVVM.ViewModel.CostRegistrationViewModel  5   
    � 	Resources 5      3   -HotelApplication.Converters.BackBoolConverter   BackBoolConverter/   )HotelApplication.Converters.DateConverter  DateConverter<   6HotelApplication.Converters.CoastRegistrationConverter  CoastRegistrationConverter3   -HotelApplication.Converters.HotelRoomToString  HotelRoomToString6   0HotelApplication.Converters.WindowStateConverter  WindowStateConverter �� 
TargetType �� Property	 �� Value��%�  &	        &	      &	 &     &	 9     &	 L     '�� _     '��     '�� J      5   
   6!     5"   
   6     5%   
   6*     5(   
   6!     5+   
   6$   �� 5.   
   " ��6   .�5/      ��  � ��
	 �����6+   �� 50       � ��$	 14(�6)   �� 51       � ��$
	 20 0q�6'   �� 52       � ��$	 Center=�69   53      �� 56   
   " ��6   .�57      ��  � ��
	 �����6+   �� 58       � ��$	 14(�6)   �� 59       � ��$
	 20 0q�6'   �� 5:       � ��$	 30��6'   �� 5;       � ��$	 Stretch=�64   �� 5<       � ��$	 Center=�69   �� 5=       � ��
	 �   �6+   5>      �� 5A   
   " ��6   .�5B      ��  � ��
	 ���� 6+   �� 5C       � ��$	 1q�60   �� 5D       � ��$	 30��6'   �� 5E       � ��$
	 20 0q�6'   �� 5F       � ��
	 �����6+   �� 5G       � ��$	 Center=�69   �� 5H       � ��$	 5 0 0 0q�6(   5I      5K      .��5O      �� ���#302E44
 �� CornerRadius$
 20��5P      ._�5Q   
   �  � ColumnDefinitions 5S      �� 5T      �� 5U       � RowDefinitions 5Y      �� 5Z      $��75 �6    �� 5[      .+�5_      �� 
������ $��2��5`      �� 5c      -   5        $��	LblHeader��5c      $<��6Стоимость проживания клиента��6%   $��20(�5d      $��20q�5e      $��2��5f      $��Center=�5g      � 5j      $��1��6   $��2��5k       5n      �� 5o      �� 5p       5t      �� 5u      �� 5v      �� 5w      .+�5{      �� $��0��6    �� Orientation$ Vertical=�6*   $��Center=�5|      .+�5}      �� $��Клиент:��6   �� 5~      ��6   �� *SelectedClientsList+ �� SelectedValue 5      �� *SelectedClient+.  (System.Windows.Interactivity.Interaction   Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�         	EventName$ SelectionChanged��6-   .	  (System.Windows.Interactivity.TriggerBase 	  Actions. 5�   "   6
  0System.Windows.Interactivity.InvokeCommandAction
   
  Command 68   �� *$"SelectionClientRegistrationCommand+5�      5�      5�      �� 5�      $��1��6   $ Vertical=�6-   $��Center=�5�      .+�5�      �� $ ��Поиск клиента:��6   �� 5�      -   5         ��Name$ TbxSearchClient��5�      .��67   .��5�   B    5�        5�      $ TextChanged��6-   . 5�   "   
   68   �� *SearchClientsCommand+ 
  CommandParameter 5�   8   �� *Text+ �� ElementName$ TbxSearchClient��5�      .��5�   2   5�      5�      &   System.Windows.Controls.DataGrid  5�      $��1��6   $��2��6(   $
��0 20q�6<      AutoGenerateColumns .   �� AlternationCount$ 2��5�      $	��300��5�         RowHeaderWidth$ 0Q�5�         
IsReadOnly . $
��Left=�5�         SelectionMode$ Single=�5�      
������    CanUserSortColumns .  ��5�   J   �� *SelectedRegistrationList+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер комнаты��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 20* 6i   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *HotelRoom.Number+6i     5�      $ Этаж��6-   $	 20* 6f    6;   �� *HotelRoom.Floor.Number+6f     5�      $ 
Город��6-   $	 20* 6Z    6<   �� *	City.Name+6Z     5�      $3 -Длительность проживания��6-   $	 25* 6k    6N   �� *
Duration+6k     5�      $% Дата регистрации��6-   $	 25* 6�    6G   �� *RegistrationDate+  �� 	Converter#  [ 6�     5�      $% Стоимость (сутки)��6-   $	 20* 6z    6H   �� *HotelRoom.TypeHotelRoom.Price+6z     5�      $% Стоимость (всего)��6-   $	 20* 6�    6H   �� #  [ 6�   5�      �� 5�      $��3��6   $��2��6&   $��
Выход��5�      ���#844EFF! �� IsCancel! . $��20q�5�      $	��250��5�      5�      5�      5�      5�      