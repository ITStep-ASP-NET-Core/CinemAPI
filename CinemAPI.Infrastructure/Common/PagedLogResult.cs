namespace CinemAPI.Infrastructure.Common
{
	public class PagedLogResult<T> where T : class
	{
		public ICollection<T> Items { get; set; } = [];
		public string? ContinuationToken { get; set; }
	}
}
