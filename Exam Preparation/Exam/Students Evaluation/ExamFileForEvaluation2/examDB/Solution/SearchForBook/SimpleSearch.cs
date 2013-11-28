using System;
using System.Xml;
using Bookstore.Model;

namespace SearchForBook
{
    static class SimpleSearch
    {
        static void Main(string[] args)
        {
            // Problem 5
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test5.xml");
            string title = xmlDoc.GetChildText("/query/title");
            string author = xmlDoc.GetChildText("/query/author");
            string isbn = xmlDoc.GetChildText("/query/isbn");
            var books =
                BooksDAL.FindBooks(title,author,isbn);
            if (books.Count > 0)
            {
                Console.WriteLine(books.Count + " books found:");
                foreach (var book in books)
                {
                    Console.Write(book.Title);
                    if(book.Reviews.Count > 0)
                        Console.WriteLine(" --> " + book.Reviews.Count + " reviews");
                    else
                        Console.WriteLine(" --> no reviews");
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
