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


		public UnitOfWork
        (
            ApplicationContext context,
			IGenericRepository<Actor> actors,
			IGenericRepository<Genre> genres,
			IMovieRepository movies
		)
        {
            _context = context;
			Actors = actors;
			Genres = genres;
			Movies = movies;
		}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}