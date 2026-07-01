using OpenAI.Chat;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Infrastructure.Repositories
{
	public class ChatRepository : IChatRepository
	{
		private readonly ChatClient _chatClient;

		public ChatRepository ( ChatClient chatClient )
		{
			_chatClient = chatClient;
		}

		public async Task<ChatCompletion> AnalyzeAsync ( string prompt, string imageUrl )
		{
			var result = await _chatClient.CompleteChatAsync
			([
					new UserChatMessage(
						ChatMessageContentPart.CreateImagePart( new Uri(imageUrl) ),
						ChatMessageContentPart.CreateTextPart( prompt )
					)
			]);

			return result.Value;

		}
	}
}
