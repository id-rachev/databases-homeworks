using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BooksoreEntity;

namespace SimpleSearch
{
    static class SimpleSearch
    {
        public static BookstoreEntities booksDB = new BookstoreEntities();

        static void Main()
        { 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test6.xml");
            string title = xmlDoc.GetChildText("/query/title");
            string author = xmlDoc.GetChildText("/query/author");
            string isbn = xmlDoc.GetChildText("/query/isbn");

            var books = FindBookmarksByUsernameAndTag(title, author, isbn);

            if (books.Count > 0)
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " --> " + book.Reviews.Count());
                }
            }
            else
            {
                Console.WriteLine("Nothing found");
            }
        }

        private static IList<Book> FindBookmarksByUsernameAndTag(string title, string author, string isbn)
        {
            var booksQuery =
                from b in booksDB.Books
                select b;

            //if (author != null)
            //{
            //    booksQuery.Where(b => b.Authors.)

            //}
            if (isbn != null)
            {
                booksQuery =
                    from b in booksDB.Books
                    where b.ISBN == isbn
                    select b;
            }
            if (title != null)
            {
                booksQuery =
                    from b in booksDB.Books
                    where b.Title == title
                    select b;
            }

            booksQuery = booksQuery.OrderBy(b => b.Title);

            return booksQuery.ToList();
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
