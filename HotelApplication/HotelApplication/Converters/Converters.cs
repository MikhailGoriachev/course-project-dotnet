using HotelApplication.Models;
using HotelClassLibrary.Controllers;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HotelApplication.Converters
{
    // Консвертер возвращает обратное значение Bool
    public class BackBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает "уволен"/"работает"
    public class EmployeeIsDeletedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? "Уволен" : "Работает";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает "удалён"/"активен"
    public class IsDeletedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? "Удалён" : "Активен";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает строковое представление клиента
    public class ClientToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // если значение null
            if (value == null)
                return null;

            // клиент
            Client client = value as Client;

            return $"{client.Person.Surname} {client.Person.Name} {client.Person.Patronymic} - {client.Passport}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Конвертер возвращает строковое представление комнаты
    public class HotelRoomToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // если значение null
            if (value == null)
                return null;

            // комната
            HotelRoom room = value as HotelRoom;

            // получить количество занятых мест в комнате
            int count = HotelController.CountBusyPlace(room, DateTime.Now);

            return $"{room.TypeHotelRoom.Name}: №{room.Number} {count}/{room.TypeHotelRoom.CountPlace}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }    
    
    // Конвертер возвращает строковое мест (занято/всего) комнаты
    public class HotelRoomPlaceToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // если значение null
            if (value == null)
                return null;

            // id комнаты
            int id = (int)value;

            // комната
            HotelRoom room = HotelController.GetHotelRooms().FirstOrDefault(r => r.Id == id);

            // получить количество занятых мест в комнате
            int count = HotelController.CountBusyPlace(room, DateTime.Now);

            return $"{count}/{room.TypeHotelRoom.CountPlace}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }    
    
    // Конвертер возвращает строковое состояний окна ("Добавить"/"Сохранить")
    public class WindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // если значение null
            if (value == null)
                return null;

            // комната
            WindowState state = (WindowState)value;

            switch (state)
            {
                case WindowState.CreateState:
                    return "Создать";
                case WindowState.EditState:
                    return "Сохранить";
                case WindowState.AddState:
                    return "Добавить";
                case WindowState.RemoveState:
                    return "Удалить";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает строковое представление даты
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // если значение null
            if (value == null)
                return null;

            return $"{(DateTime)value:d}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }    


    // Конвертер отнимает от числа значения возвращает строковое представление
    public class DoubleSubtractionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value.ToString()) - double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает строковое значение Collapsed если значение равно WindowState.AddState, иначе Visible
    public class AddWindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (WindowState)value == WindowState.AddState ? "Visible" : "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Конвертер возвращает стоимость за проживание клиента
    public class CoastRegistrationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            // приведение записи
            HistoryRegistrationHotel registration = value as HistoryRegistrationHotel;

            return registration.Duration * registration.HotelRoom.TypeHotelRoom.Price;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
