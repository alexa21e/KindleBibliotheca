using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(Guid id);
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<int> CountAsync(ISpecification<Book> spec);
        Task<IReadOnlyList<Book>> GetBooksWithSpecAsync(ISpecification<Book> spec);
        Task<Book> GetEntityWithSpec(ISpecification<Book> spec);
    }
}
