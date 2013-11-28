using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModel
{
    public class VendorTotalReport
    {
        public string VendorName { get; set; }
        public decimal Incomes { get; set; }
        public decimal Expenses { get; set; }
        public decimal Taxes { get; set; }
        public decimal FinancialResult { get; set; }
    }
}
