
namespace CinemAPI.Domain.Common
{
	public class UploadFile
	{
		public Stream Content { get; set; } = null!;
		public string ContentType { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public string FileExtension { get; set; } = null!;
	}
}