namespace Core.Specifications
{
    public class BookSpecParam
    {
        private const int MaxPageSize = 30;
        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        private int _pageSize = 6;
        public Guid? SeriesId { get; set; }
        public string? Sort { get; set; }
    }
}
