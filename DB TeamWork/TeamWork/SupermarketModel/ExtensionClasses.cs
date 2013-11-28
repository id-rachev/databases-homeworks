using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModel
{
    public partial class Product
    {
        private ICollection<Sale> sales;

        public Product()
        {
            this.sales = new HashSet<Sale>();
        }

        public virtual ICollection<Sale> Sales { get; set; }
    }

    public partial class Vendor
    {
        private ICollection<VendorsExpense> expenses;

        public Vendor()
        {
            this.expenses = new HashSet<VendorsExpense>();
        }

        public virtual ICollection<VendorsExpense> Expenses { get; set; }
    }
}
