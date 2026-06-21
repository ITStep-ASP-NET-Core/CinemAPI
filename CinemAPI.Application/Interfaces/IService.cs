using CinemAPI.Application.Common;
using CinemAPI.Domain.Common;

namespace CinemAPI.Application.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<PagedResult<T>> GetAllAsync(int page);
        Task<T?> GetAsync(int id);
        Task<Result> AddAsync(T obj);
        Task<Result> EditAsync(T obj);
        Task<Result> DeleteAsync(int id);
    }
}