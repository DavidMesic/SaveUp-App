using System.Collections.ObjectModel;
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

        // Produkte laden und nach Datum sortieren (neueste zuerst)
        public void LoadProducts()
        {
            Products.Clear();
            var products = ProductRepository.GetProducts()
                .OrderByDescending(p => p.DateSaved) // Sortiere nach Datum (neueste zuerst)
                .ToList();

            foreach (var product in products)
            {
                Products.Add(product);
            }

            OnPropertyChanged(nameof(TotalSum));
        }
    }
}