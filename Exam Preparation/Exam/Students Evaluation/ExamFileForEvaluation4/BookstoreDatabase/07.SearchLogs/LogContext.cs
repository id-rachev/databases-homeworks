using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace _07.SearchLogs
{
    public class LogContext : DbContext
    {
        public LogContext()
            : base("LogQueries")
        { 
        
        }

        public DbSet<LogQuery> LogQueries { get; set; }
    }
}
