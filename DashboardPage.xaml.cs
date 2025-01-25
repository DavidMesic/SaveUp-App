namespace SaveUpApp;

using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using SaveUpApp.ViewModels;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
        LoadChartData();
    }

    private void LoadChartData()
    {
        // Daten aus dem Repository abrufen
        var products = ProductRepository.GetProducts();

        if (products == null || !products.Any())
        {
            // Zeige eine Fehlermeldung an, wenn keine Daten vorhanden sind
            DisplayAlert("Fehler", "Es sind keine Produkte verfügbar.", "OK");
            return;
        }

        // Debugging: Zeige die Anzahl der geladenen Produkte an
        Console.WriteLine($"Anzahl der geladenen Produkte: {products.Count}");

        // Validierung: Überprüfe, ob die notwendigen Felder vorhanden sind
        var validProducts = products.Where(p => p.DateSaved != default && p.Price > 0).ToList();

        if (!validProducts.Any())
        {
            DisplayAlert("Fehler", "Es gibt keine gültigen Produkte mit Datum und Preis.", "OK");
            return;
        }

        // Debugging: Zeige die gültigen Produkte an
        foreach (var product in validProducts)
        {
            Console.WriteLine($"Produkt: {product.Description}, Preis: {product.Price}, Datum: {product.DateSaved}");
        }

        // Filtere die letzten 7 Tage
        var last7Days = validProducts
            .Where(p => p.DateSaved >= DateTime.Now.AddDays(-7))
            .GroupBy(p => p.DateSaved.Date) // Gruppiere nach Datum
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

        // Debugging: Zeige die gefilterten Daten an
        foreach (var entry in last7Days)
        {
            Console.WriteLine($"Datum: {entry.Date}, Betrag: {entry.Amount}");
        }

        // Konvertiere die Daten für Microcharts
        var chartEntries = last7Days.Select(entry => new ChartEntry(entry.Amount)
        {
            Label = entry.Date.ToString("ddd"),
            ValueLabel = $"{entry.Amount}€",
            Color = SKColor.Parse("#3498db")
        }).ToList();

        // Debugging: Zeige die Chart-Einträge an
        foreach (var chartEntry in chartEntries)
        {
            Console.WriteLine($"Label: {chartEntry.Label}, Wert: {chartEntry.ValueLabel}");
        }

        // Erstelle ein Balkendiagramm
        var chart = new BarChart
        {
            Entries = chartEntries,
            LabelTextSize = 30,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelOrientation = Orientation.Horizontal
        };

        // Weise das Diagramm der ChartView zu
        SavingsChart.Chart = chart;
    }

    public class SavingsEntry
    {
        public DateTime Date { get; set; }
        public float Amount { get; set; }
    }
}