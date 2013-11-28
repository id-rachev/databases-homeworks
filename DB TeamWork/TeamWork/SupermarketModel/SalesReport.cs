using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModel
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public List<string> Products { get; set; }
        public List<string> Quantities { get; set; }
        public List<decimal> UnitPrices { get; set; }
        public List<string> Locations { get; set; }
        public List<decimal> Sums { get; set; }

        public SalesReport()
        {
            this.Products = new List<string>();
            this.Quantities = new List<string>();
            this.UnitPrices = new List<decimal>();
            this.Locations = new List<string>();
            this.Sums = new List<decimal>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Date : " + this.Date);

            for (int i = 0; i < this.Products.Count; i++)
            {
                sb.AppendLine(String.Format("{0,-20} | {1,-10} | {2,-10} | {3,-20} | {4,-10} ",
                    this.Products[i], this.Quantities[i], this.UnitPrices[i], this.Locations[i], this.Sums[i]));
            }

            return sb.ToString();
        }
    }
}
