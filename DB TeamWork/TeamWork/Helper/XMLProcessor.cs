using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SupermarketEntityData;
using SupermarketModel;

namespace Helper
{
    public class XMLProcessor
    {
        public static void GenerateSalesReport(List<VendorReport> vendorReports)
        {
            string fileName = "..\\..\\..\\Reports\\Sales-by-Vendors-report.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (var writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");

                foreach (var vendorReport in vendorReports)
                {
                    writer.WriteStartElement("sale");
                    writer.WriteAttributeString("vendor", vendorReport.VendorName);
                    foreach (var summary in vendorReport.DailyReports)
                    {
                        WriteSummary(writer, (DateTime)summary.Date, summary.TotalSum);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndDocument();
            }

        }

        public static List<VendorExpenseObj> LoadVendorExpenses(string path)
        {
            List<VendorExpenseObj> vendorExpenses = new List<VendorExpenseObj>();
            using (XmlReader reader = XmlReader.Create(path))
            {
                string vendorName = string.Empty;
                while (reader.Read())
                {
                    VendorExpenseObj vendorExpense = new VendorExpenseObj();
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "sale"))
                    {
                        vendorName = reader.GetAttribute("vendor");
                    }
                    else if ((reader.NodeType == XmlNodeType.Element) &&
                             (reader.Name == "expenses"))
                    {
                        string date = reader.GetAttribute("month");
                        string[] dateSplit = date.Split('-');
                        int month = GetMonth(dateSplit[0]);
                        int year = int.Parse(dateSplit[1]);
                        DateTime vendorDate = new DateTime(year, month, 1);
                        vendorExpense.Date = vendorDate;
                        vendorExpense.VendorName = vendorName;

                        decimal expense = decimal.Parse(reader.ReadElementString());
                        vendorExpense.Expense = expense;
                        vendorExpenses.Add(vendorExpense);
                    }
                }
            }

            return vendorExpenses;
        }

        private static void WriteSummary(XmlTextWriter writer, DateTime date, decimal totalSum)
        {
            string month = GetMonth(date);
            string dateStr = date.Day + "-" + month + "-" + date.Year;
            writer.WriteStartElement("summary");
            writer.WriteAttributeString("date", dateStr);
            writer.WriteAttributeString("total-sum", totalSum.ToString());
            writer.WriteEndElement();
        }

        private static string GetMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    throw new ArgumentException("Invalid month");
            }
        }

        private static int GetMonth(string month)
        {
            switch (month)
            {
                case "Jan":
                    return 1;
                case "Feb":
                    return 2;
                case "Mar":
                    return 3;
                case "Apr":
                    return 4;
                case "May":
                    return 5;
                case "Jun":
                    return 6;
                case "Jul":
                    return 7;
                case "Aug":
                    return 8;
                case "Sep":
                    return 9;
                case "Oct":
                    return 10;
                case "Nov":
                    return 11;
                case "Dec":
                    return 12;
                default:
                    throw new ArgumentException("Invalid month");
            }
        }
    }
}
