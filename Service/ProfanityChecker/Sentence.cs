using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service
{
	public class InputSentence
	{
		public string Sentence { get; set; }
	}

	[StatusText("Success")]
	public class CheckResultSuccess : BaseResponse
	{
		public List<string> WordList = new();
		public TimeSpan TimeTaken = new();
	}
}