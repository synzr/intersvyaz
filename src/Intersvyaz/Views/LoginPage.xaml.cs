using Windows.UI.Xaml.Controls;
using Intersvyaz.ViewModels;

namespace Intersvyaz.Views
{
    public sealed partial class LoginPage : Page
    {
        /// <summary>
        /// Модель представления страницы входа.
        /// </summary>
        public LoginViewModel ViewModel { get; }

        public LoginPage()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetInstance<LoginViewModel>();
        }
    }
}
