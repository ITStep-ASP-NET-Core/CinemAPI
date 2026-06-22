using CinemAPI.Application.DTO.Genre;
using CinemAPI.Domain.Entities;

namespace CinemAPI.Application.Mappers
{
	public class GenreMapper
	{
		public static GenreDto ToDto ( Genre genre ) => new()
		{
			Id = genre.Id,
			Name = genre.Name,
			Description = genre.Description,
		};

		public static Genre ToEntity ( GenreDto genreDto ) => new()
		{
			Name = genreDto.Name,
			Description = genreDto.Description ?? "None",
		};
	}
}
