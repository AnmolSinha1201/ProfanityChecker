using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service
{
	public class IndividualStatistic
	{
		public int Frequency = 0;
	}

	public class ProfaneWord
	{
		public string Word;
		public int Frequency;
	}
}