using CinemAPI.Application.DTO.Actor;
using CinemAPI.Application.DTO.Genre;
using CinemAPI.Application.Implementations;
using CinemAPI.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CinemAPI.Application.ServiceProviderExtensions
{
    public static class ApplicationServicesExtensions
	{
        public static void AddApplicationServices ( this IServiceCollection services )
		{
			services.AddScoped<IService<ActorDto>, ActorService>();
			services.AddScoped<IService<GenreDto>, GenreService>();
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<ILogService, LogService>();
		}
	}
}