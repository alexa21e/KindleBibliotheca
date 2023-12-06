using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IReadOnlyList<Book>> GetBooksAsync();
    }
}
