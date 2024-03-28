using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(Guid id);
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<int> CountAsync(ISpecification<Book> spec);
        Task<IReadOnlyList<Book>> GetBooksWithSpecAsync(ISpecification<Book> spec);
        Task<Book> GetEntityWithSpec(ISpecification<Book> spec);
        void Add(Book book);
        void Update(Book book);
        Task SaveAsync();
    }
}
