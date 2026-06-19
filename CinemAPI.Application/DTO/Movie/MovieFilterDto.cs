
namespace CinemAPI.Application.DTO.Movie
{
	public class MovieFilterDto
	{
		public string? SearchQuery { get; set; }
		public IEnumerable<int>? ActorIds { get; set; }
		public IEnumerable<int>? GenreIds { get; set; }
	}
}