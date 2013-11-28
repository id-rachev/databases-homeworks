using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Bookstore.Model;

namespace ComplexSearch
{
    public static class ComplexSearch
    {
        private static void Main(string[] args)
        {
            // Problem 6
            XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../../test1.xml");


                DateTime start;
                DateTime end;
                string authorName;


                string xPathQuery = "/review-queries/query[@type='by-period']";

                XmlNodeList periodsList = xmlDoc.SelectNodes(xPathQuery);
                foreach (XmlNode periodNode in periodsList)
                {
                    start = DateTime.Parse(periodNode.SelectSingleNode("start-date").InnerText);
                    end = DateTime.Parse(periodNode.SelectSingleNode("end-date").InnerText);
                    var reviews = BooksDAL.FindReviews(start, end);
                }

                xPathQuery = "/review-queries/query[@type='by-author']";

                XmlNodeList authorsList = xmlDoc.SelectNodes(xPathQuery);
                foreach (XmlNode authorNode in authorsList)
                {
                    authorName = authorNode.SelectSingleNode("author-name").InnerText;
                    var reviews = BooksDAL.FindReviews(authorName);
                }
        }

        private static string GetChildText(
            this XmlNode node, string xpath)
        {
            XmlNode childNode = node.SelectSingleNode(xpath);
            if (childNode == null)
            {
                return null;
            }
            return childNode.InnerText.Trim();
        }
    }
}
