using System.Collections.Generic;
using Core.Entities;

namespace Infrastructure.Data
{
    public class BibliothecaContext : DbContext
    {
        public BibliothecaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
