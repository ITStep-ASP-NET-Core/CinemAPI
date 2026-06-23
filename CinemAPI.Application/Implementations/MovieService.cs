using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Movie;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IStorageRepository _storageRepository;
		private readonly IUnitOfWork _uow;

		public MovieService ( IMovieRepository movieRepository, IStorageRepository storageRepository, IUnitOfWork uow )
		{
			_movieRepository = movieRepository;
			_storageRepository = storageRepository;
			_uow = uow;
		}

		public async Task<PagedResult<MovieDto>> GetMoviesAsync ( int page )
		{
			var source = await _movieRepository.GetMoviesWithAllAsync(Math.Max(0, page - 1));
			var movies = source.Items.Select(MovieMapper.ToDto).ToList();

			return new PagedResult<MovieDto>
			{
				Items = movies,
				PageNumber = page,
				PageSize = source.PageSize,
				TotalCount = source.TotalCount,
			};
		}

		public async Task<PagedResult<MovieDto>> GetMoviesByFiltersAsync ( MovieFilterDto filter, int page )
		{
			var source = await _movieRepository.GetMoviesByFiltersAsync(
				filter.SearchQuery,
				filter.ActorIds,
				filter.GenreIds,
				Math.Max(0, page - 1));
			var movies = source.Items.Select(MovieMapper.ToDto).ToList();

			return new PagedResult<MovieDto>
			{
				Items = movies,
				PageNumber = page,
				PageSize = source.PageSize,
				TotalCount = source.TotalCount,
			};
		}

		public async Task<MovieDetailDto?> GetMovieByIdAsync ( int movieId )
		{
			var movie = await _movieRepository.GetMovieByIdWithAllAsync(movieId);
			if(movie == null) return null;
			return MovieMapper.ToDetailDto(movie);
		}

		public async Task<Result> UploadMoviePosterAsync ( int movieId, UploadFile uploadFile, CancellationToken ct = default )
		{
			try
			{
				var movie = await _movieRepository.GetMovieByIdAsync(movieId);
				if(movie == null) return Result.Fail("Movie does not exist");

				uploadFile.FileName = $"{Guid.NewGuid()}/poster";

				var posterUrl = await _storageRepository.UploadAsync(uploadFile, "movie-gallery", ct);
				
				movie.PosterUrl = posterUrl;

				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("Failed to upload movie poster");
			}
			return Result.Ok();
		}

		public async Task<Result> CreateMovieAsync ( MovieCreateDto dto )
		{
			try {
				await _movieRepository.AddMovieAsync(MovieMapper.ToEntity(dto), dto.ActorIds, dto.GenreIds);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Movie failed to create");
			}
			return Result.Ok();
		}

		public async Task<Result> EditMovieAsync ( MovieEditDto dto )
		{
			try {
				var movie = await _movieRepository.GetMovieByIdWithAllAsync(dto.Id);
				if(movie == null) return Result.Fail("Movie does not exist");

				movie.Title = dto.Title ?? movie.Title;
				movie.Description = dto.Description ?? movie.Description;
				movie.ReleaseYear = dto.ReleaseYear ?? movie.ReleaseYear;

				await _movieRepository.UpdateMovie(movie, dto.ActorIds, dto.GenreIds);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Movie failed to edit");
			}
			return Result.Ok();
		}

		public async Task<Result> DeleteMovieAsync ( int movieId )
		{
			try {
				var movie = await _movieRepository.GetMovieByIdWithAllAsync(movieId);
				if(movie == null) return Result.Fail("Movie does not exist");

				_movieRepository.DeleteMovie(movie);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Movie failed to delete");
			}
			return Result.Ok();
		}

	}
}