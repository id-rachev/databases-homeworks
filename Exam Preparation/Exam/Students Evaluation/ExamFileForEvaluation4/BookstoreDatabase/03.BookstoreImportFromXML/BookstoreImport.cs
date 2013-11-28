using System;
using System.Linq;
using System.Xml;
using Bookstore.Data;

namespace _03.BookstoreImportFromXML
{
    public static class BookstoreImport
    {
        static void Main()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../simple-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode bookNode in bookList)
            {

                string author = bookNode.GetChildText("author");
                string title = bookNode.GetChildText("title");
                string ISBN = bookNode.GetChildText("isbn");
                decimal price = 0;
                if (bookNode.GetChildText("price") != null)
                {
                    price = decimal.Parse(bookNode.GetChildText("price"));
                }

                string website = bookNode.GetChildText("web-site");

                BookstoreDAL.AddBookImportXml(author, title, ISBN, price, website);
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
