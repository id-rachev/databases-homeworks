namespace Bookstore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class BookstoreDAL
    {
        public static void AddBook(string author, string title, string isbn, string price, string website)
        {
            BookstoreEntities context = new BookstoreEntities();
            Book newBook = new Book();

            newBook.Authors.Add(CreateOrLoadAuthor(context, author));
            newBook.Title = title;
            newBook.ISBN = isbn;            
            newBook.Price = decimal.Parse(price);
            newBook.Website = website;
            
            context.Books.Add(newBook);
            context.SaveChanges();
        }

        public static void AddBookComplex(string[] authors, string title, string isbn, string price, string website, string[] reviews)
        {
            BookstoreEntities context = new BookstoreEntities();
            Book newBook = new Book();

            for (int i = 0; i < authors.Length; i++)
            {
                string author = authors[i];
                newBook.Authors.Add(CreateOrLoadAuthor(context, author));
            }
            
            newBook.Title = title;
            newBook.ISBN = isbn;
            newBook.Price = decimal.Parse(price);
            newBook.Website = website;

            for (int i = 0; i < reviews.Length; i++)
            {
                string review = reviews[i];
                //newBook.Reviews.Add(review);
            }

            context.Books.Add(newBook);
            context.SaveChanges();
        }

        private static Author CreateOrLoadAuthor(
            BookstoreEntities context, string author)
        {
            Author existingAuthor =
                (from a in context.Authors
                 where a.Author1.ToLower() == author.ToLower()
                 select a).FirstOrDefault();

            if (existingAuthor != null)
            {
                return existingAuthor;
            }

            Author newAuthor = new Author();
            newAuthor.Author1 = author;

            context.Authors.Add(newAuthor);
            context.SaveChanges();

            return newAuthor;
        }

        public static IList<Book> FindBooksByTitleAuthorOrIsbn(
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
                    b => b.Authors.Any(a =>
                                           a.Author1.ToLower() == author.ToLower()));
            }

            if (isbn != null)
            {
                booksQuery = from b in context.Books
                             where b.ISBN == isbn
                             select b;
            }

            booksQuery = booksQuery.OrderBy(b => b.Title);
            return booksQuery.ToList();
        }

        public static string CountReviewsByTitle(int bookId)
        {
            BookstoreEntities context = new BookstoreEntities();
             
            var reviewQuery = from r in context.Reviews
                              where r.BookID == bookId
                              select r;

            if (reviewQuery.Count() > 0)
            {
                return reviewQuery.Count().ToString() + "reviews";
            }

            return "no reviews";
        }

        public static IList<Review> FindReviewsByAuthorAndPeriod(
            string author, string startDate, string endDate)
        {
            BookstoreEntities context = new BookstoreEntities();
            var reviewsQuery =
                              from r in context.Reviews
                              select r;

            if (author != null)
            {
                reviewsQuery =
                              from r in context.Reviews
                              where r.Author.Author1 == author
                              select r;
            }

            if (startDate != null && endDate != null)
            {
                var startDateParsed = DateTime.ParseExact(startDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

                var endDateParsed = DateTime.ParseExact(endDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

                reviewsQuery = from r in context.Reviews
                               where r.CreationDate >= startDateParsed &&
                                     r.CreationDate <= endDateParsed
                               select r;
            }

            reviewsQuery = reviewsQuery.OrderBy(r => r.CreationDate)
                                       .ThenBy(r => r.Review1);

            Console.WriteLine(reviewsQuery.ToString());

            return reviewsQuery.ToList();
        }
    }
}