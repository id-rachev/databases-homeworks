using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class BookstoreDAL
    {
        public static void AddBook(string[] authors, string title, string isbn,
            string price, string website)
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
            newBook.Website = website;

            foreach (var authorName in authors)
            {
                Author author = CreateOrLoadAuthor(context, authorName);
                newBook.Authors.Add(author);
            }

            context.Books.Add(newBook);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Author CreateOrLoadAuthor
            (BookstoreEntities context, string authorName)
        {
            Author existingAuthor =
                (from a in context.Authors
                 where a.Name.ToLower() == authorName.ToLower()
                 select a).FirstOrDefault();
            if (existingAuthor != null)
            {
                return existingAuthor;
            }
            Author newAuthor = new Author();
            newAuthor.Name = authorName.ToLower();
            context.Authors.Add(newAuthor);

            context.SaveChanges();

            return newAuthor;
        }
    }
}





