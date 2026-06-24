using CinemAPI.Infrastructure.Common;
using CinemAPI.Infrastructure.Entities;

namespace CinemAPI.Infrastructure.Interfaces
{
	public interface ILogRepository
	{
		Task LogAsync ( LogEntity log, CancellationToken ct = default );
		Task<PagedLogResult<LogEntity>> GetAsync ( string? entity = null, string? method = null, string? continuationToken = null, CancellationToken ct = default, int pageSize = 10 );
	}
}