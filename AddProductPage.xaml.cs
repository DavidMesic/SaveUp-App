using SaveUpApp.ViewModels;

namespace SaveUpApp
{
    public partial class AddProductPage : ContentPage
    {
        public AddProductPage()
        {
            InitializeComponent();
            BindingContext = new AddProductViewModel();

        }
    }
}