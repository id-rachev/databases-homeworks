using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SupermarketEntityData;
using SupermarketEntityData.Migrations;
using SupermarketModel;
using Helper;
using MongoDB.Driver;

namespace SuperMarketClient
{
    class Program
    {
        const string Path = "..\\..\\..\\Reports\\";
        static void Main(string[] args)
        {
            Helper.MongoDb.ClearDatabases();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketContext, Configuration>());

            //Task 01
            AddReportsToDatabase(Path + "ZippedReports\\Sample-Sales-Reports.zip", 
                Path + "ExtractedExcelFiles\\");

            Console.WriteLine("Task 01 Completed - Data Transfered From MySql To Sql + Extracted Zip Data !");
            NextTask();
            //Task 02

            Helper.PDFExporter.CreatePDF(GetSalesReports());

            Console.WriteLine("PDF Created !");
            NextTask();
            //Task 03

            Helper.XMLProcessor.GenerateSalesReport(GetVendorsSalesReports());
            Console.WriteLine("XML Created !");
            NextTask();
           //Task 04

           Helper.MongoDb.InitConnection();
           using (var db = new SupermarketContext())
           {
              Helper.ProductReportGenerator.GenerateReport(db.Products.ToList());
           }
           Console.WriteLine("Added Data To Mongo !");
           NextTask();
            //Task 05

            //To Mongo
            var vendorExpenses = Helper.XMLProcessor.LoadVendorExpenses("Expenses.xml");
            Helper.MongoDb.SaveExpenses(vendorExpenses);

            //To SQl
            SaveVendorExpenses("Expenses.xml");
            Console.WriteLine("XML File Added To Mongo And SQL");
            NextTask();
            //Task 06

            Helper.ExcelWriter writer = new ExcelWriter();
            writer.CreateExcelFile();
            Console.WriteLine("Excel Report Created !");
        }

        static void SaveVendorExpenses(string path)
        {
            var data = Helper.XMLProcessor.LoadVendorExpenses(path);
            int currentVendorId;

            using (var db = new SupermarketContext())
            {
                foreach (var item in data)
                {
                    var list = db.Vendors.Where(x => x.VendorName == item.VendorName).ToList();

                    if (list == null || list.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        currentVendorId = list[0].VendorID;
                    }

                    db.VendorsExpenses.Add( 
                        new VendorsExpense{VendorId = currentVendorId, Date = item.Date, Expense = item.Expense});

                    db.SaveChanges();
                }
            }
        }

        static List<SalesReport> GetSalesReports()
        {
            List<SalesReport> reports = new List<SalesReport>();

            using (var db = new SupermarketContext())
            {
                var sales = db.Sales.GroupBy(x => x.Date);
                SalesReport currentReport;

                foreach (var date in sales)
                {
                    currentReport = new SalesReport { Date = (DateTime)date.Key };

                    foreach (var sale in date)
                    {
                        currentReport.Products.Add(sale.Product.ProductName);
                        currentReport.Quantities.Add(sale.Quantity + " " + sale.Product.Measure.MeasureName);
                        currentReport.UnitPrices.Add(sale.UnitPrice);
                        currentReport.Locations.Add(sale.Market.MarketName);
                        currentReport.Sums.Add(sale.Sum);
                    }

                    reports.Add(currentReport);
                }
            }

            return reports;
        }

        static List<VendorReport> GetVendorsSalesReports()
        {
            List<VendorReport> reports = new List<VendorReport>();

            using (var db = new SupermarketContext())
            {
                var vendors = db.Vendors;
                VendorReport currentVendorReport;

                foreach (var vendor in vendors)
                {
                    currentVendorReport = new VendorReport { 
                        VendorName = vendor.VendorName,
                        DailyReports = new List<VendorSalesReport>() };

                    foreach (var product in vendor.Products)
                    {
                        var salesByDate = product.Sales.GroupBy(x => x.Date);

                        foreach (var date in salesByDate)
                        {
                            currentVendorReport.DailyReports.Add( 
                                new VendorSalesReport { Date = date.Key, TotalSum = date.Sum(x => x.Sum) });
                        }
                    }

                    reports.Add(currentVendorReport);
                }
            }

            return reports;
        }
           
        static void AddReportsToDatabase(string archivePath, string destinationPath)
        {
            var pathsToFiles = ZipReader.ExtractZip(archivePath, destinationPath);

            List<Tuple<Market, List<Sale>>> excelFilesContent = new List<Tuple<Market, List<Sale>>>();

            foreach (var path in pathsToFiles)
            {
                excelFilesContent.Add(ExcelReader.ReadTable(destinationPath + path));
            }

            using (var db = new SupermarketContext())
            {
                Market currentMarket;

                foreach (var report in excelFilesContent)
                {
                    var dublicateMarkets = db.Markets.Where(x => x.MarketName == report.Item1.MarketName).ToList();

                    if (dublicateMarkets.Count == 0)
                    {
                        currentMarket = new Market { MarketName = report.Item1.MarketName };
                        db.Markets.Add(currentMarket);
                        db.SaveChanges();
                    }
                    else
                    {
                        currentMarket = dublicateMarkets[0];
                    }

                    foreach (var sale in report.Item2)
                    {
                        db.Sales.Add(new Sale 
                        { 
                            MarketId = currentMarket.MarketID,
                            ProductId = sale.ProductId,
                            Date = sale.Date,
                            Quantity = sale.Quantity,
                            UnitPrice = sale.UnitPrice,
                            Sum = sale.Sum
                        });
                    }

                    db.SaveChanges();
                }
            }
        }

        public static void NextTask()
        {
            Console.WriteLine("{0}", Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Any Key To Continue To Next Task ...");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
