
namespace CinemAPI.Infrastructure.Interfaces
{
	public interface ITranslatorRepository
	{
		Task<IEnumerable<string>> TranslateAsync ( IEnumerable<string> texts, string targetLang );
	}
}
