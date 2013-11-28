using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _04.ComplexBookImportFromXML
{
    static class ComplexBookmarkImport
    {
        static void Main()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../complex-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode bookNode in bookList)
            {

                XmlNode authorsPerm = bookNode.SelectSingleNode("authors");
                XmlNode reviewsPerm = bookNode.SelectSingleNode("reviews");

                XmlNodeList authors = null;
                XmlNodeList reviews = null;

                if (authorsPerm != null)
                {
                    authors = authorsPerm.SelectNodes("author");
                }

                if (reviewsPerm!=null)
                {
                    reviews = reviewsPerm.SelectNodes("review");
                }

                string title = bookNode.GetChildText("title");
                string ISBN = bookNode.GetChildText("isbn");
                decimal price = 0;
                if (bookNode.GetChildText("price") != null)
                {
                    price = decimal.Parse(bookNode.GetChildText("price"));
                }

                string website = bookNode.GetChildText("web-site");

                BookstoreDAL.AddBookComplexImportXml(authors, reviews, title, ISBN, price, website);
            }
        }

        private static string GetChildText(this XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                return null;
            }
            return childNode.InnerText.Trim();
        }
    }
}
