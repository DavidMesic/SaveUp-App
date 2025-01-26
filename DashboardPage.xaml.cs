using Microcharts;
using SkiaSharp;
using System.Globalization;

namespace SaveUpApp
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();

            // Abonniere Änderungen im Produktrepository
            ProductRepository.ProductsChanged += OnProductsChanged;
        }

        private void OnProductsChanged(object sender, EventArgs e)
        {
            // Aktualisiere die Chart-Daten, wenn Produkte geändert werden
            LoadChartData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Aktualisiere die Chart-Daten, wenn die Seite angezeigt wird
            LoadChartData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Abonniere Änderungen ab, wenn die Seite verschwindet
            ProductRepository.ProductsChanged -= OnProductsChanged;
        }

        private void LoadChartData()
        {
            // Daten aus dem Repository abrufen
            var products = ProductRepository.GetProducts();

            if (products == null || !products.Any())
            {
                DisplayAlert("Fehler", "Es sind keine Produkte verfügbar.", "OK");
                return;
            }

            // Debugging: Anzahl der geladenen Produkte anzeigen
            Console.WriteLine($"Anzahl der geladenen Produkte: {products.Count}");

            // Überprüfe, ob die Produkte gültige Daten haben
            var validProducts = products
                .Where(p => p.DateSaved != default && p.Price > 0)
                .ToList();

            if (!validProducts.Any())
            {
                DisplayAlert("Fehler", "Es gibt keine gültigen Produkte mit Datum und Preis.", "OK");
                return;
            }

            // Filtere Produkte der letzten 7 Tage und gruppiere sie nach Datum
            var last7Days = validProducts
                .Where(p => p.DateSaved >= DateTime.Now.AddDays(-7))
                .GroupBy(p => p.DateSaved.Date)
                .Select(g => new SavingsEntry
                {
                    Date = g.Key,
                    Amount = g.Sum(p => p.Price)
                })
                .OrderBy(entry => entry.Date)
                .ToList();

            if (!last7Days.Any())
            {
                DisplayAlert("Fehler", "Keine Daten für die letzten 7 Tage gefunden.", "OK");
                return;
            }

            // Konvertiere die Daten für die Anzeige im Balkendiagramm
            var chartEntries = last7Days.Select(entry => new ChartEntry(entry.Amount)
            {
                Label = entry.Date.ToString("ddd"), // Wochentag (z. B. "Mo", "Di")
                ValueLabel = $"{entry.Amount.ToString("C", new CultureInfo("de-CH"))}", // Währungsformat für CHF
                Color = SKColor.Parse("#3498db")
            }).ToList();

            // Debugging: Zeige die Chart-Daten an
            Console.WriteLine("Chart-Daten:");
            chartEntries.ForEach(entry =>
                Console.WriteLine($"Label: {entry.Label}, Wert: {entry.ValueLabel}")
            );

            // Erstelle das Balkendiagramm
            var chart = new BarChart
            {
                Entries = chartEntries,
                BackgroundColor = SKColors.Transparent,
                LabelTextSize = 30,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                BarAreaAlpha = 150 // Transparenz der Balken
            };

            // Weisen das Diagramm der ChartView zu
            SavingsChart.Chart = chart;
        }

        public class SavingsEntry
        {
            public DateTime Date { get; set; }
            public float Amount { get; set; }
        }
    }
}