using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.SearchLogs
{
    [Table("Logs")]
    public class LogQuery
    {
        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("QueryXml")]
        public string QueryXML { get; set; }
    }
}
