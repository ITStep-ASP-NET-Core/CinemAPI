using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Movie;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Entities;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IStorageRepository _storageRepository;
		private readonly IUnitOfWork _uow;
		private readonly ILogRepository _logs;

		public MovieService (
			IMovieRepository movieRepository,
			IStorageRepository storageRepository,
			IUnitOfWork uow,
			ILogRepository logs )
		{
			_movieRepository = movieRepository;
			_storageRepository = storageRepository;
			_uow = uow;
			_logs = logs;
		}

		public async Task<PagedResult<MovieDto>> GetMoviesAsync ( int page )
		{
			var source = await _movieRepository.GetMoviesWithAllAsync(Math.Max(0, page - 1));
			var movies = source.Items.Select(MovieMapper.ToDto).ToList();

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "movie",
				Method = "view",
				Action = "GetMoviesAsync",
				Entity = "movie",
				Message = $"Movies page {page} viewed"
			});

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

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "movie",
				Method = "view",
				Action = "GetMoviesByFiltersAsync",
				Entity = "movie",
				Message = $"Movies page {page} viewed by {(filter.SearchQuery != null ? filter.SearchQuery : "_")}{(filter.ActorIds?.Any() == true ? $"; Actors: {string.Join(", ", filter.ActorIds)}" : "")}{(filter.GenreIds?.Any() == true ? $"; Genres: {string.Join(", ", filter.GenreIds)}" : "")}"
			});

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

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "movie",
				Method = "view",
				Action = "GetMovieByIdAsync",
				Entity = "movie",
				Message = $"Movie {movieId} viewed"
			});

			return MovieMapper.ToDetailDto(movie);
		}

		public async Task<Result> UploadMoviePosterAsync ( int movieId, UploadFile uploadFile, CancellationToken ct = default )
		{
			var log = new LogEntity
			{
				PartitionKey = "movie",
				Method = "upload",
				Action = "UploadMoviePosterAsync",
				Entity = "movie",
			};

			try
			{
				var movie = await _movieRepository.GetMovieByIdAsync(movieId);
				if(movie == null)
					return Result.Fail("Movie does not exist");

				uploadFile.FileName = $"{Guid.NewGuid()}/poster";

				var posterUrl = await _storageRepository.UploadAsync(uploadFile, "movie-gallery", ct);

				movie.PosterUrl = posterUrl;
				await _uow.SaveChangesAsync();

				log.Message = $"Poster uploaded for movie {movieId}";
			}
			catch(Exception ex)
			{
				log.Message = $"Error uploading poster for movie {movieId}: {ex.Message}";
				return Result.Fail("Failed to upload movie poster");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> CreateMovieAsync ( MovieCreateDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "movie",
				Method = "create",
				Action = "CreateMovieAsync",
				Entity = "movie",
			};

			try
			{
				await _movieRepository.AddMovieAsync(
					MovieMapper.ToEntity(dto),
					dto.ActorIds,
					dto.GenreIds);

				await _uow.SaveChangesAsync();

				log.Message = $"Movie '{dto.Title}' created";
			}
			catch(Exception ex)
			{
				log.Message = $"Error creating movie '{dto.Title}': {ex.Message}";
				return Result.Fail("The Movie failed to create");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> EditMovieAsync ( MovieEditDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "movie",
				Method = "edit",
				Action = "EditMovieAsync",
				Entity = "movie",
			};

			try
			{
				var movie = await _movieRepository.GetMovieByIdWithAllAsync(dto.Id);
				if(movie == null) 
					return Result.Fail("Movie does not exist");

				movie.Title = dto.Title ?? movie.Title;
				movie.Description = dto.Description ?? movie.Description;
				movie.ReleaseYear = dto.ReleaseYear ?? movie.ReleaseYear;

				await _movieRepository.UpdateMovie(movie, dto.ActorIds, dto.GenreIds);
				await _uow.SaveChangesAsync();

				log.Message = $"Movie {dto.Id} edited";
			}
			catch(Exception ex)
			{
				log.Message = $"Error editing movie {dto.Id}: {ex.Message}";
				return Result.Fail("The Movie failed to edit");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> DeleteMovieAsync ( int movieId )
		{
			var log = new LogEntity
			{
				PartitionKey = "movie",
				Method = "delete",
				Action = "DeleteMovieAsync",
				Entity = "movie",
			};

			try
			{
				var movie = await _movieRepository.GetMovieByIdWithAllAsync(movieId);
				if(movie == null)
					return Result.Fail("Movie does not exist");

				_movieRepository.DeleteMovie(movie);
				await _uow.SaveChangesAsync();

				log.Message = $"Movie {movieId} deleted";
			}
			catch(Exception ex)
			{
				log.Message = $"Error deleting movie {movieId}: {ex.Message}";
				return Result.Fail("The Movie failed to delete");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}
	}
}