using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service
{
	public class ProfaneWord
	{
		public string Word;
		public int Frequency;
	}

	[StatusText("Success")]
	public class OverallStatisticSuccess : BaseResponse
	{
		public List<ProfaneWord> PopularWords { get; set; } = new();
		public TimeSpan TotalTime { get; set; }
		public int TimesCalled { get; set; }
	}
}