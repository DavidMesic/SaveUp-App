using SaveUpApp.Interfaces;
using SaveUpApp.ViewModels;
using System.Windows.Input;

namespace SaveUpApp
{
    public partial class MainPage : ContentPage
    {
        public ICommand ClearAllCommand { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            // AlertService an das ViewModel übergeben
            BindingContext = new MainViewModel(DependencyService.Get<IAlertService>());
        }
    }

    public class AlertService : IAlertService
    {
        public async Task<bool> ShowConfirmation(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task ShowMessage(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}