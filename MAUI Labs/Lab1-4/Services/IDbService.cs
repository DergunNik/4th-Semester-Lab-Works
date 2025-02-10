using Lab1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public interface IDbService
    {
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Book> GetAuthorBooks(int id);
        void Init();
    }
}
