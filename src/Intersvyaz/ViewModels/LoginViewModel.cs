using Intersvyaz.Common;
using Intersvyaz.Core.Services;
using System.Diagnostics;

namespace Intersvyaz.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        /// <summary>
        /// Сервис сессии пользователя.
        /// </summary>
        private readonly ISessionService _sessionService;
        
        /// <summary>
        /// Заднее поле имени пользователя.
        /// </summary>
        private string _username;

        /// <summary>
        /// Заднее поле пароля пользователя.
        /// </summary>
        private string _password;

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Команда входа.
        /// </summary>
        public RelayCommand LoginCommand { get; }

        public LoginViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
            
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            Username = Password = string.Empty;
        }

        private async void ExecuteLoginCommand(object _)
        {
            var sessionData = await _sessionService.GetSessionAsync(Username, Password);
            
            // TODO: Обработка данных об сессии
        }

        private bool CanExecuteLoginCommand(object _)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
    }
}
