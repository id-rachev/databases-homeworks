using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.IO;
using SupermarketModel;
using SupermarketEntityData;

namespace Helper
{
    public static class ProductReportGenerator
    {
        public static void GenerateReport(List<Product> products)
        {
            var database = SaveToMongoDB(products);
            SaveToFiles(database);
        }

        private static void SaveToFiles(MongoDatabase database)
        {
            var products = database.GetCollection("Products").FindAll();
            string folder = "..\\..\\..\\Reports\\Product-Reports";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            foreach (var product in products)
            {
                BsonDocument bsonProduct = new BsonDocument();
                bsonProduct["product-id"] = product["product-id"];
                bsonProduct["product-name"] = product["product-name"];
                bsonProduct["vendor-name"] = product["vendor-name"];
                bsonProduct["total-quantity-sold"] = product["total-quantity-sold"];
                bsonProduct["total-incomes"] = product["total-incomes"];

                string path = folder + "\\" + product["product-id"] + ".json";

                StreamWriter writer = new StreamWriter(path);

                using (writer)
                {
                    writer.Write(bsonProduct.AsBsonDocument.ToString());
                }
            }
        }

        private static MongoDatabase SaveToMongoDB(List<Product> products)
        {
            MongoDatabase database;
            
            using(var db = new SupermarketContext())
            {
                database = Helper.MongoDb.SaveProductReports(products, db);
            }

            return database;
        }
    }
}
