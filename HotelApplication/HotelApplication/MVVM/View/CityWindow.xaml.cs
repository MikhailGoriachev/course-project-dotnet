using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using HotelClassLibrary.Models;         // модели


namespace HotelApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        // город
        public City CurrentCity { get; set; }


        #region Конструкторы

        // конструктор о умолчанию - Запуск в режиме создания
        public CityWindow()
        {
            InitializeComponent();

            // установка контекста
            this.DataContext = CurrentCity = new City();
        }


        // конструктор инициализирующий - Запуск в режиме редактирования
        public CityWindow(City city)
        {
            InitializeComponent();

            CurrentCity = city;

            // получение объекта из контекста
            this.DataContext = new City() { Name = city.Name };

            // установка текстовок
            LblHeader.Content = "Редактирование город";
            BtnOK.Content = "Сохранить";
        }

        #endregion


        #region Методы

        // кнопка "Добавить/Изменить"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // контекст
            City city = this.DataContext as City;

            // установка значений полей
            CurrentCity.Name = city.Name;

            // установка результата и закрытие окна
            this.DialogResult = true;
            this.Close();
        }

        #endregion
    }
}
