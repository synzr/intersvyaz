using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Intersvyaz.Common
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие команды.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Действие проверки «можно ли исполнить команду?».
        /// </summary>
        private readonly Predicate<object> _canExecute = null;

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Вызов события изменения состояния.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
