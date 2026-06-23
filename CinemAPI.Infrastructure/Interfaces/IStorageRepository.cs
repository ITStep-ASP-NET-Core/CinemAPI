using CinemAPI.Domain.Common;

namespace CinemAPI.Infrastructure.Interfaces
{
	public interface IStorageRepository
	{
		Task<string> UploadAsync ( UploadFile uploadFile, string containerName = "uploads", CancellationToken ct = default );
	}
}
