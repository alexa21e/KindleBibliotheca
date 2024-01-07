using Core.Entities;

namespace Core.Specifications
{
    public class BookWithFiltersForCountSpecification : BaseSpecification<Book>
    {
        public BookWithFiltersForCountSpecification(BookSpecParam bookParams)
            : base(x =>
                (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.SeriesId.HasValue || x.SeriesId == bookParams.SeriesId))
        {

        }
    }
}
