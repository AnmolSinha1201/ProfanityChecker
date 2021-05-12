using System.Text.Json.Serialization;

namespace Service
{
	public class WordInput
	{
		[JsonInclude]
		public string Word;
	}
}