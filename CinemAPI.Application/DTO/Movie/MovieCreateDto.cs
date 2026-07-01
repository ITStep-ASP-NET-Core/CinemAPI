
namespace CinemAPI.Application.DTO.Movie
{
	public class MovieCreateDto
	{
		public string Title { get; set; } = null!;
		public IEnumerable<int> ActorIds { get; set; } = [];
		public IEnumerable<int> GenreIds { get; set; } = [];
		public DateOnly ReleaseYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);

	}
}
