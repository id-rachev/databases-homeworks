namespace SearchLogger
{
    using System;
    using System.Data.Entity;

    public class Program
    {
        static void Main()
        { 
            using (var db = new SearchLogContext())
            {
                // TODO
            }
        }
    }
    public class SearchLog
    {
        public int id { get; set; }
        public DateTime logDate { get; set; }
        public string queryXml { get; set; }
    }

    public class SearchLogContext : DbContext
    {
        public DbSet<SearchLog> Logs { get; set; }
    }
}
