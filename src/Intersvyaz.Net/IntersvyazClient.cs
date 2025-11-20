using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Intersvyaz.Net.Models;
using Intersvyaz.Net.Common;
using Newtonsoft.Json;

namespace Intersvyaz.Net
{
    public class IntersvyazClient
    {
        /// <summary>
        /// URL-адрес API.
        /// </summary>
        private readonly Uri API_URI = new Uri("https://api.is74.ru");
        
        /// <summary>
        /// Клиент HTTP.
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Событие при успешном входе в систему.
        /// </summary>
        public event EventHandler<LoginResponseDto> OnLogin;

        public IntersvyazClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = API_URI,
            };

            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("Intersvyaz.Net", "1.0"));
            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("(+https://github.com/synzr/intersvyaz)"));
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Ответ от системы.</returns>
        public async Task<LoginResponseDto> Login(string username, string password)
        {
            var content = new JsonContent(new LoginRequestDto()
            {
                Username = username,
                Password = password,
            });

            using (var message = await _httpClient.PostAsync("/auth/mobile", content))
            {
                var data = await message.Content.ReadAsStringAsync();

                if ((int)message.StatusCode == 422) // NOTE: Data Validation Error
                {
                    var errors = JsonConvert.DeserializeObject<ValidationErrorDto[]>(data);
                    throw new InvalidOperationException(string.Join("; ", errors.Select(e => e.Message)));
                }

                if (!message.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Неизвестная ошибка сервера");
                }

                var result = JsonConvert.DeserializeObject<LoginResponseDto>(data);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Token);
                OnLogin?.Invoke(this, result);

                return result;
            }
        }
    }
}
