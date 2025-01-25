using SaveUpApp.ViewModels;


namespace SaveUpApp
{
    public partial class ProductListPage : ContentPage
    {
        private ProductListViewModel viewModel;

        public ProductListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductListViewModel();
        }

        // OnAppearing-Methode sorgt für die Aktualisierung, wenn sich die Page öffnet und nicht wenn sie initialisiert wird
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts();
        }
    }
}