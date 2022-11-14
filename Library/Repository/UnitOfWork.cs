using Library.Data;
using Library.IRepository;

namespace Library.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private IGenericRepository<Book> _books;
        private IGenericRepository<Author> _authors;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Book> Books => _books ??= new GenericRepository<Book>(_context);

        public IGenericRepository<Author> Authors => _authors ??= new GenericRepository<Author>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
