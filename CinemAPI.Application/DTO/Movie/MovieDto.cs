
namespace CinemAPI.Application.DTO.Movie
{
    public class MovieDto {
		public int Id { get; set; }
		public string Title { get; set; } = "Untitled";
		public string? PosterUrl { get; set; }
		public IEnumerable<int> ActorIds { get; set; } = [];
		public IEnumerable<int> GenreIds { get; set; } = [];
		public DateOnly ReleaseYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	}
}