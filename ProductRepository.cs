using System.Text.Json;
using SaveUpApp.Models;

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
            // Lade Produkte aus der Datei (bei Bedarf)
            LoadProducts();
            return products;
        }

        public static void AddProduct(Product product)
        {
            // Produkt hinzufügen und speichern
            products.Add(product);
            SaveProducts();
            OnProductsChanged(); // Event auslösen
        }

        public static void RemoveProduct(Product product)
        {
            // Produkt entfernen und speichern
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
            // Produkte in JSON speichern
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(FilePath, json);
        }

        private static void LoadProducts()
        {
            // Produkte aus JSON laden
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

        private static void OnProductsChanged()
        {
            // Event auslösen, um Änderungen zu signalisieren
            ProductsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}