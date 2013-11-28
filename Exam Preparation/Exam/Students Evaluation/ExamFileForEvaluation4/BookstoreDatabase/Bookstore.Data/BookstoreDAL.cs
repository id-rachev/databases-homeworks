using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Bookstore.Data
{
    static public class BookstoreDAL
    {
        

        //03.
        public static void AddBookImportXml(string authorName, string title, string ISBN, decimal price, string website)
        {
            BookstoreDBEntities context = new BookstoreDBEntities();

            Book newBook = new Book();
            newBook.Authors.Add(CheckIfAuthorExists(context, authorName));
            newBook.Title = CheckIfTitleExists(title);
            newBook.ISBN = ISBN;
            newBook.Price = price;
            newBook.OfficialWebSite = website;

            context.Books.Add(newBook);
            context.SaveChanges();
        }

        //04.
        public static void AddBookComplexImportXml(
            XmlNodeList authors, XmlNodeList reviews, string title, string ISBN, decimal price, string website)
        {
            BookstoreDBEntities context = new BookstoreDBEntities();

            Book newBook = new Book();

            if (authors!=null)
            {
                foreach (XmlNode author in authors)
                {
                    newBook.Authors.Add(CheckIfAuthorExists(context, author.InnerText));
                }
            }
            
            if (reviews != null)
            {
                foreach (XmlNode review in reviews)
                {
                    newBook.Reviews.Add(CreateReview(context, review));
                }
            }
            
            newBook.Title = CheckIfTitleExists(title);
            newBook.ISBN = ISBN;
            newBook.Price = price;
            newBook.OfficialWebSite = website;

            context.Books.Add(newBook);
            context.SaveChanges();
        }

        //04.
        private static Review CreateReview(BookstoreDBEntities context, XmlNode reviewNode)
        {
            Review newReview = new Review();
            newReview.ReviewContent = reviewNode.InnerText.Trim();

            if (reviewNode.Attributes["author"]!= null)
            {
                newReview.Author = CheckIfAuthorExists(context, reviewNode.Attributes["author"].Value);
            }
            

            if (reviewNode.Attributes["date"] != null)
            {
                newReview.DateOfCreation = DateTime.Parse(reviewNode.Attributes["date"].Value);
            }
            else
            {
                newReview.DateOfCreation = DateTime.Now;
            }

            context.Reviews.Add(newReview);
            context.SaveChanges();

            return newReview;
        }

        private static Author CheckIfAuthorExists(
            BookstoreDBEntities context, string authorName)
        {
            if (authorName==null)
            {
                throw new ArgumentNullException("the Author can not be null!");
            }

            Author existingAuthor =
                (from a in context.Authors
                 where a.Name.ToLower() == authorName.ToLower()
                 select a).FirstOrDefault();
            if (existingAuthor != null)
            {
                return existingAuthor;
            }

            Author newAuthor = new Author();
            newAuthor.Name = authorName;
            context.Authors.Add(newAuthor);
            context.SaveChanges();

            return newAuthor;
        }

        private static string CheckIfTitleExists(string title)
        {
            if (title == null)
            {
                throw new ArgumentNullException("the Title can not be null!");
            }

            return title;
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

        //05.
        public static IList<Book> FindBooks(string title, string authorName, string isbn)
        {
            BookstoreDBEntities context = new BookstoreDBEntities();
            var booksQuery =
                from b in context.Books.Include("Authors").Include("Reviews")
                select b;

            if (isbn != null)
            {
                booksQuery = booksQuery.Where(
                    b => b.ISBN == isbn);
            }

            if (authorName != null)
            {
                booksQuery = booksQuery.Where(
                    b => b.Authors.Any(a => a.Name.ToLower() == authorName.ToLower()));
            }

            if (title != null)
            {
                booksQuery = booksQuery.Where(
                    b => b.Title.ToLower() == title.ToLower());
            }

           
            booksQuery = booksQuery.OrderBy(b => b.Title);

            return booksQuery.ToList();
        }

        //06.
        public static IList<Review> QueriesByPeriod(XmlNode query)
        {
            BookstoreDBEntities context = new BookstoreDBEntities();
            var reviewQuery =
                from r in context.Reviews
                select r;

            DateTime startDate = DateTime.Parse(query.GetChildText("start-date"));
            DateTime endDate = DateTime.Parse(query.GetChildText("end-date"));

            reviewQuery = reviewQuery.Where(r => r.DateOfCreation >= startDate && r.DateOfCreation <= endDate);

            reviewQuery = reviewQuery.OrderBy(r => r.DateOfCreation).ThenBy(r => r.ReviewContent);

            return reviewQuery.ToList();
        }

        public static List<Review> QueriesByAuthor(XmlNode query)
        {
            string authorName = (query.GetChildText("author-name"));

            BookstoreDBEntities context = new BookstoreDBEntities();
            var authorQuery = context.Reviews.Include("Author").Include("Book").Select(r => r)
                .Where(a => a.Author.Name == authorName).OrderBy(r => r.DateOfCreation)
                .ThenBy(r => r.ReviewContent);

            return authorQuery.ToList();
        }
    }
}
