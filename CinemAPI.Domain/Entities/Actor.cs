
namespace CinemAPI.Domain.Entities
{
	public class Actor
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Biography { get; set; } = string.Empty;
		public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
		public IEnumerable<Movie> Movies { get; set; } = [];
	}
}
