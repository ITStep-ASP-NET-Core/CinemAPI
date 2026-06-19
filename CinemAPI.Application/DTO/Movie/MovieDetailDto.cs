using CinemAPI.Application.DTO.Actor;
using CinemAPI.Application.DTO.Genre;

namespace CinemAPI.Application.DTO.Movie
{
	public class MovieDetailDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = "Untitled";
		public string? Description { get; set; }
		public IEnumerable<ActorDto> ActorIds { get; set; } = [];
		public IEnumerable<GenreDto> GenreIds { get; set; } = [];
		public DateOnly ReleaseYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	}
}