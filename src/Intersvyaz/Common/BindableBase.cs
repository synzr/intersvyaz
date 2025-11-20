using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Intersvyaz.Common
{
    public class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие об изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Установка значения в заднее поля свойства с вызовом события.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="back">Заднее поле свойства.</param>
        /// <param name="value">Новое значение.</param>
        /// <param name="propertyName">Имя свойства. (Необязательный параметр)</param>
        /// <remarks>При компилиции, имя свойства будет автоматически заполнено.</remarks>
        protected void SetProperty<T>(ref T back, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(back, value))
            {
                back = value;
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Вызов события об изменении
        /// </summary>
        /// <param name="propertyName">Имя свойства. (Необязательный параметр)</param>
        /// <remarks>При компилиции, имя свойства будет автоматически заполнено.</remarks>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
