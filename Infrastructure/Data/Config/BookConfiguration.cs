using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
            builder.Property(b => b.CoverUrl).IsRequired();
            builder.Property(b => b.Formats).IsRequired();
            builder.Property(b => b.Genre).IsRequired();
            builder.Property(b => b.PagesNumber).IsRequired();
            builder.Property(b => b.PublishingHouse).IsRequired();
            builder.Property(b => b.Rating).IsRequired();
            builder.Property(b => b.Rating).HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.Series).WithMany()
                .HasForeignKey(s => s.SeriesId);
        }
    }
}
