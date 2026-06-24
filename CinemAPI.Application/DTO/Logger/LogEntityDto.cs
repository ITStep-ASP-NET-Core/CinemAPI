
namespace CinemAPI.Application.DTO.Logger
{
	public class LogEntityDto
	{
		public string Method { get; set; } = "Anonymous";
		public string Action { get; set; } = "None";
		public string Entity { get; set; } = "None";
		public string Message { get; set; } = "None";
		public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
	}
}
