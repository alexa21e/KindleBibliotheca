using Core.Entities;

namespace Core.Specifications
{
    public class BooksWithSeriesSpecifications: BaseSpecification<Book>
    {
        public BooksWithSeriesSpecifications()
        {
            AddInclude(x => x.Series);
        }
    }
}
