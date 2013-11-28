using System;
using System.Linq;
using System.Xml;
using System.Transactions;
using BooksoreEntity;

namespace Test
{
    static class SimpleBooks
    {
        public static BookstoreEntities booksDB = new BookstoreEntities();

        static void Main()
        {
            TransactionScope tran = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.RepeatableRead
                });
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../simple-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode book in booksList)
            {
                using (tran)
                {
                    string author = GetChildText(book, "author");
                    string title = GetChildText(book, "title");
                    string isbn = GetChildText(book, "isbn");
                    string price = GetChildText(book, "price");
                    string webSite = GetChildText(book, "web-site");

                    AddBook(author, title, isbn, price, webSite);
                    tran.Complete();
                }
            }
        }

        public static void AddBook(string author, string title, string isbn, string price, string webSite)
        {
            Book newBook = new Book();
            CreateOrLoadAuthor(booksDB, author);
            newBook.Title = title;
            newBook.Price = Convert.ToDecimal(price);
            newBook.ISBN = isbn;
            newBook.WebSite = webSite;

            booksDB.Books.Add(newBook);
            booksDB.SaveChanges();
        }

        private static Author CreateOrLoadAuthor(BookstoreEntities booksDB, string authorName)
        {
            Author existingAuthor = (from a in booksDB.Authors
                                     where a.Name.ToLower() == authorName.ToLower()
                                     select a).FirstOrDefault();

            if (existingAuthor != null)
            {
                return existingAuthor;
            }

            Author newAuthor = new Author();
            newAuthor.Name = authorName;
            booksDB.Authors.Add(newAuthor);
            booksDB.SaveChanges();

            return newAuthor;
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
