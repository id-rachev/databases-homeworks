using SupermarketModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModel
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime? Date { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Sum { get; set; }

        public override string ToString()
        {
            return String.Format("{0,-40} | {1,-15} | {2,-5} | {3,-10:C2} | {4,-5:C} | {5} ",
                this.Market.MarketName, this.Product.ProductName, 
                this.Quantity + " " + this.Product.Measure.MeasureName, 
                this.UnitPrice, this.Sum, this.Date);
        }
    }
}
