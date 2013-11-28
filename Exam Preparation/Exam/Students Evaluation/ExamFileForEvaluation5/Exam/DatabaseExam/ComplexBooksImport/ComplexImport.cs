using System;
using System.Linq;
using System.Xml;
using System.Transactions;
using BooksoreEntity;

namespace ComplexBooksImport
{
    static class ComplexImport
    {
        public static BookstoreEntities booksDB = new BookstoreEntities();

        static void Main()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../complex-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode book in booksList)
            {
                TransactionScope tran = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.RepeatableRead
                    });
                using (tran)
                {
                    string[] author = GetAllAuthors(book);
                    string title = GetChildText(book, "title");
                    string isbn = GetChildText(book, "isbn");
                    string price = GetChildText(book, "price");
                    string webSite = GetChildText(book, "web-site");

                    var addedBook = AddBook(author, title, isbn, price, webSite);

                    GetAllReviews(book, addedBook);
                    tran.Complete();
                }
            }
        }

        public static Book AddBook(string[] authors, string title, string isbn, string price, string webSite)
        {
            Book newBook = new Book();

            foreach (var author in authors)
            {
                CreateOrLoadAuthor(booksDB, author);
            }
           
            newBook.Title = title;
            newBook.Price = Convert.ToDecimal(price);
            newBook.ISBN = isbn;
            newBook.WebSite = webSite;

            booksDB.Books.Add(newBook);
            booksDB.SaveChanges();

            return newBook;
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

        private static string[] GetAllAuthors(this XmlNode node)
        {
            var childNode = node.SelectNodes("/catalog/book/authors/author");
            string[] authors = new string[childNode.Count];
            if (childNode == null)
            {
                return null;
            }
            for (int i = 0; i < childNode.Count; i++)
            {
                authors[i] = childNode[i].InnerText.Trim();
            }
            return authors;  
        }

        private static void GetAllReviews(this XmlNode node, Book currentBook)
        {
            Review newReview = new Review();
            var childNode = node.SelectNodes("/catalog/book/reviews/review");
            string[] reviews = new string[childNode.Count];

            for (int i = 0; i < childNode.Count; i++)
            {
                //var attr = childNode[i].Attributes;
                //if (attr == null || childNode[i].Attributes["date"].Value == null)
                //{
                //    newReview.DateOfCreation = DateTime.Now;
                //}
                //else if (childNode[i].Attributes.Name == "author")
                //{
                //    var author = CreateOrLoadAuthor(booksDB, childNode[i].Attributes["author"].Value);
                //    newReview.AuthorID = author.AuthorID;
                //}
                var reviewText = reviews[i] = childNode[i].InnerText.Trim();
                newReview.Review1 = reviewText;
                newReview.BookID = currentBook.BookID;
                booksDB.Reviews.Add(newReview);
                booksDB.SaveChanges();
            }
        }
    }
}
