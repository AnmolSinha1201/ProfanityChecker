using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service
{
	public class InputSentence
	{
		public string Sentence;
	}

	[StatusText("Success")]
	public class CheckResultSuccess : BaseResponse
	{
		public List<string> WordList = new();
		public TimeSpan TimeTaken = new();
	}
}