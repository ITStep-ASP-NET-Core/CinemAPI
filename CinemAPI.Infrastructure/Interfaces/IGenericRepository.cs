using CinemAPI.Domain.Common;

namespace CinemAPI.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}