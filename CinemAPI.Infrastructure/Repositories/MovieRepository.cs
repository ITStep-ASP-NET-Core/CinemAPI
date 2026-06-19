using CinemAPI.Domain.Common;
using CinemAPI.Domain.Entities;
using CinemAPI.Infrastructure.Data;
using CinemAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemAPI.Infrastructure.Repositories
{
	public class MovieRepository : IMovieRepository
	{
		private readonly ApplicationContext _context;

		public MovieRepository ( ApplicationContext context )
		{
			_context = context;
		}

		public async Task<PagedResult<Movie>> GetMoviesAsync ( int page, int pageSize = 2 )
		{
			var items = await _context.Movies
				.Skip(page * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();

			return new PagedResult<Movie>
			{
				Items = items,
				PageNumber = page,
				PageSize = pageSize,
				TotalCount = await _context.Movies.CountAsync()
			};
		}

		public async Task<PagedResult<Movie>> GetMoviesByFiltersAsync (
			string? searchQuery,
			ICollection<int>? actorIds,
			ICollection<int>? genreIds,
			int page,
			int pageSize = 2 )
		{
			var query = _context.Movies.AsQueryable();

			if(!string.IsNullOrWhiteSpace(searchQuery))
				query = query.Where(m => m.Title.ToLower().Contains(searchQuery.ToLower()));

			if(actorIds != null && actorIds.Count > 0)
				query = query.Where(m => m.Actors.Any(a => actorIds.Contains(a.Id)));

			if(genreIds != null && genreIds.Count > 0)
				query = query.Where(m => m.Genres.Any(g => genreIds.Contains(g.Id)));

			var items = await query
				.Skip(page * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();

			return new PagedResult<Movie>
			{
				Items = items,
				PageNumber = page,
				PageSize = pageSize,
				TotalCount = await query.CountAsync()
			};
		}

		public async Task<Movie?> GetMovieByIdAsync ( int id )
		{
			return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task<Movie?> GetMovieByIdWithAllAsync ( int id )
		{
			return await _context.Movies
				.Include(r => r.Actors)
				.Include(r => r.Genres)
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task AddMovieAsync ( Movie movie )
		{
			await _context.Movies.AddAsync(movie);
		}

		public void UpdateMovie ( Movie movie )
		{
			_context.Movies.Update(movie);
		}

		public void DeleteMovie ( Movie movie )
		{
			_context.Movies.Remove(movie);
		}
	}
}