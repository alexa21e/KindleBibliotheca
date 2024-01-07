using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime PublishingDate { get; set; }
        public decimal Rating { get; set; }
        public Genre Genre { get; set; }
        public string PublishingHouse { get; set; } = string.Empty;
        public Guid? SeriesId { get; set; }
        public Series? Series { get; set; }
        public int? SeriesPlace { get; set; }
        public int PagesNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
    }
}
