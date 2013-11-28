namespace BooksSearch
{
    using System;
    using System.Xml;
    using Bookstore.Data;

    public static class BookSearch
    {
        public static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test6.xml");

            string title = xmlDoc.GetChildText("/query/title");
            string author = xmlDoc.GetChildText("/query/author");
            string isbn = xmlDoc.GetChildText("/query/isbn");

            var books =
                BookstoreDAL.FindBooksByTitleAuthorOrIsbn(
                    title, author, isbn);

            //PrintQuery(title, author, isbn);

            if (books.Count > 0)
            {
                Console.WriteLine("{0} books found:", books.Count);
                foreach (var book in books)
                {
                    var reviewCount = BookstoreDAL.CountReviewsByTitle(book.BookID);
                    Console.WriteLine("{0}-->{1}", book.Title, reviewCount);
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

        private static void PrintQuery(string title,
            string author, string isbn)
        {
            Console.WriteLine();
            Console.WriteLine("{0} | {1} | {2}",
                author, title, isbn);
        }
    }
}