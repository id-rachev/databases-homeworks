using System;
using System.Linq;
using System.Data.Entity;
using Logs;

namespace LogsContext
{
    public class LogsContext:DbContext
    {
         public LogsContext()
            :base ("Logs")
        {
        }

         public DbSet<SearchLog> Logs { get; set; }
    }
}
