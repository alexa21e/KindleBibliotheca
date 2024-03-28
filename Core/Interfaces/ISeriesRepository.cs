using Core.Entities;

namespace Core.Interfaces
{
    public interface ISeriesRepository
    {
        Task<Series> GetSeriesByIdAsync(Guid id);
        Task<IReadOnlyList<Series>> GetSeriesAsync();
    }
}
