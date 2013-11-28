using System;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Data;
using System.Xml;

namespace _05.SimpleSearchForBooks
{
    public static class SimpleQuery
    {
        static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test6.xml");
            string title = xmlDoc.GetChildText("/query/title");
            string author = xmlDoc.GetChildText("/query/author");
            string isbn = xmlDoc.GetChildText("/query/isbn");
            var books = BookstoreDAL.FindBooks(title, author, isbn);

            Console.WriteLine("{0} books found:", books.Count);

            if (books.Count > 0)
            {
                foreach (var book in books)
                {
                    Console.WriteLine("{0} --> {1} reviews", book.Title, book.Reviews.Count);
                }
            }
            else
            {
                Console.WriteLine("Nothing found");
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
