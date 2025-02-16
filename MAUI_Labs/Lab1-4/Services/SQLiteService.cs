using Lab1.Entities;
using Lab1.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public class SQLiteService : IDbService
    {
        private SQLiteConnection connection;
        private readonly string DB_PATH =  Path.Combine(FileSystem.AppDataDirectory, "authors_books.db");

        public IEnumerable<Author> GetAllAuthors()
        {
            return from author in connection.Table<Author>()
                   select author;
        }

        public IEnumerable<Book> GetAuthorBooks(int id)
        {
            return (from book in connection.Table<Book>()
                   where book.AuthorId == id
                   select book).OrderBy(b => b.PublicationYear);
        }

        public void Init()
        {
            if (File.Exists(DB_PATH))
            {
                connection = new SQLiteConnection(DB_PATH);
                return;
            }
            connection = new SQLiteConnection(DB_PATH);
            connection.CreateTables<Author, Book>();
            IEnumerable<Author> authors = new List<Author> 
            {
                new Author { Name = "Isaac Asimov", Country = "USA"},
                new Author { Name = "Erich Maria Remarque", Country = "Germany/USA"},
                new Author { Name = "Mikhail Bulgakov", Country = "Russian Empire/USSR"},
            };
            connection.InsertAll(authors);
            int asimovId = (from author in connection.Table<Author>()
                            where author.Name == "Isaac Asimov"
                            select author.Id).First<int>();
            int remarqueId = (from author in connection.Table<Author>()
                              where author.Name == "Erich Maria Remarque"
                              select author.Id).First<int>();
            int bulgakovId = (from author in connection.Table<Author>()
                              where author.Name == "Mikhail Bulgakov"
                              select author.Id).First<int>();
            IEnumerable < Book > books = new List<Book>
            {
                new Book { Title = "Foundation", PublicationYear = 1951, AuthorId = asimovId },
                new Book { Title = "I, Robot", PublicationYear = 1950, AuthorId = asimovId },
                new Book { Title = "Norby", PublicationYear = 1983, AuthorId = asimovId },
                new Book { Title = "The End of Eternity", PublicationYear = 1955, AuthorId = asimovId },
                new Book { Title = "And Then There Were None", PublicationYear = 1951, AuthorId = asimovId },

                new Book { Title = "All Quiet on the Western Front", PublicationYear = 1929, AuthorId = remarqueId },
                new Book { Title = "Shadows in Paradise", PublicationYear = 1971, AuthorId = remarqueId },
                new Book { Title = "Three Comrades", PublicationYear = 1937, AuthorId = remarqueId },
                new Book { Title = "Arch of Triumph", PublicationYear = 1945, AuthorId = remarqueId },
                new Book { Title = "A Time to Love and a Time to Die", PublicationYear = 1954, AuthorId = remarqueId },

                new Book { Title = "The Master and Margarita", PublicationYear = 1966, AuthorId = bulgakovId },
                new Book { Title = "Heart of a Dog", PublicationYear = 1925, AuthorId = bulgakovId },
                new Book { Title = "The White Guard", PublicationYear = 1926, AuthorId = bulgakovId },
                new Book { Title = "A Young Doctor's Notebook", PublicationYear = 1926, AuthorId = bulgakovId },
                new Book { Title = "Diaboliad", PublicationYear = 1924, AuthorId = bulgakovId }
            };
            connection.InsertAll(books);
        }
    }
}
