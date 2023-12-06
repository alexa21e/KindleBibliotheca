using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BibliothecaContext : DbContext
    {
        public BibliothecaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Series> Series { get; set; }
    }
}
