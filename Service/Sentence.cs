using System.Text.Json.Serialization;

namespace Service
{
	public class ServiceInput
	{
		[JsonInclude]
		public string Sentence;
	}
}