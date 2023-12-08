﻿using Core.Entities;
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
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IReadOnlyList<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
    }
}