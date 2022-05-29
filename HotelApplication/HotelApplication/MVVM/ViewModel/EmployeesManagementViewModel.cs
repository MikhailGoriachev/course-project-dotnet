using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

using HotelClassLibrary.Controllers;        // контроллер
using HotelClassLibrary.Models;             // модели
using HotelApplication.MVVM.View;


namespace HotelApplication.MVVM.ViewModel
{
    // Класс Модель представления окна "Служащие гостиницы"
    public class EmployeesManagementViewModel : ObservableObject
    {
        // количество работников
        public int CountEmployees => Employees.Count; 

        // коллекция работников
        private BindingList<Employee> _employees = HotelController.GetEmployeesBindingList();
        public BindingList<Employee> Employees => _employees;


        // выбранный работник в таблице
        public Employee SelectedEmployee { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public EmployeesManagementViewModel()
        {
            // подписка на события добавления и удаления работника
            Employees.ListChanged += (sender, e) => { OnPropertyChanged("CountEmployees"); };
        }

        #endregion


        #region Команды

        // добавление работника
        private RelayCommand _addEmployeeCommand;

        public RelayCommand AddEmployeeCommand => _addEmployeeCommand ?? (_addEmployeeCommand = new RelayCommand((o) =>
        {
            // окно
            EmployeeWindow window = new EmployeeWindow();

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // добавление записи
            HotelController.AddEmployee(window.CurrentEmployee);

            // сохранение добавленной записи
            HotelController.SaveChanges();

            // сохранение графика
            window.SaveShedule();
        }));

        // изменение выбранного работника
        private RelayCommand _editEmployeeCommand;

        public RelayCommand EditEmployeeCommand => _editEmployeeCommand ?? (_editEmployeeCommand = new RelayCommand((o) =>
        {
            // изменение служащего
            //SelectedEmployee.Person.Patronymic = "MOD";

            // окно
            EmployeeWindow window = new EmployeeWindow(SelectedEmployee);

            // запуск окна
            if (window.ShowDialog() != true)
                return;

            // сохранение изменений записи
            HotelController.SaveChanges();

            // сохранение графика
            window.SaveShedule();
        }, 
            (o) => SelectedEmployee != null));

        // удаление выбранного работника
        private RelayCommand _removeEmployeeCommand;

        public RelayCommand RemoveEmployeeCommand => _removeEmployeeCommand ?? (_removeEmployeeCommand = new RelayCommand((o) =>
        {
            // увольнение служащего
            HotelController.RemoveEmployee(SelectedEmployee);
        },
            (o) => SelectedEmployee != null));

        #endregion
    }
}
