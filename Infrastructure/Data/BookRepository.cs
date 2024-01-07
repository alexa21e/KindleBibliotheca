using System.ComponentModel;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BibliothecaContext _context;
        public BookRepository(BibliothecaContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Series)
                .FirstAsync(b => b.Id == id);
        }

        public async Task<IReadOnlyList<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Series)
                .ToListAsync();
        }

        public async Task<Series> GetSeriesByIdAsync(Guid id)
        {
            return await _context.Series.FirstAsync(s => s.Id == id);
        }

        public async Task<IReadOnlyList<Series>> GetSeriesAsync()
        {
            return await _context.Series.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            return await _context.Authors.FirstAsync(a => a.Id == id);
        }

        public async Task<IReadOnlyList<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
