namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishingDate { get; set; }
        public decimal Rating { get; set; }
        public Genre Genre { get; set; }
        public string PublishingHouse { get; set; } = string.Empty;
        public Guid SeriesId { get; set; }
        public int PagesNumber { get; set; }
        private List<Format> _formats = new List<Format>();
        public IReadOnlyCollection<Format> Students => _formats;
        public string Description { get; set; } = string.Empty;
    }
}
