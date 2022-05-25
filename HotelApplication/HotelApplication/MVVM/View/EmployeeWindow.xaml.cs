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

using HotelClassLibrary.Controllers;        // контроллеры
using HotelClassLibrary.Models;             // модели


namespace HotelApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        // рабочий
        public Employee CurrentEmployee { get; set; }


        #region Конструкторы

        // конструктор о умолчанию - Запуск в режиме создания
        public EmployeeWindow()
        {
            InitializeComponent();

            // установка контекста
            this.DataContext = CurrentEmployee = new Employee { Person = new Person() };

            // заполнение графика из полученных данных
            SetShedule(new List<CleaningSchedule>());
        }


        // конструктор инициализирующий - Запуск в режиме редактирования
        public EmployeeWindow(Employee employee)
        {
            InitializeComponent();


            CurrentEmployee = employee;

            // получение объекта из контекста
            this.DataContext = new Employee() { Person = new Person { Surname = employee.Person.Surname, Name = employee.Person.Name, Patronymic = employee.Person.Patronymic },
                                                IsDeleted = employee.IsDeleted };

            // заполнение графика из полученных данных
            SetShedule(HotelController.GetSheduleEmployee(employee));

            // установка текстовок
            LblHeader.Content = "Редактирование работника";
            BtnOK.Content = "Сохранить";
        }

        #endregion


        #region Методы

        // кнопка "Добавить/Изменить"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // контекст
            Employee employee= this.DataContext as Employee;

            // установка значений полей
            CurrentEmployee.Person.Surname      = employee.Person.Surname;
            CurrentEmployee.Person.Name         = employee.Person.Name;
            CurrentEmployee.Person.Patronymic   = employee.Person.Patronymic;
            CurrentEmployee.Person.IsDeleted    = employee.Person.IsDeleted;
            CurrentEmployee.IsDeleted           = employee.IsDeleted;

            // установка результата и закрытие окна
            this.DialogResult = true;
            this.Close();
        }


        // заполнение графика из полученных данных
        private void SetShedule(List<CleaningSchedule> list)
        {
            // коллекция контролов
            List<(CheckBox checkBox, ComboBox comboBox)> controls = new List<(CheckBox checkBox, ComboBox comboBox)>
            {
                (ChbMonday, CmbMonday),
                (ChbTuesday, CmbTuesday),
                (ChbWednesday, CmbWednesday),
                (ChbThursday, CmbThursday),
                (ChbFriday, CmbFriday),
                (ChbSaturday, CmbSaturday),
                (ChbSunday, CmbSunday),
            };

            // этажи
            int[] floor = HotelController.GetFloors().Select(i => i.Number).ToArray();

            // установка этажей в comboBox
            controls.ForEach(c => { c.comboBox.ItemsSource = floor; c.comboBox.SelectedIndex = 0; });

            // установка выбранных дней
            list.ForEach(i => { var c = controls[i.DayOfWeek.Number - 1]; c.checkBox.IsChecked = true; c.comboBox.SelectedItem = i.Floor.Number; });
        }


        // получение графика из полей
        public void SaveShedule()
        {
            // коллекция контролов
            List<(CheckBox checkBox, ComboBox comboBox)> controls = new List<(CheckBox checkBox, ComboBox comboBox)>
            {
                (ChbMonday, CmbMonday),
                (ChbTuesday, CmbTuesday),
                (ChbWednesday, CmbWednesday),
                (ChbThursday, CmbThursday),
                (ChbFriday, CmbFriday),
                (ChbSaturday, CmbSaturday),
                (ChbSunday, CmbSunday),
            };

            // этажи
            //int[] floor = HotelController.GetFloors().Select(i => i.Number).ToArray();

            // установка этажей в comboBox
            //controls.ForEach(c => { c.comboBox.ItemsSource = floor; c.comboBox.SelectedIndex = 0; });

            // установка выбранных дней
            //list.ForEach(i => { var c = controls[i.DayOfWeek.Number - 1]; c.checkBox.IsChecked = true; c.comboBox.SelectedItem = i.Floor.Number; });

            // получить список отмеченных дней и этажей
            HotelController.SetSheduleEmployee(CurrentEmployee, controls.Where(c => c.checkBox.IsChecked == true)
                                                                        .Select(c => (controls.IndexOf(c) + 1, (int)c.comboBox.SelectedValue))
                                                                        .ToList());
        }

        #endregion
    }
}
