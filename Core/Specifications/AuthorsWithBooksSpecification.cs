using Core.Entities;

namespace Core.Specifications
{
    public class AuthorsWithBooksSpecification: BaseSpecification<Author>
    {
        public AuthorsWithBooksSpecification()
        {
            AddInclude(a => a.Books);
        }
    }
}
