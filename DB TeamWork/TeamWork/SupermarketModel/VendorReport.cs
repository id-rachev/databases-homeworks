using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModel
{
    public class VendorReport
    {
        public string VendorName { get; set; }
        public List<VendorSalesReport> DailyReports { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Report For '" + this.VendorName + '\'');

            foreach (var report in this.DailyReports)
            {
                sb.AppendLine(String.Format("Date : {0} - > Sum : {1:C2}", 
                    ((DateTime)report.Date).ToString("yyyy-MM-dd"), report.TotalSum));
            }

            return sb.ToString();
        }
    }
}
