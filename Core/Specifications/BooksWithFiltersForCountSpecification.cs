using Core.Entities;

namespace Core.Specifications
{
    public class BooksWithFiltersForCountSpecification : BaseSpecification<Book>
    {
        public BooksWithFiltersForCountSpecification(BookSpecParam bookParams)
            : base(x =>
                (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.SeriesId.HasValue || x.SeriesId == bookParams.SeriesId) &&
                (!bookParams.AuthorId.HasValue || x.AuthorId == bookParams.AuthorId))
        {

        }
    }
}
