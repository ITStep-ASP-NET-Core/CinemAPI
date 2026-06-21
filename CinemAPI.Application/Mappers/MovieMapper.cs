using CinemAPI.Application.DTO.Movie;
using CinemAPI.Domain.Entities;

namespace CinemAPI.Application.Mappers
{
	public class MovieMapper
	{
		public static MovieDto ToDto ( Movie movie ) => new()
		{
			Id = movie.Id,
			Title = movie.Title,
			ReleaseYear = movie.ReleaseYear,
			ActorIds = movie.Actors.Select( a => a.Id ).ToList(),
			GenreIds = movie.Genres.Select( a => a.Id ).ToList(),
		};

		public static MovieDetailDto ToDetailDto ( Movie movie ) => new()
		{
			Id = movie.Id,
			Title = movie.Title,
			Description = movie.Description,
			ReleaseYear = movie.ReleaseYear,
			Actors = movie.Actors.Select(ActorMapper.ToDto).ToList(),
			Genres = movie.Genres.Select(GenreMapper.ToDto).ToList(),
		};

		public static Movie ToEntity ( MovieCreateDto movieDto ) => new()
		{
			Title = movieDto.Title,
			Description = movieDto.Description,
			ReleaseYear = movieDto.ReleaseYear,
		};
	}
}
