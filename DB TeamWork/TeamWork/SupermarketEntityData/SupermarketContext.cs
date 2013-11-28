using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketModel;
using System.Data.Entity;
using Telerik.OpenAccess;

namespace SupermarketEntityData
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext()
            : base("supermarketConnection")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<VendorsExpense> VendorsExpenses { get; set; }

        public void Update()
        {
            using (var db = new EntitiesModel())
            {
                foreach (var v in db.Vendors)
                {
                    if (this.Vendors.Find(v.VendorID) == null)
                    {
                        this.Vendors.Add(new Vendor { VendorName = v.VendorName });
                    }
                }
                this.SaveChanges();

                foreach (var m in db.Measures)
                {
                    if (this.Measures.Find(m.MeasureID) == null)
                    {
                        this.Measures.Add(new Measure { MeasureName = m.MeasureName });
                    }
                }
                this.SaveChanges();

                foreach (var p in db.Products)
                {
                    if (this.Products.Find(p.ProductID) == null)
                    {
                        this.Products.Add(new Product
                        {
                            ProductName = p.ProductName,
                            BasePrice = p.BasePrice,
                            VendorID = p.VendorID,
                            MeasureID = p.MeasureID
                        });
                    }
                }
                this.SaveChanges();
            }
        }
    }
}
