using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Entities
{
    [Table("Books")]
    public class Book
    {
        [PrimaryKey, AutoIncrement, Indexed]
        [Column("Id")]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        [Indexed]
        public int AuthorId { get; set; }
    }
}
