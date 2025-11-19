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
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Данные об сессии.</returns>
        SessionData GetSession(string username);

        /// <summary>
        /// Проверить, есть ли сессия пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        bool HasSession(string username);

        /// <summary>
        /// Удалить сессию пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        void DeleteSession(string username);
    }
}
