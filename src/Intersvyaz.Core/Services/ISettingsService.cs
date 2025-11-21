using System;

namespace Intersvyaz.Core.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Событие об изменении текущего пользователя.
        /// </summary>
        event EventHandler<string> CurrentUserChanged;

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <returns>Имя пользователя.</returns>
        string GetCurrentUser();

        /// <summary>
        /// Установить нового текущего пользователя.
        /// </summary>
        /// <param name="value">Имя нового пользователя.</param>
        void SetCurrentUser(string value);
    }
}
