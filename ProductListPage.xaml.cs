using SaveUpApp.ViewModels;


namespace SaveUpApp
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
            BindingContext = new ProductListViewModel();
        }
    }
}