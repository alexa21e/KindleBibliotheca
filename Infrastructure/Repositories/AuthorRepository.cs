﻿using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;

namespace Infrastructure.Repositories
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly BibliothecaContext _context;
        public AuthorRepository(BibliothecaContext context)
        {
            _context = context;
        }
        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            return await _context.Authors.FirstAsync(a => a.Id == id);
        }
        public async Task<IReadOnlyList<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<IReadOnlyList<Author>> GetAuthorsWithSpecAsync(ISpecification<Author> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<Author> ApplySpecification(ISpecification<Author> spec)
        {
            return SpecificationEvaluator<Author>.GetQuery(_context.Set<Author>().AsQueryable(), spec);
        }
    }
}
