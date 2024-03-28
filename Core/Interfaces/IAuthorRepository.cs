using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<IReadOnlyList<Author>> GetAuthorsAsync();
        Task<IReadOnlyList<Author>> GetAuthorsWithSpecAsync(ISpecification<Author> spec);
        Task<Author> GetAuthorByNameAsync(string authorName);
        void Add(Author author);
        void Update(Author author);
        Task SaveAsync();
    }
}
