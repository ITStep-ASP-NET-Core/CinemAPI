
namespace CinemAPI.Application.DTO.Actor
{
	public class ActorDto
	{
		public int? Id { get; set; }
		public string Name { get; set; } = "Noname";
		public string? Biography { get; set; }
		public DateOnly? BirthDate { get; set; }
	}
}