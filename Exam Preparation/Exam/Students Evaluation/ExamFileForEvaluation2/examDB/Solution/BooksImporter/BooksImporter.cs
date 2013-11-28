using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml.Linq;
using Bookstore.Model;

namespace BooksImporter
{
    class BooksImporter
    {
        static void Main()
        {
            // Problem 3
            SimpleBooksImport();
            // Problem 4
            ComplexBooksImport();
        }

        private static void SimpleBooksImport()
        {
            XDocument doc = XDocument.Load("../../simple-books.xml");

            IEnumerable<XElement> elements =
                (from e in doc.Descendants("book")
                 select e);

            foreach (XElement el in elements)
            {
                string[] authors = { };
                if (GetElementValue(el, "author") != null)
                    authors = GetElementValue(el, "author").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(auth => auth.Trim()).ToArray();
                string title = GetElementValue(el, "title");
                string isbn = GetElementValue(el, "isbn");
                string priceString = GetElementValue(el, "price");
                decimal? price = null;
                if (priceString != null)
                    price = decimal.Parse(priceString);
                string website = GetElementValue(el, "web-site");

                BooksDAL.AddBook(authors, title, isbn, price, website, null, null);
            }
        }

        private static void ComplexBooksImport()
        {
            TransactionScope tran =	new TransactionScope(
            TransactionScopeOption.Required,
                new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.RepeatableRead
                    });
            using (tran)
            {
                XDocument doc = XDocument.Load("../../complex-books.xml");

                IEnumerable<XElement> elements =
                    (from e in doc.Descendants("book")
                     select e);

                foreach (XElement el in elements)
                {
                    string[] authors = {};
                    if (GetElementValue(el, "author") != null)
                        authors =
                            GetElementValue(el, "author").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                                                         .Select(auth => auth.Trim()).ToArray();
                    string title = GetElementValue(el, "title");
                    string isbn = GetElementValue(el, "isbn");
                    string priceString = GetElementValue(el, "price");
                    decimal? price = null;
                    if (priceString != null)
                        price = decimal.Parse(priceString);
                    string website = GetElementValue(el, "web-site");
                    IEnumerable<XElement> reviews = GetReviewElements(el, "reviews");
                    IEnumerable<XElement> authorsCol = GetAuthorElements(el, "authors");
                    BooksDAL.AddBook(authors, title, isbn, price, website, reviews, authorsCol);
                }
                tran.Complete();
            }
        }

        private static IEnumerable<XElement> GetAuthorElements(XElement el, string authors)
        {
            var xElement = el.Elements(authors);
            var reviewsCollection =
                (from e in xElement.Descendants("author")
                 select e);

            return reviewsCollection;
        }

        private static IEnumerable<XElement> GetReviewElements(XElement el, string p)
        {
            var xElement = el.Elements(p);
            var reviewsCollection =
                (from e in xElement.Descendants("review")
                 select e);

            return reviewsCollection;
        }

        private static string GetElementValue(XElement el, string node)
        {
            var xElement = el.Element(node);

            return xElement == null ? null : xElement.Value;
        }
    }
}
