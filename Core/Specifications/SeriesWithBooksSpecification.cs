using Core.Entities;

namespace Core.Specifications
{
    public class SeriesWithBooksSpecification: BaseSpecification<Series>
    {
        public SeriesWithBooksSpecification()
        {
            AddInclude(s => s.Books);
        }
    }
}
