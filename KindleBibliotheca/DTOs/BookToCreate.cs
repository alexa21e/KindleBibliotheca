using Core.Entities;

namespace KindleBibliotheca.DTOs
{
    public class BookToCreate
    {
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; }
        public DateTime PublishingDate { get; set; }
        public decimal Rating { get; set; }
        public Genre Genre { get; set; }
        public string PublishingHouse { get; set; } = string.Empty;
        public string? SeriesName { get; set; }
        public int? SeriesPlace { get; set; }
        public int PagesNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public string PDFUrl { get; set; } = string.Empty;
    }
}
