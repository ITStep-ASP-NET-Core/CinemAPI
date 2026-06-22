using CinemAPI.Domain.Common;

namespace CinemAPI.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync( int page, int pageSize = 2 );
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}