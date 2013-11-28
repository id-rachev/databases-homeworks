using System;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Data;
using System.Xml;
using System.Text;
using _07.SearchLogs;
using System.Data.Entity;
using _07.SearchLogs.Migrations;

namespace _06.SearchForReviews
{
    static class SearchForReviews
    {
        static void Main()
        {
            //7.
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion
            //<LogContext, Configuration>());

            string fileName = "../../reviews-search-results.xml";
            using (XmlTextWriter writer =
                new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("search-results");
                


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../../performance-test.xml");
                string xPathQuery = "/review-queries/query";

                XmlNodeList queries = xmlDoc.SelectNodes(xPathQuery);
                foreach (XmlNode query in queries)
                {
                    //07.
                    //LogDAL.AddQueryToBase(query.OuterXml);

                    var queryAttr = query.Attributes["type"].Value;

                    if (queryAttr == "by-period")
                    {
                        var queriesByPeriod = BookstoreDAL.QueriesByPeriod(query);

                        WriteBookmarks(writer, queriesByPeriod);
                    }
                    else
                    {
                        
                        var queriesByAuthor = BookstoreDAL.QueriesByAuthor(query);

                        WriteBookmarks(writer, queriesByAuthor);
                        
                    }

                }
                writer.WriteEndDocument();
            }


        }

        private static void WriteBookmarks(
        XmlTextWriter writer, IList<Review> reviews)
        {
            writer.WriteStartElement("result-set");
            foreach (var review in reviews)
            {
                writer.WriteStartElement("review");
                if (review.DateOfCreation != null)
                {
                    writer.WriteElementString("date", review.DateOfCreation.ToString());
                }
                if (review.ReviewContent != null)
                {
                    writer.WriteElementString("content", review.ReviewContent);
                }
                if (review.Book != null)
                {
                    writer.WriteStartElement("book");

                    if (review.Book.Title != null)
                    {
                        writer.WriteElementString("title", review.Book.Title);
                    }
                    if (review.Book.OfficialWebSite != null)
                    {
                        writer.WriteElementString("url", review.Book.OfficialWebSite);
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
