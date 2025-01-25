using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SaveUpApp.Models;
using System.Threading.Tasks;

namespace SaveUpApp.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; }
        public double TotalSum => Products.Sum(p => p.Price);

        public ProductListViewModel()
        {
            Products = new ObservableCollection<Product>(ProductRepository.GetProducts());
        }
    }
}