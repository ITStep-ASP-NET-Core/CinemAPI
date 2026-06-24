using CinemAPI.Application.DTO.Logger;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Infrastructure.Common;
using CinemAPI.Infrastructure.Entities;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
    public class LogService : ILogService
    {
		private readonly ILogRepository _logs;

		public LogService ( ILogRepository logs )
		{
			_logs = logs;
		}

		public async Task<PagedLogResult<LogEntityDto>> GetAllAsync ( LogFilterDto? filter, string? continuationToken = null, CancellationToken ct = default )
		{
			var source = await _logs.GetAsync(filter?.Entity, filter?.Method, continuationToken, ct);
			var logs = source.Items.Select(LogMapper.ToDto).ToList();

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "log",
				Method = "view",
				Action = "GetAllAsync",
				Entity = "log",
				Message = $"Logs viewed by {(filter?.Entity != null ? filter?.Entity : "_")}{(filter?.Method != null ? $", {filter?.Method}" : ", _")}"
			});

			return new PagedLogResult<LogEntityDto>
			{
				Items = logs,
				ContinuationToken = source.ContinuationToken,
			};
		}
	}
}