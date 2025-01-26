using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SaveUpApp.Models;
using System.Collections.Generic;

namespace SaveUpApp
{
    public static class ProductRepository
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "products.json");
        private static List<Product> products = new();

        // Event zur Benachrichtigung bei Änderungen
        public static event EventHandler ProductsChanged;

        public static List<Product> GetProducts()
        {
            LoadProducts();
            return products;
        }

        public static void AddProduct(Product product)
        {
            products.Add(product);
            SaveProducts();
            OnProductsChanged(); // Event auslösen
        }

        public static void RemoveProduct(Product product)
        {
            products.Remove(product);
            SaveProducts();
            OnProductsChanged(); // Event auslösen
        }

        public static void ClearAllProducts()
        {
            products.Clear();
            SaveProducts();
            OnProductsChanged(); // Event auslösen
        }

        private static void SaveProducts()
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(FilePath, json);
        }

        private static void LoadProducts()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath) ?? string.Empty;
                products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            else
            {
                products = new List<Product>();
            }
        }

        // Methode zum Auslösen des Events
        private static void OnProductsChanged()
        {
            ProductsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}