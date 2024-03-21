using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        Task SaveAsync();
    }
}
