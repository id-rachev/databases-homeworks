using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace _1_5.NorthwindDB_Operations
{
    class NorthwindDBOperations
    {
        static void Main()
    {
        SqlConnection nwConnection = new SqlConnection(
            "Server = .\\SQLEXPRESS; " +
            "Database = northwind; Integrated Security = true");
        nwConnection.Open();
        using (nwConnection)
        {
            // Task 1
            // Write a program that retrieves from the Northwind sample database
            // in MS SQL Server the number of  rows in the Categories table.

            SqlCommand commandCount = new SqlCommand(
                "SELECT COUNT(*) FROM Categories", nwConnection);
            int categoriesCount = (int)commandCount.ExecuteScalar();
            Console.WriteLine(
                "Number of rows in Categories table: {0}.\n", categoriesCount);

            // Task 2
            // Write a program that retrieves the name and description
            // of all categories in the Northwind DB.

            SqlCommand commandDescription = new SqlCommand(
                "SELECT CategoryName, Description FROM Categories", nwConnection);
            SqlDataReader reader = commandDescription.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    string description = (string)reader["Description"];
                    Console.WriteLine("Category name: {0},\nDescription: {1}.\n",
                        categoryName, description);
                }
            }

            // Task 3
            // Write a program that retrieves from the Northwind database all
            // product categories and the names of the products in each category.
            // Can you do this with a single SQL query (with table join)?

            Console.WriteLine("Category Name  | Product Name");
            Console.WriteLine(new string('-', 50));
            SqlCommand commandProducts = new SqlCommand(
                "SELECT c.CategoryName, p.ProductName " +
                "FROM Products p " +
                "JOIN Categories c " +
                    "ON p.CategoryID = c.CategoryID " +
                "GROUP BY c.CategoryName, p.ProductName", nwConnection);
            reader = commandProducts.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    string productName = (string)reader["ProductName"];
                    Console.WriteLine("{0,-15}| {1}", categoryName, productName);
                    Console.WriteLine(new string('-', 50));
                }
            }

            // Task 4
            // Write a method that adds a new product in the products table 
            // in the Northwind database. Use a parameterized SQL command.

            string newProductName = "Rokamola";
            int? supplierID = 7;
            int? categoryID = 3;
            string quantityPerUnit;
            decimal? unitPrice = 21.50m;
            int? unitsInStock = 70;
            int? unitsOnOrder = 10;
            int? reorderLevel;
            bool discontinued = true;

            AddProduct(newProductName, supplierID, categoryID, null, unitPrice,
                unitsInStock, unitsOnOrder, null, discontinued, nwConnection);

            // Task 5
            // Write a program that retrieves the images for all categories in
            // the Northwind database and stores them as JPG files in the file system.

            List<Image> imagesSet = new List<Image>();
            SqlCommand commandImages = new SqlCommand(
                "SELECT Picture FROM Categories", nwConnection);
            reader = commandImages.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    Image currentImage = new Image((byte[])reader["Picture"]);
                    imagesSet.Add(currentImage);
                }
            }

            string imagePath = @"..\..\product_images\picture_";
            for (int i = 0; i < imagesSet.Count; i++)
			{
                imagesSet[i].WriteToFile(imagePath + (i + 1));                
            }
        }
    }

        public static void AddProduct(string productName, int? supplierID, int? categoryID,
            string quantityPerUnit, decimal? unitPrice, int? unitsInStock, int? unitsOnOrder,
            int? reorderLevel, bool discontinued, SqlConnection nwConnection)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Products " +
              "(ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, " +
              "UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) VALUES " +
              "(@productName, @supplierID, @categoryID, @quantityPerUnit, @unitPrice, " +
              "@unitsInStock, @unitsOnOrder, @reorderLevel, @discontinued)", nwConnection);

            cmd.Parameters.AddWithValue("@productName", productName);

            SqlParameter sqlParamSupplierID = new SqlParameter("@supplierID", supplierID);
            if (supplierID == null)
            {
                sqlParamSupplierID.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamSupplierID);

            SqlParameter sqlParamCategoryID = new SqlParameter("@categoryID", categoryID);
            if (categoryID == null)
            {
                sqlParamCategoryID.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamCategoryID);

            SqlParameter sqlParamQuantityPerUnit = new SqlParameter("@quantityPerUnit", quantityPerUnit);
            if (quantityPerUnit == null)
            {
                sqlParamQuantityPerUnit.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamQuantityPerUnit);

            SqlParameter sqlParamUnitPrice = new SqlParameter("@unitPrice", unitPrice);
            if (unitPrice == null)
            {
                sqlParamUnitPrice.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamUnitPrice);

            SqlParameter sqlParamUnitsInStock = new SqlParameter("@unitsInStock", unitsInStock);
            if (unitsInStock == null)
            {
                sqlParamUnitsInStock.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamUnitsInStock);

            SqlParameter sqlParamUnitsOnOrder = new SqlParameter("@unitsOnOrder", unitsOnOrder);
            if (unitsOnOrder == null)
            {
                sqlParamUnitsOnOrder.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamUnitsOnOrder);

            SqlParameter sqlParamReorderLevel = new SqlParameter("@reorderLevel", reorderLevel);
            if (reorderLevel == null)
            {
                sqlParamReorderLevel.Value = DBNull.Value;
            }
            cmd.Parameters.Add(sqlParamReorderLevel);

            cmd.Parameters.AddWithValue("@discontinued", discontinued);
            cmd.ExecuteNonQuery();
        }
    }
}
