using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.DependencyInjection;

namespace CinemAPI.Application.ServiceProviderExtensions
{
    public static class ApplicationChatExtensions
	{
		public static void AddChatContext ( this IServiceCollection services, string? endpoint, string? key, string? model )
		{
			if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(model))
			{
				throw new ArgumentException("Chat endpoint, key and model cannot be null or empty.");
			}
			var azureOpenAIClient = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
			var chatClient = azureOpenAIClient.GetChatClient(model);
			services.AddSingleton(chatClient);
		}
	}
}	