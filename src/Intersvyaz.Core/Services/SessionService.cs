using System;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Intersvyaz.Net;
using Intersvyaz.Core.Models;
using Windows.Security.Credentials;

namespace Intersvyaz.Core.Services
{
    public class SessionService : ISessionService
    {
        /// <summary>
        /// Имя ресурса.
        /// </summary>
        private const string RESOURCE_NAME = "Intersvyaz";

        /// <summary>
        /// Клиент Интерсвязи.
        /// </summary>
        private IntersvyazClient _intersvyazClient;

        /// <summary>
        /// Хранилище паролей.
        /// </summary>
        private PasswordVault _passwordVault;

        public SessionService(IntersvyazClient intersvyazClient)
        {
            _intersvyazClient = intersvyazClient;
            _passwordVault = new PasswordVault();
        }

        /// <inheritdoc />
        public async Task<SessionData> GetSessionAsync(string username, string password)
        {
            try
            {
                return GetSession(username);
            }
            catch
            {
                return await LoginAndSaveSession(username, password);
            }
        }

        /// <inheritdoc />
        public SessionData GetSession(string username)
        {
            var passwordCredential = _passwordVault.Retrieve(RESOURCE_NAME, username);
            return ConvertToSessionData(passwordCredential);
        }

        /// <inheritdoc />
        public bool HasSession(string username)
        {
            var passwordCredentials = _passwordVault.FindAllByResource(RESOURCE_NAME);

            var count = passwordCredentials
                .Where(passwordCredential => passwordCredential.UserName == username)
                .Count();
            return count > 0;
        }

        /// <inheritdoc />
        public void DeleteSession(string username)
        {
            var passwordCredential = _passwordVault.Retrieve(RESOURCE_NAME, username);
            _passwordVault.Remove(passwordCredential);
        }

        /// <summary>
        /// Войти в систему и сохранить сессию пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Данные об сессии.</returns>
        private async Task<SessionData> LoginAndSaveSession(string username, string password)
        {
            var responseDto = await _intersvyazClient.Login(username, password);

            var sessionData = new SessionData()
            {
                Token = responseDto.Token,
                ExpiresAt = responseDto.AccessEnd,
                Username = username,
            };
            var passwordCredential = ConvertToPasswordCredential(sessionData);

            _passwordVault.Add(passwordCredential);
            return sessionData;
        }

        /// <summary>
        /// Сконвертировать нативную учетчую запсть в SessionData.
        /// </summary>
        /// <param name="passwordCredential">Учетная запись.</param>
        /// <returns>Данные об сессии.</returns>
        private SessionData ConvertToSessionData(PasswordCredential passwordCredential)
        {
            var entries = passwordCredential.Password.Split(
                new string[] { ";" }, 2, StringSplitOptions.RemoveEmptyEntries);

            return new SessionData()
            {
                Token = entries[0],
                ExpiresAt = DateTime.Parse(entries[1], CultureInfo.InvariantCulture),
                Username = passwordCredential.UserName,
            };
        }

        /// <summary>
        /// Сконвертировать SessionData в нативную учетную запись.
        /// </summary>
        /// <param name="sessionData">Данные об сессии.</param>
        /// <returns>Нативная учетная запись.</returns>
        private PasswordCredential ConvertToPasswordCredential(SessionData sessionData)
        {
            var entries = new string[]
            {
                sessionData.Token,
                sessionData.ExpiresAt.ToString(CultureInfo.InvariantCulture),
            };

            return new PasswordCredential(
                RESOURCE_NAME, sessionData.Username, string.Join(";", entries));
        }
    }
}
