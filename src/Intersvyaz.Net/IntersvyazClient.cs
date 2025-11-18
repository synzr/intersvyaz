using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Intersvyaz.Net.Models;
using Newtonsoft.Json;

namespace Intersvyaz.Net
{
    public class IntersvyazClient
    {
        private readonly Uri BASE_ADDRESS = new Uri("https://api.is74.ru");

        private HttpClient httpClient;

        public IntersvyazClient()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = BASE_ADDRESS,
            };

            // TODO: Добавить здесь ссылку на репозитории.
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("Intersvyaz.Net", "1.0"));
        }

        public IntersvyazClient(string token) : base()
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<LoginResponseDto> Login(string username, string password)
        {
            var content = new JsonContent(new LoginRequestDto()
            {
                Username = username,
                Password = password,
            });

            using (var message = await httpClient.PostAsync("/auth/mobile", content))
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

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Token);

                return result;
            }
        }
    }
}
