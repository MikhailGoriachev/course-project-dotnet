   M S B A M L   `   `   `  ����K  GHotelApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:HotelApplication.MVVM.ViewHotelApplication.MVVM.View  5   	   L HHotelClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null^Aclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibraryHotelClassLibrary.Models 5   	   I)clr-namespace:HotelApplication.ConvertersHotelApplication.Converters  5	   	   +    %HotelApplication.MVVM.View.CityWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:HotelApplication.MVVM.View     5   	   SmodelAclr-namespace:HotelClassLibrary.Models;assembly=HotelClassLibrary     5   	   :conv)clr-namespace:HotelApplication.Converters     5	   	     � Title$  
CityWindow��5
   	   $	��250��6   $	��400��6)    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ 
�������$��12(�5   	    � DataContext 5      #  HotelClassLibrary.Models.City  5   
    � 	Resources 5      3   -HotelApplication.Converters.BackBoolConverter   BackBoolConverter �� 
TargetType �� Property �� Value��%�  &	        '��      '�� �       5   
   6!   �� 5   
   " ��6   .�5       ��  � ��
 �����6+   �� 5!       � ��$ 14(�6)   �� 5"       � ��$ 0q�6'   5#      �� 5&   
   " ��6   .�5'      ��  � ��
 ���� 6+   �� 5(       � ��$ 1q�60   �� 5)       � ��$ 25��6'   �� 5*       � ��$ 0 0 0 20q�6'   �� 5+       � ��
 �����6+   �� 5,       � ��$ Center=�69   �� 5-       � ��$ 5 0 0 0q�6(   5.      50      .��53      �� ���#302E44	 �� CornerRadius$	 20��54      ._�55   
   � 
 � ColumnDefinitions
 57      �� 58       � RowDefinitions 5<      �� 5=      $��75 �6    �� 5>      �� 5?      $��80 �6    .+�5C      �� 
������ $��2��5D      �� 5G      -   5        $��	LblHeader��5G      $'��!Добавление города��6%   $��20(�5H      $��20q�5I      $��2��5J      $��Center=�5K      �� 5N      $��1��6   $��20q�6&   .+�5Q      ��  �� Orientation$ Vertical=�6   .+�5R      �� $��Название:��6   �� 5S      ��6   �� *Name+5T      5V      �� 5Z      $��2��6   $ 
Horizontal=�6&   .+�5]      �� -   5        $��BtnOK��5]      $��Добавить��6&   ���#4EFF91$��20q�5`      �� 5c      $��Отмена��6   ���#844EFF �� IsCancel . $��20q�5f      5h      5j      5k      5l      