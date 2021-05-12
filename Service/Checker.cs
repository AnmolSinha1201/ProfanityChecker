using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Service
{
	public static class ProfanityChecker
	{
		public static Dictionary<string, IndividualStatistic> WordDictionary = new();
		public static WordListStatistic OverallStat = new();
		public static string SourceFile = "ProfaneWords.txt";
		static object Locker = new object();

		static ProfanityChecker()
		{
			if (!File.Exists(SourceFile))
			{
				Console.WriteLine($"{SourceFile} DOES NOT EXIST");
				return;
			}

			WordDictionary = File.ReadAllLines(SourceFile)
				.Where(i => !i.Trim().Equals(""))
				.ToDictionary(k => k, v => new IndividualStatistic());
		}

		public static List<string> Check(string Sentence)
		{
			var foundList = new List<string>();
			OverallStat.Frequency++;

			var watch = new Stopwatch();
			watch.Start();
			foreach (var word in Sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				if (!WordDictionary.ContainsKey(word))
					continue;

				WordDictionary[word].Frequency++;
				foundList.Add(word);
			}
			watch.Stop();
			lock(Locker)
			{
				OverallStat.TotalTime.Add(watch.Elapsed);
			}

			return foundList;
		}

		public static OverallStatistic GetOverallStatistic()
		=> new OverallStatistic()
		{
			TotalTime = TotalTime,
			TimesCalled = TimesCalled,
			PopularWords = WordDictionary
				.Where(kvp => kvp.Value.Frequency != 0)
				.OrderByDescending(kvp => kvp.Value.Frequency)
				.Take(10)
				.Select(kvp => kvp.Key)
				.ToList()
		};
	}
}