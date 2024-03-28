using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class SeriesRepository: ISeriesRepository
    {
        private readonly BibliothecaContext _context;
        public SeriesRepository(BibliothecaContext context)
        {
            _context = context;
        }
        public async Task<Series> GetSeriesByIdAsync(Guid id)
        {
            return await _context.Series.FirstAsync(s => s.Id == id);
        }

        public async Task<IReadOnlyList<Series>> GetSeriesAsync()
        {
            return await _context.Series.ToListAsync();
        }
    }
}
