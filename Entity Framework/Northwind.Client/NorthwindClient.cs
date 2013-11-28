using System;
using System.Linq;
using Northwind.Model;

namespace Northwind.Client
{
    class NorthwindClient
    {
        static void Main()
        {
            // Task 1 and 2
            Customer customer = new Customer();
            customer.CustomerID = "POKOM";
            customer.CompanyName = "Poko Moko";

            //int rowsAffected = DAO.AddCustomer(customer);
            //Console.WriteLine("Rows affected: {0}", rowsAffected);

            //int rowsAffected = DAO.RemoveCustomer(customer);
            //Console.WriteLine("Rows affected: {0}", rowsAffected);

            Customer changedCustomer = new Customer
            {
                CustomerID = "POKOM",
                CompanyName = "Poko Mokov",
                ContactName = "Pokolan Mokovski",
                City = "Warsawa",
                Country = "Polska"
            };

            //int rowsAffected = DAO.ModifyCustomer(customer, changedCustomer);
            //Console.WriteLine("Rows affected: {0}", rowsAffected);

            // Task 3
            Console.WriteLine("With extension methods:");
            DAO.FindCustomersByOrder("Canada", 1997);

            // Task 4
            Console.WriteLine("\nWith SQL string:");
            DAO.FindCustomersByOrderSql("Canada", 1997);

            // Task 5
            Console.WriteLine();
            DAO.FindSales("Essex", new DateTime(1997, 1, 1),
                new DateTime(1998, 12, 31));
        }
    }
}
