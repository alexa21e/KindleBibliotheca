using Core.Entities;
using Core.Specifications;
using KindleBibliotheca.DTOs;

namespace Core.Interfaces
{
    public interface IBooksService
    {
        Task<IReadOnlyList<Book>> GetBooks(BookSpecParam bookParams);
        Task<int> GetBooksCount(BookSpecParam bookParams);
        Task<BookToReturn> GetBook(Guid id);
        Task<Book> CreateBook(BookToCreate bookToCreate);
    }
}
