using System.Collections.ObjectModel;
using System.Linq;
using SaveUpApp.Models;

namespace SaveUpApp.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; private set; }
        public double TotalSum => Products.Sum(p => p.Price);

        public ProductListViewModel()
        {
            Products = new ObservableCollection<Product>();
        }

        // Eigene LoadProducts-Methode, weil die aktualisierung im CodeBehind sein muss
        public void LoadProducts()
        {
            Products.Clear();
            var products = ProductRepository.GetProducts();
            foreach (var product in products)
            {
                Products.Add(product);
            }
            OnPropertyChanged(nameof(TotalSum));
        }
    }
}