using Library.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
                List<Author> authorList;
                if (!_dbContext.Authors.Any())
                {
                    authorList = new List<Author>
                    {
                        new Author { FirstName = "Henryk", LastName = "Sienkiewicz" },
                        new Author { FirstName = "Rowan", LastName = "Miller" },
                        new Author { FirstName = "Julia", LastName = "Lerman" }

                    };
                    _dbContext.Authors.AddRange(authorList);
                }
                else
                {
                    authorList = _dbContext.Authors.ToList();
                }

                List<Book> bookList;
                
                if (!_dbContext.Books.Any())
                {
                    bookList = new List<Book>
                    {
                        new Book { Name = "Krzyżacy", ISBN = "1234567890", Authors = new List<Author> { authorList[0]} },
                        new Book { Name = "Programming Entity Framework: DbContext", ISBN = "0987654321", Authors = new List<Author> { authorList[1], authorList[2]} }

                    };
                    _dbContext.Books.AddRange(bookList);
                }
                else
                {
                    bookList = _dbContext.Books.ToList();
                }

                List<IdentityRole> rolesList;

                if (!_dbContext.Roles.Any())
                {
                    rolesList = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "User", NormalizedName = "USER" },
                        new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" }

                    };
                    _dbContext.Roles.AddRange(rolesList);
                }
                else
                {
                    rolesList = _dbContext.Roles.ToList();
                }

                _dbContext.SaveChanges();
            }
        }

        //private IEnumerable<Book> GetBooks()
        //{
        //    var books = new List<Book>()
        //   {
        //       new Book()
        //       {
        //           Name = "Krzyżacy",
        //           ISBN = "1234567890",
        //           Authors = new List<Author>
        //           {
        //               new Author()
        //               {
        //                   FirstName = "Henryk",
        //                   LastName = "Sienkiewicz"
        //               }
        //           }
        //       },
        //       new Book()
        //       {
        //           Name = "Programming Entity Framework: DbContext",
        //           ISBN = "0987654321",
        //           Authors = new List<Author>
        //           {
        //               new Author()
        //               {
        //                   FirstName = "Julia",
        //                   LastName = "Lerman"
        //               },
        //               new Author()
        //               {
        //                   FirstName = "Rowan",
        //                   LastName = "Miller"
        //               }
        //           }
        //       }
        //   };
        //    return books;
        //}



    }
}
