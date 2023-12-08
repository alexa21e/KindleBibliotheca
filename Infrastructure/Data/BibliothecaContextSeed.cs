using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class BibliothecaContextSeed
    {
        public static async Task SeedAsync(BibliothecaContext context)
        {
            if (!context.Books.Any())
            {
                var booksData = File.ReadAllText("../Infrastructure/Data/SeedData/books.json");
                var books = JsonSerializer.Deserialize<List<Book>>(booksData);
                context.Books.AddRange(books);
            }
            if (!context.Series.Any())
            {
                var seriesData = File.ReadAllText("../Infrastructure/Data/SeedData/series.json");
                var series = JsonSerializer.Deserialize<List<Series>>(seriesData);
                context.Series.AddRange(series);
            }

            if(context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
    }
}
