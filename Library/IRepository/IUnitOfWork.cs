using Library.Data;

namespace Library.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Author> Authors { get; }
        Task Save();
    }
}
