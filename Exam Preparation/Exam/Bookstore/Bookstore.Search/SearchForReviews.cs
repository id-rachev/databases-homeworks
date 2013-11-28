using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Bookstore.Search
{
    class SearchForReviews
    {
        static void Main()
        {
            ReviewsImport();
        }
  
        private static void ReviewsImport()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../reviews-queries.xml");
            string xPathQuery = "/review-queries/query[@type='by-period']";

            XmlNodeList periodsList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode periodNode in periodsList)
            {
                string startDate = periodNode.SelectSingleNode("start-date").InnerText;
                Console.WriteLine(startDate);

                string endDate = periodNode.SelectSingleNode("end-date").InnerText;
                Console.WriteLine(endDate);
            }

            xPathQuery = "/review-queries/query[@type='by-author']";

            XmlNodeList authorsList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode authorNode in authorsList)
            {
                string authorName = authorNode.SelectSingleNode("author-name").InnerText;
                Console.WriteLine(authorName);
            }
        }
    }
}
