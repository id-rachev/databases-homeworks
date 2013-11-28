using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace Bookstore.Model
{
    public class BooksDAL
    {
        public static void AddBook(string[] authors, string title, string isbn, decimal? price, string website, IEnumerable<XElement> reviews, IEnumerable<XElement> authorsCol)
        {
            BookstoreEntities context = new BookstoreEntities();

            Book newBook = new Book();
            if (authorsCol == null)
            {
                ICollection<Author> authorsColection = CreateOrLoadAuthors(context, authors);
                foreach (var author in authorsColection)
                {
                    newBook.Authors.Add(author);
                    context.SaveChanges();
                }
            }
            else
            {
                foreach (var author in authorsCol)
                {
                    newBook.Authors.Add(CreateOrLoadAuthor(context, author.Value));
                }
            }
            newBook.Title = title;
            newBook.ISBN = isbn;
            newBook.Price = price;

            newBook.Website = website;

            ICollection<Review> reviewsCollection;
            if (reviews != null)
            {
                reviewsCollection = GetReviews(context, reviews);
                foreach (var review in reviewsCollection)
                {
                    newBook.Reviews.Add(review);
                }
            }
            context.Books.Add(newBook);
            context.SaveChanges();
        }

        private static ICollection<Review> GetReviews(BookstoreEntities context, IEnumerable<XElement> reviews)
        {
            ICollection<Review> collection = new Collection<Review>();
            foreach (var element in reviews)
            {
                Review newReview = new Review();
                newReview.Text = element.Value.Trim();
                if (element.Attribute("date") != null)
                    newReview.DateOfCreation = DateTime.Parse(element.Attribute("date").Value);

                if (element.Attribute("author") != null)
                    newReview.Author = CreateOrLoadAuthor(context, element.Attribute("author").Value);

                collection.Add(newReview);
            }
            return collection;
        }

        private static Author CreateOrLoadAuthor(BookstoreEntities context, string author)
        {
            Author existingAuthor =
                    (from a in context.Authors
                     where a.Name == author
                     select a).FirstOrDefault();

            if (existingAuthor == null)
            {
                Author newAuthor = new Author { Name = author };
                context.Authors.Add(newAuthor);
                context.SaveChanges();
                return newAuthor;
            }

            context.SaveChanges();
            return existingAuthor;
        }

        private static ICollection<Author> CreateOrLoadAuthors(BookstoreEntities context, string[] authors)
        {
            ICollection<Author> authorsCollection = new Collection<Author>();
            foreach (string author in authors)
            {
                Author existingAuthor =
                    (from a in context.Authors
                     where a.Name == author
                     select a).FirstOrDefault();

                if (existingAuthor == null)
                {
                    Author newAuthor = new Author { Name = author };
                    context.Authors.Add(newAuthor);
                    authorsCollection.Add(newAuthor);
                }
                else
                {
                    authorsCollection.Add(existingAuthor);
                }

                context.SaveChanges();
            }
            return authorsCollection;
        }

        public static IList<Book> FindBooks(
            string title, string author, string isbn)
        {
            BookstoreEntities context = new BookstoreEntities();
            var booksQuery =
                from b in context.Books
                select b;
            if (title != null)
            {
                booksQuery =
                    from b in context.Books
                    where b.Title.ToLower() == title.ToLower()
                    select b;
            }
            if (author != null)
            {
                booksQuery = booksQuery.Where(
                    b => b.Authors.Any(a => a.Name.ToLower() == author.ToLower()));
            }
            if (isbn != null)
            {
                booksQuery = booksQuery.Where(
                    b => b.ISBN == isbn);
            }
            booksQuery = booksQuery.OrderBy(b => b.Title);

            Console.WriteLine(booksQuery.ToString());

            return booksQuery.ToList();
        }

        public static object FindReviews(DateTime start, DateTime end)
        {
            BookstoreEntities context = new BookstoreEntities();

            var reviewsQuery =
                from r in context.Reviews
                select r;

            reviewsQuery =
                (from r in context.Reviews
                 where r.DateOfCreation > start && r.DateOfCreation < end
                 orderby r.DateOfCreation, r.Text
                 select r
                );

            Console.WriteLine(reviewsQuery.ToString());

            return reviewsQuery.ToList();
        }

        public static object FindReviews(string authorName)
        {
            throw new NotImplementedException();
        }
    }
}
