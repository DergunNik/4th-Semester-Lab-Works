using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Entities
{
    [Table("Authors")]
    public class Author
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Country { get; set; }
    }
}
