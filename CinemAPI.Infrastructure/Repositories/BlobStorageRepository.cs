using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Infrastructure.Repositories
{
	public class BlobStorageRepository : IStorageRepository
	{
		protected readonly BlobServiceClient _blobServiceClient;

		public BlobStorageRepository ( BlobServiceClient blobServiceClient )
		{
			_blobServiceClient = blobServiceClient;
		}

		public async Task<string> UploadAsync ( UploadFile uploadFile, string containerName = "uploads", CancellationToken ct = default )
		{
			var blobPath = $"{uploadFile.FileName}{uploadFile.FileExtension}";
			var container = _blobServiceClient.GetBlobContainerClient(containerName);

			await container.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

			var blobClient = container.GetBlobClient(blobPath);
			await blobClient.UploadAsync(uploadFile.Content, new BlobHttpHeaders { ContentType = uploadFile.ContentType }, cancellationToken: ct);

			return blobClient.Uri.ToString();
		}
	}
}
