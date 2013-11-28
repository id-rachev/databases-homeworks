using System;
using System.Collections.Generic;
using System.Linq;
using SupermarketModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SupermarketEntityData;

namespace Helper
{
    public static class MongoDb
    {
        public static MongoServerSettings settings = new MongoServerSettings();
        public static MongoServer server;

        public static void InitConnection()
        {
            settings = new MongoServerSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);
            server = new MongoServer(settings);
        }

        public static MongoDatabase SaveProductReports(List<Product> products, SupermarketContext db)
        {
            var database = server.GetDatabase("ProductsDB");
            var productsCollection = database.GetCollection("Products");

            if (!productsCollection.Exists())
            {
                database.CreateCollection("Products");
                productsCollection = database.GetCollection("Products");
            }

            foreach (var product in products)
            {
                var quantitiesSold = from s in db.Sales
                                     join p in db.Products
                                     on s.ProductId equals p.ProductID
                                     where s.ProductId == product.ProductID
                                     select s.Quantity;

                var incomes = from s in db.Sales
                              join p in db.Products
                              on s.ProductId equals p.ProductID
                              where s.ProductId == product.ProductID
                              select s.Sum;

                int totalQuantitySold;
                decimal totalIncomes;

                SetValue(quantitiesSold.ToList(), out totalQuantitySold);
                SetValue(incomes.ToList(), out totalIncomes);

                BsonDocument bsonProduct = new BsonDocument();
                bsonProduct["product-id"] = product.ProductID;
                bsonProduct["product-name"] = product.ProductName;
                bsonProduct["vendor-name"] = product.Vendor.VendorName;
                bsonProduct["total-quantity-sold"] = totalQuantitySold;
                bsonProduct["total-incomes"] = (double)totalIncomes;
                productsCollection.Insert(bsonProduct);
            }

            return database;
        }

        private static void SetValue(List<int> queryCollection, out int totalSum)
        {
            if (queryCollection != null || queryCollection.Count != 0)
            {
                totalSum = queryCollection.Sum();
            }
            else
            {
                totalSum = 0;
            }
        }

        private static void SetValue(List<decimal> queryCollection, out decimal totalSum)
        {
            if (queryCollection != null || queryCollection.Count != 0)
            {
                totalSum = queryCollection.Sum();
            }
            else
            {
                totalSum = 0;
            }
        }

        public static void SaveExpenses(List<VendorExpenseObj> vendorsExpenses)
        {
            var database = server.GetDatabase("VendorsDB");
            var vendorsExpensesCollection = database.GetCollection("VendorsExpenses");

            if (!vendorsExpensesCollection.Exists())
            {
                database.CreateCollection("VendorsExpenses");
                vendorsExpensesCollection = database.GetCollection("VendorsExpenses");
            }

            foreach (var vendorExpense in vendorsExpenses)
            {
                BsonDocument bsonProduct = new BsonDocument();
                bsonProduct["vendor-name"] = vendorExpense.VendorName;
                bsonProduct["vendor-date"] = vendorExpense.Date;
                bsonProduct["vendor-expense"] = (double)vendorExpense.Expense;
                vendorsExpensesCollection.Insert(bsonProduct);
            }
        }

        public static List<BsonDocument> LoadProducReports()
        {
            var database = server.GetDatabase("ProductsDB");
            var productReportsCollection = database.GetCollection("Products").FindAll().ToList();
            return productReportsCollection;
        }

        public static void ClearDatabases()
        {
            var _mongoServer = MongoServer.Create("mongodb://localhost:27017");

            var names = _mongoServer.GetDatabaseNames();
            foreach (var name in names)
            {
                _mongoServer.DropDatabase(name);
            }
        }
    }
}
