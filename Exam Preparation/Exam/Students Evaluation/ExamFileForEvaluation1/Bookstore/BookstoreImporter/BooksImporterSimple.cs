namespace BookstoreImporter
{
    using System;
    using System.Transactions;
    using System.Xml;
    using Bookstore.Data;

    public static class BooksImporterSimple
    {
        public static void Main()
        {
            TransactionScope tran = new TransactionScope(
            TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.RepeatableRead
                });

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../simple-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);

            foreach (XmlNode bookNode in booksList)
            {
                string author = bookNode.GetChildText("author");
                string title = bookNode.GetChildText("title");
                string isbn = bookNode.GetChildText("isbn");
                string price = bookNode.GetChildTextNum("price");
                string website = bookNode.GetChildText("web-site");

                using (tran)
                {
                    BookstoreDAL.AddBook(author, title, isbn, price, website);
                    //tran.Complete();
                }
                AddBook(author, title, isbn, price, website);
            }
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

        private static string GetChildTextNum(this XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                return "0";
            }

            return childNode.InnerText.Trim();
        }
        private static void AddBook(string author,
            string title, string isbn, string price, string website)
        {
            Console.WriteLine();
            Console.WriteLine("{0} {1} {2} {3} {4}",
                author, title, isbn, price, website);
        }
    }
}
