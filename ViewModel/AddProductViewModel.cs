using SaveUpApp.Models;
using System.Windows.Input;

namespace SaveUpApp.ViewModels
{
    public class AddProductViewModel : BaseViewModel
    {
        public string? Description { get; set; }
        public string? Price { get; set; }
        public ICommand SaveProductCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public AddProductViewModel()
        {
            SaveProductCommand = new Command(SaveProduct);
            NavigateBackCommand = new Command(() => Shell.Current.GoToAsync("///MainPage"));
        }

        private void SaveProduct()
        {
            if (!string.IsNullOrWhiteSpace(Description) && float.TryParse(Price, out float price))
            {
                var product = new Product { Description = Description, Price = price };
                ProductRepository.AddProduct(product);
                Shell.Current.GoToAsync("///ProductListPage");
            }
        }
    }
}