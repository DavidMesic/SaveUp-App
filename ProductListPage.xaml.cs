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

            // Aktualisiere die Gesamtsumme, wenn sich die Produktliste ändert
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProductListViewModel.TotalSum))
                {
                    // Gesamtsumme fix aktualisieren, falls notwendig
                    TotalSumLabel.Text = $"Gesamtersparnis: {viewModel.TotalSum:C}";
                }
            };
        }

        // OnAppearing sorgt für die Aktualisierung der Produkte
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts();
        }
    }
}