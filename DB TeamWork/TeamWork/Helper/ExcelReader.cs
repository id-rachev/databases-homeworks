using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using SupermarketModel;

namespace Helper
{
    public class ExcelReader
    {
        public static Tuple<Market, List<Sale>> ReadTable(string path)
        {
            List<Sale> sales = new List<Sale>();
            Market superMarket = new Market();
            DateTime date = ParseDate(path);

            path = path + ";";
            string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=" + path +
            @"Extended Properties=""Excel 8.0;HDR=YES"";";

            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            using (dbConnection)
            {
                string worksheetName = "Sales";
                OleDbConnection con = new System.Data.OleDb.OleDbConnection(connectionString);
                OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter(
                    "select * from [" + worksheetName + "$]", con);

                con.Open();
                DataSet excelDataSet = new DataSet();
                cmd.Fill(excelDataSet);
                con.Close();

                DataTable table = excelDataSet.Tables[0];

                string supermarketName = table.Rows[0].ItemArray[0].ToString();
                superMarket.MarketName = supermarketName;

                for (int row = 2; row < table.Rows.Count - 2; row++)
                {
                    int productID = int.Parse(table.Rows[row].ItemArray[0].ToString());
                    int quantity = int.Parse(table.Rows[row].ItemArray[1].ToString());
                    decimal price = decimal.Parse(table.Rows[row].ItemArray[2].ToString());
                    decimal sum = decimal.Parse(table.Rows[row].ItemArray[3].ToString());
                    Sale sale = new Sale
                    {
                        Market = superMarket,
                        ProductId = productID,
                        Quantity = quantity,
                        UnitPrice = price,
                        Sum = sum,
                        Date = date
                    };
                    sales.Add(sale);
                }
            }

            return new Tuple<Market, List<Sale>>(superMarket, sales);
        }

        private static DateTime ParseDate(string path)
        {
            int startIndex = path.LastIndexOf('\\');
            int endIndex = path.Length;

            string dateString = path.Substring(startIndex, endIndex - startIndex);
            string[] fileName = dateString.Split(new char[] { '-', '.' },
                StringSplitOptions.RemoveEmptyEntries);

            int length = fileName.Length;
            int year = int.Parse(fileName[length - 2]);
            int month = GetMonth(fileName[length - 3]);
            int day = int.Parse(fileName[length - 4]);

            DateTime date = new DateTime(year, month, day);
            return date;
        }

        private static int GetMonth(string month)
        {
            switch (month)
            {
                case "Jan":
                    return 1;
                case "Feb":
                    return 2;
                case "Mar":
                    return 3;
                case "Apr":
                    return 4;
                case "May":
                    return 5;
                case "Jun":
                    return 6;
                case "Jul":
                    return 7;
                case "Aug":
                    return 8;
                case "Sep":
                    return 9;
                case "Oct":
                    return 10;
                case "Nov":
                    return 11;
                case "Dec":
                    return 12;
                default:
                    throw new ArgumentException("Invalid month");
            }
        }
    }
}