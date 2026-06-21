
namespace CinemAPI.Application.DTO.Movie
{
	public class MovieFilterDto
	{
		public string? SearchQuery { get; set; }
		public ICollection<int>? ActorIds { get; set; }
		public ICollection<int>? GenreIds { get; set; }
	}
}