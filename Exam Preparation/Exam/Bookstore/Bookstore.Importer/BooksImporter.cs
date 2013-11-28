using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml;

namespace Bookstore.Importer
{
    class BooksImporter
    {
        static void Main()
        {
            TransactionScope transaction = new TransactionScope(
                TransactionScopeOption.Required,
                    new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.RepeatableRead
                    });
            using (transaction)
            {
                SimpleBooksImport();

                //ComplexBooksImport();

                transaction.Complete();
            }
        }

        private static void ComplexBooksImport()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../complex-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode book in booksList)
            {
                string title = GetChildText(book, "title");
                string isbn = GetChildText(book, "isbn");
                string price = GetChildText(book, "price");
                string webSite = GetChildText(book, "web-site");
                List<string> authors = GetChildNodes(book, "authors");
                List<string> reviews = GetChildNodes(book, "reviews");

                BookstoreDAL.AddComplexBook(title, isbn, price, webSite, authors, reviews);
            }
        }
  
        private static void SimpleBooksImport()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../simple-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode book in booksList)
            {
                string title = GetChildText(book, "title");
                string isbn = GetChildText(book, "isbn");
                string price = GetChildText(book, "price");
                string webSite = GetChildText(book, "web-site");
                string author = GetChildText(book, "author");

                BookstoreDAL.AddSimpleBook(title, author, isbn, price, webSite);
            }
        }

        private static List<string> GetChildNodes(XmlNode node, string tagName)
        {
            List<string> subTagsList = new List<string>();
            foreach (XmlNode childNode in node)
            {


                string currentTag = GetChildText(childNode, tagName);
                subTagsList.Add(currentTag);
            }

            return subTagsList;
        }

        private static string GetChildText(XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                if (tagName == "author" || tagName == "title")
                {
                    throw new ArgumentNullException("childNode", 
                        "Required element is missing!");
                }
                return null;
            }
            return childNode.InnerText.Trim();
        }
    }
}
