using System;
using System.Collections.Generic;
using System.Linq;
using PDFExporterLib;

namespace UsePDFExport
{
    class Program
    {
        static void Main()
        {
            List<SalesReport> reports = new List<SalesReport>(){new SalesReport()
                {
                    Date = DateTime.Today,
                    Products = new List<string> { "Beer1", "Beer2", "Beer3", "Beer4" },
                    Quantities = new List<string> { "40 liters", "50 liters", "20 liters", "10 liters" },
                    UnitPrices = new List<decimal> { 12, 3.9m, 2.5m, 1.5m },
                    Locations = new List<string> { "Supermarket 'Kaspichan – Center'", "Supermarket 'Bourgas – Plaza'",
                        "Supermarket 'Bay Ivan' – Zmeyovo", "Supermarket 'Bourgas – Plaza'" },
                    Sums = new List<decimal> { 120, 30.9m, 20.5m, 10.5m },
                }, new SalesReport()
                {
                    Date = DateTime.Today,
                    Products = new List<string> { "Beer6", "Beer7", "Beer8", "Beer9" },
                    Quantities = new List<string> { "40 liters", "50 liters", "20 liters", "10 liters" },
                    UnitPrices = new List<decimal> { 12, 3.9m, 2.5m, 1.5m },
                    Locations = new List<string> { "Supermarket 'Kaspichan – Center'", "Supermarket 'Bourgas – Plaza'",
                        "Supermarket 'Bay Ivan' – Zmeyovo", "Supermarket 'Bourgas – Plaza'" },
                    Sums = new List<decimal> { 120, 30.9m, 20.5m,10.5m },
                }
            };

            var newReport = new SalesReport()
                {
                    Date = DateTime.Now,
                    Products = new List<string> { "Beer1", "Beer2", "Beer3", "Beer4" },
                    Quantities = new List<string> { "40 liters", "50 liters", "20 liters", "10 liters" },
                    UnitPrices = new List<decimal> { 12, 3.9m, 2.5m, 1.5m },
                    Locations = new List<string> { "Supermarket 'Kaspichan – Center'", "Supermarket 'Bourgas – Plaza'",
                        "Supermarket 'Bay Ivan' – Zmeyovo", "Supermarket 'Bourgas – Plaza'" },
                    Sums = new List<decimal> { 120, 30.9m, 20.5m, 10.5m }
                };
            reports.Add(newReport);

            PDFExporter.CreatePDF(reports);
        }
    }
}
