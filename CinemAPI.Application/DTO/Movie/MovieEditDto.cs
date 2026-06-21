
namespace CinemAPI.Application.DTO.Movie
{
	public class MovieEditDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public IEnumerable<int> ActorIds { get; set; } = [];
		public IEnumerable<int> GenreIds { get; set; } = [];
		public DateOnly? ReleaseYear { get; set; }

	}
}
