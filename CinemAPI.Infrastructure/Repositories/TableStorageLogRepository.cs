using Azure.Data.Tables;
using CinemAPI.Infrastructure.Common;
using CinemAPI.Infrastructure.Entities;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Infrastructure.Repositories
{
	public class TableStorageLogRepository : ILogRepository
	{
		protected readonly TableClient _tableClient;

		public TableStorageLogRepository ( TableServiceClient tableClient )
		{
			_tableClient = tableClient.GetTableClient("CinemAPIlogs");
			_tableClient.CreateIfNotExists();
		}

		public async Task LogAsync ( LogEntity entity, CancellationToken ct = default )
		{
			await _tableClient.AddEntityAsync(entity, ct);
		}

		public async Task<PagedLogResult<LogEntity>> GetAsync ( string? entity = null, string? method = null, string? continuationToken = null, CancellationToken ct = default, int pageSize = 10 )
		{
			var filters = new List<string>();

			if(!string.IsNullOrEmpty(entity))
				filters.Add($"PartitionKey eq '{entity}'");

			if(!string.IsNullOrEmpty(method))
				filters.Add($"Method eq '{method}'");

			var pageable = _tableClient.QueryAsync<LogEntity>(
				filter: string.Join(" and ", filters),
				maxPerPage: pageSize,
				cancellationToken: ct);

			await using var enumerator = pageable.AsPages(continuationToken).GetAsyncEnumerator(ct);

			if(!await enumerator.MoveNextAsync())
				return new PagedLogResult<LogEntity>();

			var page = enumerator.Current;

			return new PagedLogResult<LogEntity>
			{
				Items = page.Values.ToList(),
				ContinuationToken = page.ContinuationToken
			};
		}
	}
}
