using Core.Entities;

namespace KindleBibliotheca.DTOs
{
    public class BookToReturn
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public string Author { get; set; } = string.Empty;
        public DateTime PublishingDate { get; set; }
        public decimal Rating { get; set; }
        public Genre Genre { get; set; }
        public string PublishingHouse { get; set; } = string.Empty;
        public Guid SeriesId { get; set; }
        public string Series { get; set; } = string.Empty;
        public int? SeriesPlace { get; set; }
        public int PagesNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public string PDFUrl { get; set; } = string.Empty;
    }
}
