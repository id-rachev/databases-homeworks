using System;
using System.Collections.Generic;
using SupermarketModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Data.Entity;
using System.Data.SQLite;
using MongoDB.Bson;
using SupermarketEntityData;
using System.Linq;
using MongoDB.Driver;

namespace Helper
{
    public class ExcelWriter
    {
        public List<BsonDocument> GetProductData()
        {
            MongoDb.InitConnection();
            return MongoDb.LoadProducReports();
        }

        public List<VendorTotalReport> GenerateReports()
        {
            List<VendorTotalReport> results = new List<VendorTotalReport>();

            using (var db = new DbContext("SqLiteConnection"))
            {
                var taxes = db.Database.SqlQuery<ProductTax>("SELECT p.Product, p.Tax FROM ProductsTaxes p");
                var productsInfo = GetProductData();

                using (var context = new SupermarketContext())
                {
                    var vendors = context.Vendors;
                    VendorTotalReport currentVendor;
                    foreach (var vendor in vendors)
                    {
                        currentVendor = new VendorTotalReport { VendorName = vendor.VendorName };

                        var products = vendor.Products;
                        var metaExpense =  context.VendorsExpenses.Where(x => x.VendorId == vendor.VendorID).FirstOrDefault();
                        decimal expense = metaExpense == null ? 0 : metaExpense.Expense;

                        double sum = 0;
                        double tax = 0;

                        double productTax;

                        foreach (var product in products)
                        {
                            var prod = productsInfo.Where(x => x["product-name"] == product.ProductName).First();

                            sum += (double)prod["total-incomes"];
                            productTax = taxes.Where(x => x.Product == product.ProductName).FirstOrDefault().Tax;
                            tax += (double)prod["total-incomes"] * (productTax / 100.0);
                        }

                        currentVendor.Incomes = (decimal)sum;
                        currentVendor.Taxes = (decimal)tax;
                        currentVendor.Expenses = expense;
                        currentVendor.FinancialResult = currentVendor.Incomes - (currentVendor.Taxes + expense);

                        results.Add(currentVendor);
                    }
                }

            }

            return results;
        }


        public void CreateVendorTotalReports(List<VendorTotalReport> reports)
        {
            using (var excel = new ExcelPackage(new FileInfo("..\\..\\..\\Reports\\Task06ExcelTotalVendorReports.xlsx")))
            {
                if (excel.Workbook.Worksheets["One"] == null)
                {
                    excel.Workbook.Worksheets.Add("One");
                }

                var book = excel.Workbook.Worksheets["One"];

                var columnsTitles = new String[] { "Vendor", "Incomes", "Expenses", "Taxes", "Financial Result" };

                for (var i = 1; i < columnsTitles.Length + 1; i++)
                {
                    book.Cells[1, i].Value = columnsTitles[i - 1];
                    book.Column(i).Width = columnsTitles[i - 1].Length + 5;
                    book.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                book.Column(1).Width = 30;

                for (int i = 2; i < reports.Count + 2; i++)
                {
                    book.Cells[i, 1].Value = reports[i - 2].VendorName;
                    book.Cells[i, 2].Value = reports[i - 2].Incomes;
                    book.Cells[i, 3].Value = reports[i - 2].Expenses;
                    book.Cells[i, 4].Value = reports[i - 2].Taxes;
                    book.Cells[i, 5].Value = reports[i - 2].FinancialResult;
                }

                excel.Save();
            }
        }

        public void CreateExcelFile()
        {
            CreateVendorTotalReports(GenerateReports());
        }
    }
}
