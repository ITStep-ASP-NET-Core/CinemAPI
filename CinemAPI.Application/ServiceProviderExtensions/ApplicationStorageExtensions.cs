using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;

namespace CinemAPI.Application.ServiceProviderExtensions
{
    public static class ApplicationStorageExtensions
	{
        public static void AddStorageContext ( this IServiceCollection services, string? connection )
		{
			services.AddSingleton(new BlobServiceClient(connection));
		}
	}
}	