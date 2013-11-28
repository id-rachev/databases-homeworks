using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace _6_7.Excel_Table_Operations
{
    class ExcelTableOperations
    {
        static void Main()
        {
            // Task 6
            // Create an Excel file with 2 columns: name and score:
            // Write a program that reads your MS Excel file through the OLE
            // DB data provider and displays the name and score row by row.

            DataTable dataTable = new DataTable("newtable");
            OleDbConnectionStringBuilder connectionString = new OleDbConnectionStringBuilder();
            connectionString.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionString.DataSource = @"..\..\LectorsScore.xlsx";
            connectionString.Add("Extended Properties", "Excel 10.0 Xml;HDR=YES");
            
            using (OleDbConnection oleConnection = new OleDbConnection(connectionString.ConnectionString))
            {
                oleConnection.Open();
                string selectCommand = @"SELECT * FROM [Score]";

                using (OleDbDataAdapter oleAdapter = new OleDbDataAdapter(selectCommand, oleConnection))
                {
                    oleAdapter.FillSchema(dataTable, SchemaType.Source);
                    oleAdapter.Fill(dataTable);
                }


                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.WriteLine(item);
                    }
                }

                string name = "Mocho Mochev";
                int score = 19;
                int result = 0;

                string insertToTable = @"INSERT INTO [Sheet1$] (Name, Score) VALUES (@Name, @Score)";
                using (OleDbCommand command = oleConnection.CreateCommand())
                {
                    oleConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = insertToTable;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Score", score);
                    result = command.ExecuteNonQuery();
                }

                oleConnection.Close();
            }
        }
    }
}
