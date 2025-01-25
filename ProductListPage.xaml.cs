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

        // OnAppearing-Methode sorgt f�r die Aktualisierung, wenn sich die Page �ffnet und nicht wenn sie initialisiert wird
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts();
        }
    }
}