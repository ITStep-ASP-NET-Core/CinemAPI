using Azure;
using Azure.AI.Translation.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CinemAPI.Application.ServiceProviderExtensions
{
    public static class ApplicationTranslatorExtensions
	{
		public static void AddTranslatorContext ( this IServiceCollection services, string? key, string? region )
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(region))
			{
				throw new ArgumentException("Translator key and region cannot be null or empty.");
			}

			services.AddSingleton(new TextTranslationClient( new AzureKeyCredential(key), region ));
		}
	}
}	