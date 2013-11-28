using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ATM.Operator
{
    class AtmTransactions
    {
        private const string CONNECTION_STRING = "Server=.\\SQLEXPRESS;" +
        " Database=ATM; Integrated Security=true";

        static void Main()
        {

            SqlConnection dbConection = new SqlConnection(CONNECTION_STRING);
            dbConection.Open();
            using (dbConection)
            {
                SqlTransaction trans =
                    dbConection.BeginTransaction(IsolationLevel.ReadCommitted);

                SqlCommand cmd = dbConection.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    cmd.CommandText =
                        "INSERT INTO CardAccounts (CardNumber, CardPin, CardCash) " +
                        "VALUES ('9876543210', '1234', 350)";
                    cmd.ExecuteNonQuery();

                    string cardNumber = "9876543210";
                    string cardPin = "1234";
                    decimal requestedSum = 200;

                    WithdrawMoney(cmd, requestedSum);
                    
                    trans.Commit();
                    Console.WriteLine("Transaction comitted.");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Exception occured: {0}", e.Message);
                    trans.Rollback();
                    Console.WriteLine("Transaction cancelled.");
                }
            }
        }

        private static void WithdrawMoney(SqlCommand cmd, decimal requestedSum)
        {

        }
    }
}
