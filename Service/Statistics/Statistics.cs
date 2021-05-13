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
		public List<ProfaneWord> PopularWords = new();
		public TimeSpan TotalTime;
		public int TimesCalled;
	}
}