using System;
using Windows.Storage;

namespace Intersvyaz.Core.Services
{
    public class SettingsService : ISettingsService
    {
        /// <inheritdoc />
        public event EventHandler<string> CurrentUserChanged;

        /// <inheritdoc />
        public string GetCurrentUser()
        {
            return (string)ApplicationData.Current.LocalSettings.Values["CurrentUser"] ?? null;
        }

        /// <inheritdoc />
        public void SetCurrentUser(string value)
        {
            ApplicationData.Current.LocalSettings.Values["CurrentUser"] = value;
            CurrentUserChanged?.Invoke(this, value);
        }
    }
}
