using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.SearchLogs
{
    public class LogDAL
    {
        public static void AddQueryToBase(string query)
        {
            LogContext context = new LogContext();

            context.LogQueries.Add(new LogQuery
            {
                Date = DateTime.Now,
                QueryXML = query
            });

            context.SaveChanges();
        }
    }
}
