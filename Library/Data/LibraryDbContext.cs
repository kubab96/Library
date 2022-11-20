using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryDbContext : IdentityDbContext<ApiUser>
    {
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(eb =>
            {
                eb.Property(n => n.Name).IsRequired();
                eb.Property(n => n.Name).HasMaxLength(50);
                eb.Property(i => i.ISBN).IsRequired();
                eb.Property(i => i.ISBN).HasMaxLength(13);

                eb.HasMany(a => a.Authors)
                .WithMany(b => b.Books);
            });

            modelBuilder.Entity<Author>(eb =>
            {
                eb.Property(f => f.FirstName).IsRequired();
                eb.Property(f => f.FirstName).HasMaxLength(50);
                eb.Property(l => l.LastName).IsRequired();
                eb.Property(l => l.LastName).HasMaxLength(50);

                //eb.HasMany(a => a.Books)
                //.WithMany(b => b.Authors);
            });


        }
    }
}
