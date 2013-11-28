using Bookmarks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Xml;

namespace Bookmark_Importer
{
    class BookmarksImporter
    {
        static void Main()
        {
            TransactionScope tran = new TransactionScope(
                TransactionScopeOption.Required,
                    new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.RepeatableRead
                    });
            using (tran)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("../../bookmarks.xml");
                string xPathQuery = "/bookmarks/bookmark";

                XmlNodeList bookmarksList = xmlDoc.SelectNodes(xPathQuery);
                foreach (XmlNode bookmarkNode in bookmarksList)
                {
                    string username = GetChildText(bookmarkNode, "username");
                    string title = GetChildText(bookmarkNode, "title");
                    string url = GetChildText(bookmarkNode, "url");
                    string notes = GetChildText(bookmarkNode, "notes");
                    string allTags = GetChildText(bookmarkNode, "tags");
                    string[] tags = { };
                    if (allTags != null)
                    {
                        tags = allTags.Split(',');
                        for (int i = 0; i < tags.Length; i++)
                        {
                            tags[i] = tags[i].Trim();
                        }
                    }
                    BookmarksDAL.AddBookmark(username, title, url, tags, notes);
                    Thread.Sleep(500);
                }
                tran.Complete();
            }
        }

        private static string GetChildText(XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                if (tagName != "tags" && tagName != "notes")
                {
                    throw new ArgumentNullException("childNode");
                }
                return null;
            }
            return childNode.InnerText;
        }
    }
}
