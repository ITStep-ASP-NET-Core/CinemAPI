using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Logger;
using CinemAPI.Infrastructure.Common;

namespace CinemAPI.Application.Interfaces
{
    public interface ILogService
	{
		Task<PagedLogResult<LogEntityDto>> GetAllAsync ( LogFilterDto? filter, string? continuationToken = null, CancellationToken ct = default );
	}
}