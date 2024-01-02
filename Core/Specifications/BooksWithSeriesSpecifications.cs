using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BooksWithSeriesSpecifications: BaseSpecification<Book>
    {
        public BooksWithSeriesSpecifications()
        {
            AddInclude(x => x.Series);
        }
        public BooksWithSeriesSpecifications(Guid id): base(x => x.Id == id)
        {
            AddInclude(x => x.Series);
        }
    }
}
