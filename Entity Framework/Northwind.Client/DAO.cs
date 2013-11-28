using System;
using System.Linq;
using Northwind.Model;

namespace Northwind.Client
{
    public static class DAO
    {
        public static int AddCustomer(Customer newCustomer)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                nwDataBase.Customers.Add(newCustomer);
                var rowsAfected = nwDataBase.SaveChanges();
                return rowsAfected;
            }
        }

        public static int RemoveCustomer(Customer oldCustomer)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                var customer = nwDataBase.Customers.Find(oldCustomer.CustomerID);
                nwDataBase.Customers.Remove(customer);
                var rowsAfected = nwDataBase.SaveChanges();
                return rowsAfected;
            }
        }

        public static int ModifyCustomer(
            Customer customerToChange, Customer changedCustomer)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                if (customerToChange.CustomerID != changedCustomer.CustomerID)
                {
                    throw new ArgumentException("Customer ID is not valid!", "CustomerID");
                }

                nwDataBase.Customers.Attach(customerToChange);
                customerToChange.CompanyName = changedCustomer.CompanyName;
                customerToChange.ContactName = changedCustomer.ContactName;
                customerToChange.ContactTitle = changedCustomer.ContactTitle;
                customerToChange.Address = changedCustomer.Address;
                customerToChange.City = changedCustomer.City;
                customerToChange.Region = changedCustomer.Region;
                customerToChange.PostalCode = changedCustomer.PostalCode;
                customerToChange.Country = changedCustomer.Country;
                customerToChange.Phone = changedCustomer.Phone;
                customerToChange.Fax = changedCustomer.Fax;

                var rowsAfected = nwDataBase.SaveChanges();
                return rowsAfected;
            }
        }

        public static void FindCustomersByOrder(string country, int year)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                var ordersData = nwDataBase.Orders.Join(
                    nwDataBase.Customers, (o => o.CustomerID), (c => c.CustomerID), (o, c) =>
                    new { Order = o.OrderDate, Country = o.ShipCountry, Customer = c.CompanyName })
                    .Where(x => x.Country == country && x.Order.Value.Year == year);
                
                foreach (var order in ordersData)
                {
                    Console.WriteLine("{0,-30} {1:dd/MM/yyyy}  {2}",
                        order.Customer, order.Order, order.Country);
                }
            }
        }

        public static void FindCustomersByOrderSql(string country, int year)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                string query = "SELECT CompanyName, OrderDate, ShipCountry FROM Orders " +
                                "join Customers " +
                                "on Orders.CustomerID = Customers.CustomerID " +
                                "WHERE year(OrderDate) = {0} and ShipCountry = {1}";
                object[] parameters = { year, country };
                var ordersData = nwDataBase.Database.SqlQuery<CompanyAndDate>(query, parameters);

                foreach (var order in ordersData)
                {
                    Console.WriteLine("{0,-30} {1}  {2}", order.CompanyName,
                        order.OrderDate.ToString("dd/MM/yyyy"), order.ShipCountry);
                }
            }
        }

        public static void FindSales(string region, DateTime start, DateTime end)
        {
            using (var nwDataBase = new NorthwindEntities())
            {
                var sellsData = nwDataBase.Orders.Where(
                    x => x.ShippedDate >= start &&
                        x.ShippedDate <= end &&
                        x.ShipRegion == region);
                foreach (var sell in sellsData)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", sell.OrderID,
                        sell.ShippedDate.GetValueOrDefault().ToString("dd/MM/yyyy"), sell.ShipCity);
                }
            }
        }

        class CompanyAndDate
        {
            public string CompanyName { get; set; }
            public DateTime OrderDate { get; set; }
            public string ShipCountry { get; set; }
        }
    }
}
