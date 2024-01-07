using Core.Entities;

namespace Core.Specifications
{
    public class BookWithFiltersForCountSpecification : BaseSpecification<Book>
    {
        public BookWithFiltersForCountSpecification(BookSpecParam bookParams)
            : base(x =>
                (!bookParams.SeriesId.HasValue || x.SeriesId == bookParams.SeriesId))
        {

        }
    }
}
