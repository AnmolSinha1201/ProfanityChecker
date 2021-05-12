using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OneOf;

namespace Service
{
	public static class ProfanityChecker
	{
		public static Dictionary<string, IndividualStatistic> WordDictionary = new();
		public static string SourceFile = "ProfaneWords.txt";

		static object Locker = new();
		static TimeSpan TotalTime = new();
		static int TimesCalled = 0;

		static ProfanityChecker()
		{
			if (!File.Exists(SourceFile))
			{
				Console.WriteLine($"{SourceFile} DOES NOT EXIST");
				return;
			}

			WordDictionary = File.ReadAllLines(SourceFile)
				.Where(i => !i.Trim().Equals(""))
				.Select(i => i.ToLower())
				.ToDictionary(k => k, v => new IndividualStatistic());
		}

		public static List<string> Check(string Sentence)
		{
			var foundList = new List<string>();

			var watch = new Stopwatch();
			watch.Start();
			foreach (var word in Sentence.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				if (!WordDictionary.ContainsKey(word))
					continue;

				WordDictionary[word].Frequency++;
				foundList.Add(word);
			}
			watch.Stop();
			lock(Locker)
			{
				TotalTime.Add(watch.Elapsed);
				TimesCalled++;
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

		public static OneOf<Successful, Failure> AddWord(string Word)
		{
			if (WordDictionary.ContainsKey(Word))
				return new Failure() { Description = "Word list already has the proposed word" };
			
			try
			{
				File.AppendAllLines(SourceFile, new [] { Word });
				WordDictionary.Add(Word, new());
			}
			catch(Exception ex)
			{
				return new Failure() { Description = ex.Message };
			}

			return new Successful() { Description = "Successfully added the proposed word" };
		}
	}
}