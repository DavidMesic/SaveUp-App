using SaveUpApp.Interfaces;
using System.Windows.Input;

namespace SaveUpApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToAddProductCommand { get; }
        public ICommand NavigateToProductListCommand { get; }
        public ICommand NavigateToAboutUsCommand { get; }
        public ICommand ClearAllCommand { get; }

        public MainViewModel(IAlertService alertService)
        {
            _alertService = alertService;

            NavigateToDashboardCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///DashboardPage");
            });

            NavigateToAddProductCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///AddProductPage");
            });

            NavigateToProductListCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///ProductListPage");
            });

            NavigateToAboutUsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///AboutUsPage");
            });

            ClearAllCommand = new Command(async () =>
            {
                // Popup f�r die Best�tigung
                bool confirm = await _alertService.ShowConfirmation(
                    "Best�tigen",
                    "M�chten Sie wirklich die gesamte Liste l�schen?",
                    "Ja",
                    "Nein"
                );

                if (confirm)
                {
                    // Liste l�schen
                    ProductRepository.ClearAllProducts();

                    // Erfolgsmeldung anzeigen
                    await _alertService.ShowMessage("Erfolg", "Die Liste wurde erfolgreich zur�ckgesetzt.", "OK");
                }
            });
        }
    }
}