using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StudentSystem.Models
{
    public class Homework
    {
        public Homework()
        {
            this.TimeSent = DateTime.Now;
        }

        [Key, Column("HomeworkId")]
        public int Id { get; set; }

        [Column("HomeworkContent"), Required]
        public string Content { get; set; }

        [Column("HomeworkSentTime")]
        public DateTime TimeSent { get; set; }
    }
}
