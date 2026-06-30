using OpenAI.Chat;

namespace CinemAPI.Infrastructure.Interfaces
{
	public interface IChatRepository
	{
		Task<ChatCompletion> AnalyzeAsync ( string prompt, string imageUrl );
	}
}