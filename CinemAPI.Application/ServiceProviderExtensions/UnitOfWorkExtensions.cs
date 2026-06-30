using CinemAPI.Infrastructure.Interfaces;
using CinemAPI.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CinemAPI.Application.ServiceProviderExtensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IStorageRepository, BlobStorageRepository>();
            services.AddScoped<ILogRepository, TableStorageLogRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
    }
}