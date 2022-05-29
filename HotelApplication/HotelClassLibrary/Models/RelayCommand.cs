using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelClassLibrary.Models
{
    // Класс Команда
    public class RelayCommand : ICommand
    {
        // фукнция
        private Action<object> _execute;

        // проверка условия возможности исполнения
        private Func<object, bool> _canExecute;

        // событие проверки условия возможности исполнения команды
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; 

            remove => CommandManager.RequerySuggested -= value;
        }


        #region Конструкторы

        // конструктор по умолчанию
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // проверка условия возможности исполнения команды
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        #endregion

        #region Методы

        #endregion
    }
}
