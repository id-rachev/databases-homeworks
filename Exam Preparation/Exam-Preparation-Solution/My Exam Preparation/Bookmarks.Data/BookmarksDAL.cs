using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Bookmarks.Data
{
    public class BookmarksDAL
    {
		public static void AddBookmark(string username,
			string title, string url, string[] tags, string notes)
		{
			BookmarksEntities context = new BookmarksEntities();
			Bookmark newBookmark = new Bookmark();
			newBookmark.User = CreateOrLoadUser(context, username);
			newBookmark.Title = title;
			newBookmark.URL = url;
			newBookmark.Notes = notes;
			foreach (var tagName in tags)
			{
				Tag tag = CreateOrLoadTag(context, tagName);
				newBookmark.Tags.Add(tag);
			}
			string[] titleTags = Regex.Split(title, @"[,'!\. ;?-]+");
			foreach (var titleTagName in titleTags)
			{
				Tag titleTag = CreateOrLoadTag(context, titleTagName);
				newBookmark.Tags.Add(titleTag);
			}
			context.Bookmarks.Add(newBookmark);
			context.SaveChanges();
		}

		private static User CreateOrLoadUser(
			BookmarksEntities context, string username)
		{
			User existingUser =
				(from u in context.Users
				where u.Username.ToLower() == username.ToLower()
				select u).FirstOrDefault();
			if (existingUser != null)
			{
				return existingUser;
			}

			User newUser = new User();
			newUser.Username = username;
			context.Users.Add(newUser);
			context.SaveChanges();

			return newUser;
		}

		private static Tag CreateOrLoadTag(
			BookmarksEntities context, string tagName)
		{
			Tag existingTag =
				(from t in context.Tags
				 where t.Name.ToLower() == tagName.ToLower()
				 select t).FirstOrDefault();
			if (existingTag != null)
			{
				return existingTag;
			}

			Tag newTag = new Tag();
			newTag.Name = tagName.ToLower();
			context.Tags.Add(newTag);
			context.SaveChanges();

			return newTag;
		}
    }
}
