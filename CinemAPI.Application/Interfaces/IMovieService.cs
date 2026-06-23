
using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Movie;
using CinemAPI.Domain.Common;

namespace CinemAPI.Application.Interfaces
{
    public interface IMovieService
    {
        Task<PagedResult<MovieDto>> GetMoviesAsync(int page);
        Task<PagedResult<MovieDto>> GetMoviesByFiltersAsync(MovieFilterDto filter, int page);
        Task<MovieDetailDto?> GetMovieByIdAsync(int id);
        Task<Result> UploadMoviePosterAsync ( int movieId, UploadFile uploadFile, CancellationToken ct = default );
		Task<Result> CreateMovieAsync(MovieCreateDto movieDto);
        Task<Result> EditMovieAsync(MovieEditDto movieDto);
        Task<Result> DeleteMovieAsync(int id);
    }
}