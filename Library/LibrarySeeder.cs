using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class LibrarySeeder
    {
        private readonly LibraryDbContext _dbContext;
        public LibrarySeeder(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Books.Any())
                {
                    var books = GetBooks();
                    _dbContext.Books.AddRange(books);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>()
           {
               new Book()
               {
                   Name = "Krzyżacy",
                   ISBN = "1234567890",
                   Authors = new List<Author>
                   {
                       new Author()
                       {
                           FirstName = "Henryk",
                           LastName = "Sienkiewicz"
                       }
                   }
               },
               new Book()
               {
                   Name = "Programming Entity Framework: DbContext",
                   ISBN = "0987654321",
                   Authors = new List<Author>
                   {
                       new Author()
                       {
                           FirstName = "Julia",
                           LastName = "Lerman"
                       },
                       new Author()
                       {
                           FirstName = "Rowan",
                           LastName = "Miller"
                       }
                   }
               }
           };
            return books;
        }
    }
}
