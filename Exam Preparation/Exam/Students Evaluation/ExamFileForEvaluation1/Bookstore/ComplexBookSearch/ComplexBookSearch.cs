namespace ComplexBookSearch
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Bookstore.Data;
    using System;

    public static class ComplexBookSearch
    {
        public static void Main()
        {
            string fileName = "../../reviews-search-results.xml";

            using (XmlTextWriter writer =
                new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;
                writer.WriteStartDocument();
                writer.WriteStartElement("search-results");
                ReviewsImport(writer);
                writer.WriteEndDocument();
            }
        }


        private static void ProcessSearchQueries(XmlTextWriter writer)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test1.xml");

            foreach (XmlNode query in xmlDoc.SelectNodes("/review-queries/query[@type='by-period']"))
            {
                string authorName = query.GetChildText("author-name");
                var startDate = query.Attributes["start-date"].Value;
                var endDate = query.Attributes["end-date"].Value;

                List<string> booksList = new List<string>();

                if (authorName != null)
                {
                    foreach (XmlNode book in query.SelectNodes("author-name"))
                    {
                        booksList.Add(book.InnerText.Trim());
                    }
                }

                var reviews =
                    BookstoreDAL.FindReviewsByAuthorAndPeriod(authorName, startDate, endDate);

                WriteReviews(writer, reviews);
            }
        }

        private static void ReviewsImport(XmlTextWriter writer)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../test5.xml");
            string xPathQuery = "/review-queries/query[@type='by-period']";

            string authorName = "";
            string startDate = "";
            string endDate = "";

            XmlNodeList periodsList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode periodNode in periodsList)
            {
                startDate = periodNode.SelectSingleNode("start-date").InnerText;
                Console.WriteLine(startDate);

                endDate = periodNode.SelectSingleNode("end-date").InnerText;
                Console.WriteLine(endDate);
            }

            xPathQuery = "/review-queries/query[@type='by-author']";

            XmlNodeList authorsList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode authorNode in authorsList)
            {
                authorName = authorNode.SelectSingleNode("author-name").InnerText;
                Console.WriteLine(authorName);
            }

            var reviews =
                BookstoreDAL.FindReviewsByAuthorAndPeriod(authorName, startDate, endDate);

            WriteReviews(writer, reviews);
        }

        private static void WriteReviews(
            XmlTextWriter writer, IList<Review> reviews)
        {
            writer.WriteStartElement("result-set");

            foreach (var review in reviews)
            {
                writer.WriteStartElement("review");

                if (review.CreationDate != null)
                {
                    writer.WriteElementString("date", review.CreationDate.ToString());
                }

                if (review.Review1 != null)
                {
                    writer.WriteElementString("content", review.Review1);
                }

                writer.WriteStartElement("book");

                if (review.Book.Title != null)
                {
                    writer.WriteElementString("title", review.Book.Title);
                }

                if (review.Book.Authors != null)
                {
                    string authors = string.Join(", ",
                        review.Book.Authors.Select(
                            a => a.Author1).OrderBy(a => a));

                    writer.WriteElementString("isbn", authors);
                }

                if (review.Book.ISBN != null)
                {
                    writer.WriteElementString("isbn", review.Book.ISBN);
                }

                if (review.Book.Website != null)
                {
                    writer.WriteElementString("url", review.Book.Website);
                }

                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
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