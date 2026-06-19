using CinemAPI.Domain.Common;
using CinemAPI.Domain.Entities;

namespace CinemAPI.Infrastructure.Interfaces
{
    public interface IMovieRepository
    {
        Task<PagedResult<Movie>> GetMoviesAsync(int page, int pageSize = 2);
		Task<PagedResult<Movie>> GetMoviesByFiltersAsync(
			string? searchQuery,
			ICollection<int>? actorIds,
			ICollection<int>? genreIds,
			int page,
			int pageSize = 2 );
		Task<Movie?> GetMovieByIdAsync ( int id );
		Task<Movie?> GetMovieByIdWithAllAsync( int id );
        Task AddMovieAsync(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
    }
}