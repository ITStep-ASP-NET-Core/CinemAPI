using Azure.AI.Translation.Text;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Infrastructure.Repositories
{
	public class AzureTranslatorRepository : ITranslatorRepository
	{
		private readonly TextTranslationClient _translationclient;

		public AzureTranslatorRepository ( TextTranslationClient translationclient )
		{
			_translationclient = translationclient;
		}

		public async Task<IEnumerable<string>> TranslateAsync ( IEnumerable<string> texts, string targetLang )
		{
			var response = await _translationclient.TranslateAsync(
				targetLanguage: targetLang,
				content: texts
			);

			return response.Value[0].Translations.Select(t => t.Text);
		}
	}
}
