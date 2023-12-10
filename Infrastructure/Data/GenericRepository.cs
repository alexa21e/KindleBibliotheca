using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly BibliothecaContext _context;
        public GenericRepository(BibliothecaContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstAsync(b => b.Id == id);
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
