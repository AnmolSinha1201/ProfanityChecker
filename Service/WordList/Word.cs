using System.Text.Json.Serialization;

namespace Service
{
	public class InputWord
	{
		[JsonInclude]
		public string Word;
	}
}