using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using Helper.Lib;
using SupermarketModel;

namespace Helper
{
    public static class PDFExporter
    {
        public static void CreatePDF(List<SalesReport> reports)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='1' cellpadding='5' cellspacing='5'>" +
                        "<tr align='center'>" +
                            "<th colspan='5'>Aggregated Sales Report</th>" +
                        "</tr>");
            decimal grandTotal = 0;
            foreach (var report in reports)
            {
                sb.AppendFormat("<tr bgcolor='#ddd'>" +
                                    "<th colspan='5'>Date: {0:dd-MM-yyy}</th>", report.Date);
                sb.Append("</tr>" +
                          "<tr bgcolor='#ddd' align='center'>" +
                                "<th style='width:250px;'>Product</th>" +
                                "<th style='width:100px;'>Quantity</th>" +
                                "<th style='width:100px;'>Unit Price</th>" +
                                "<th style='width:350px;'>Location</th>" +
                                "<th style='width:100px;'>Sum</th>" +
                          "</tr>");
                decimal totalSum = 0;
                for (int i = 0; i < report.Products.Count; i++)
                {
                    totalSum += report.Sums[i];
                    sb.AppendFormat("<tr>" +
                                        "<td style='width:250px;'>{0}</td>" +
                                        "<td style='width:100px;' align='center'>{1}</td>" +
                                        "<td style='width:100px;' align='center'>{2}</td>" +
                                        "<td style='width:350px;'>{3}</td>" +
                                        "<td style='width:100px;' align='right'>{4:C}</td>" +
                                    "</tr>", report.Products[i],
                        report.Quantities[i], report.UnitPrices[i],
                        report.Locations[i], report.Sums[i]);
                }
                grandTotal += totalSum;
                sb.AppendFormat("<tr align='right'>" +
                                    "<td colspan='4'>Total sum for {0:dd-MM-yyy}:</td>" +
                                    "<td>{1:C}</td>" +
                                "</tr>", report.Date, totalSum);
                sb.Append("<tr align='center'>" +
                            "<td colspan='4'>...</td>" +
                            "<td>...</td>" +
                        "</tr>");
            }
            sb.AppendFormat("<tr align='right'>" +
                                "<td colspan='4'>Grand total:</td>" +
                                "<td>{0:C}</td>" +
                            "</tr>", grandTotal);
            sb.Append("</table>");

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(sb.ToString());
            byte[] file = builder.RenderPdf();

            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Reports\\PDFSalesReport\\";
            string tempFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "-Report" + ".pdf";
            if (Helpers.DirectoryExist(tempFolder))
            {
                if (!File.Exists(tempFolder + tempFileName))
                    File.WriteAllBytes(tempFolder + tempFileName, file);
            }
        }
    }
}
