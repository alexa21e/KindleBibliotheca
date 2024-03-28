using System.ComponentModel;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
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
        public async Task<int> CountAsync(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        public async Task<IReadOnlyList<Book>> GetBooksWithSpecAsync(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<Book> GetEntityWithSpec(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public void Add(Book book)
        {
            _context.Set<Book>().Add(book);
        }
        public void Update(Book book)
        {
            _context.Set<Book>().Attach(book);
            _context.Entry(book).State = EntityState.Modified;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        private IQueryable<Book> ApplySpecification(ISpecification<Book> spec)
        {
            return SpecificationEvaluator<Book>.GetQuery(_context.Set<Book>().AsQueryable(), spec);
        }
    }
}
