using System.Threading.Tasks;
using Intersvyaz.Core.Models;

namespace Intersvyaz.Core.Services
{
    public interface ISessionService
    {
        /// <summary>
        /// Получить сессию пользователя. При отсутствие сессии,
        /// входит в систему автоматически.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Данные об сессии.</returns>
        Task<SessionData> GetSessionAsync(string username, string password);

        /// <summary>
        /// Получить сессию пользователя.
        /// </summary>
        /// <returns>Данные об сессии.</returns>
        SessionData GetSession();

        /// <summary>
        /// Проверить, есть ли сессия пользователя.
        /// </summary>
        bool HasSession();

        /// <summary>
        /// Удалить сессию пользователя.
        /// </summary>
        void DeleteSession();
    }
}
