using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Book>()
                .Property(x => x.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Author>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Author>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            //modelBuilder.Entity<Book>().HasData(
            //    new Book()
            //    {
            //        Id = 1,
            //        Name = "Krzyżacy",
            //        ISBN = "1234567890",
            //        Authors = new List<Author>
            //       {
            //           new Author()
            //           {
            //               Id = 1,
            //               FirstName = "Henryk",
            //               LastName = "Sienkiewicz"
            //           }
            //       }
            //    },
            //   new Book()
            //   {
            //       Id = 2,
            //       Name = "Programming Entity Framework: DbContext",
            //       ISBN = "0987654321",
            //       Authors = new List<Author>
            //       {
            //           new Author()
            //           {
            //               Id = 2,
            //               FirstName = "Julia",
            //               LastName = "Lerman"
            //           },
            //           new Author()
            //           {
            //               Id = 3,
            //               FirstName = "Rowan",
            //               LastName = "Miller"
            //           }
            //       }
            //   }
            //    );

        }
    }
}
