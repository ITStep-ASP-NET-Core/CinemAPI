using Azure;
using Azure.Data.Tables;

namespace CinemAPI.Infrastructure.Entities
{
	public class LogEntity : ITableEntity
	{
		public string PartitionKey { get; set; } = string.Empty;
		public string RowKey { get; set; } = Guid.NewGuid().ToString();
		public ETag ETag { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public string Method { get; set; } = "Anonymous";
		public string Action { get; set; } = string.Empty;
		public string Entity { get; set; } = "None";
		public string Message { get; set; } = string.Empty;
		public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
	}
}
