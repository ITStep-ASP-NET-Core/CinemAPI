using CinemAPI.Domain.Entities;
using CinemAPI.Infrastructure.Data;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

		public IGenericRepository<Actor> Actors { get; }
		public IGenericRepository<Genre> Genres { get; }
		public IMovieRepository Movies { get; }
		public IStorageRepository Storage { get; }


		public UnitOfWork
        (
            ApplicationContext context,
			IGenericRepository<Actor> actors,
			IGenericRepository<Genre> genres,
			IMovieRepository movies,
			IStorageRepository storage
		)
        {
            _context = context;
			Actors = actors;
			Genres = genres;
			Movies = movies;
			Storage = storage;
		}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}