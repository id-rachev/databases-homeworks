using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;

namespace Bookstore.Data
{
    public class BookstoreDAL
    {
        public static void AddComplexBook(string title, string isbn, 
            string price, string webSite, List<string> autors, List<string> reviews)
        {

        }

        public static void AddSimpleBook(string title, string author,
            string isbn, string price, string webSite)
        {
            BookstoreEntities context = new BookstoreEntities();
            Book newBook = new Book();
            newBook.Title = title;
            newBook.ISBN = isbn;
            newBook.Price = 0;
            if (!String.IsNullOrEmpty(price))
            {
                newBook.Price = Convert.ToDecimal(price);
            }
            newBook.WebSite = webSite;
            Author newAuthor = CreateOrLoadAuthor(context, author);
            newBook.Authors.Add(newAuthor);

            context.Books.Add(newBook);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        private static Author CreateOrLoadAuthor(
            BookstoreEntities context, string author)
        {
            Author existingAuthor =
                (from a in context.Authors
                 where a.Name.ToLower() == author.ToLower()
                 select a).FirstOrDefault();
            if (existingAuthor != null)
            {
                return existingAuthor;
            }

            Author newAuthor = new Author();
            newAuthor.Name = author;
            context.Authors.Add(newAuthor);
            context.SaveChanges();

            return newAuthor;
        }
    }
}
