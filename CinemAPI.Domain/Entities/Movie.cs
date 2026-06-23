
namespace CinemAPI.Domain.Entities
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string? PosterUrl { get; set; }
		public ICollection<Actor> Actors { get; set; } = [];
		public ICollection<Genre> Genres { get; set; } = [];
		public DateOnly ReleaseYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	}
}
