using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<IReadOnlyList<Author>> GetAuthorsAsync();
        Task<IReadOnlyList<Author>> GetAuthorsWithSpecAsync(ISpecification<Author> spec);
    }
}
