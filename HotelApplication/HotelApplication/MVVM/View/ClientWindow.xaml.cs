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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        // клиент
        public Client CurrentClient { get; set; }


        #region Конструкторы

        // конструктор о умолчанию - Запуск в режиме создания
        public ClientWindow()
        {
            InitializeComponent();

            // установка контекста
            this.DataContext = CurrentClient = new Client { Person = new Person() };
        }


        // конструктор инициализирующий - Запуск в режиме редактирования
        public ClientWindow(Client client)
        {
            InitializeComponent();

            CurrentClient = client;

            // получение объекта из контекста
            this.DataContext = new Client()
            {
                Person = new Person { Surname = client.Person.Surname, Name = client.Person.Name, Patronymic = client.Person.Patronymic },
                Passport = client.Passport,
                IsDeleted = client.IsDeleted
            };

            // установка текстовок
            LblHeader.Content = "Редактирование клиента";
            BtnOK.Content = "Сохранить";
        }

        #endregion


        #region Методы

        // кнопка "Добавить/Изменить"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // контекст
            Client client = this.DataContext as Client;

            // установка значений полей
            CurrentClient.Person.Surname = client.Person.Surname;
            CurrentClient.Person.Name = client.Person.Name;
            CurrentClient.Person.Patronymic = client.Person.Patronymic;
            CurrentClient.Person.IsDeleted = client.Person.IsDeleted;
            CurrentClient.Passport = client.Passport;
            CurrentClient.IsDeleted = client.IsDeleted;

            // установка результата и закрытие окна
            this.DialogResult = true;
            this.Close();
        }

        #endregion
    }
}
