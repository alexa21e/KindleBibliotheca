using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(Guid id);
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<Series> GetSeriesByIdAsync(Guid id);
        Task<IReadOnlyList<Series>> GetSeriesAsync();
    }
}
