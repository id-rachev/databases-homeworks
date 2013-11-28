using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;


   static class BookstoreImporter
    {
       static void Main(string[] args)
       {
           
            TransactionScope tran =	new TransactionScope(
            TransactionScopeOption.Required,
                new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.RepeatableRead
                    });
            using (tran)
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../../test5.xml");
                string xPathQuery = "/catalog/book";

                XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
                foreach (XmlNode bookNode in bookList)
                {
                    string allAuthor = bookNode.GetChildText("author");
                    string[] authors = { };
                    if (allAuthor != null)
                    {
                        authors = allAuthor.Split(',');
                        for (int i = 0; i < authors.Length; i++)
                        {
                            authors[i] = authors[i].Trim();
                        }
                    }
                    string title = bookNode.GetChildText("title");
                    string isbn = bookNode.GetChildText("isbn");
                    string price = bookNode.GetChildText("price");
                    string website = bookNode.GetChildText("web-site");

                    BookstoreDAL.AddBook(authors, title, isbn, price, website);

                }
                tran.Complete();
            }
            
       }

            
        private static string GetChildText(this XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
              if(childNode==null)
              {
                  return null;
              }
            return childNode.InnerText.Trim();
        }

    }

