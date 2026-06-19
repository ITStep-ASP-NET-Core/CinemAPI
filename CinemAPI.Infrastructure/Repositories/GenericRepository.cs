using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Data;
using CinemAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemAPI.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

		public async Task<PagedResult<T>> GetAllAsync ( int page, int pageSize = 2 )
		{
			var items = await _dbSet
				.Skip(page * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();

			return new PagedResult<T>
			{
				Items = items,
				PageNumber = page,
				PageSize = pageSize,
				TotalCount = await _dbSet.CountAsync()
			};
		}

		public async Task<T?> GetByIdAsync ( int id )
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task AddAsync ( T obj )
		{
			await _dbSet.AddAsync(obj);
		}

		public void Update ( T obj )
		{
			_dbSet.Update(obj);
		}

		public void Delete ( T obj )
		{
			_dbSet.Remove(obj);
		}
	}
}