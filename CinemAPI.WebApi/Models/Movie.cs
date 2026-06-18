namespace CinemAPI.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public IEnumerable<Genre> Genres { get; set; } = [];
		public DateOnly ReleaseYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	}
}
