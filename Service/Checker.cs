using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OneOf;
using Npgsql;
using Dapper;

namespace Service
{
	public static class ProfanityChecker
	{
		static object Locker = new();
		static TimeSpan TotalTime = new();
		static int TimesCalled = 0;
		//TODO : Fix cases when conneciton string is not present
		static string ConnectionString = Environment.GetEnvironmentVariable("PostgreSQLConnectionString");

		public static OneOf<CheckResultSuccess, Failure> Check(string Sentence)
		{
			var foundList = new List<string>();

			var preparedWords = Sentence.ToLower()
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(i => $"'{i.Prepare()}'")
				.Aggregate((prev, current) => $"{prev}, {current}");
			
			var watch = new Stopwatch();
			watch.Start();
			using (var connection = new NpgsqlConnection(ConnectionString))
			{
				try
				{
					var query = $"update profanities Set frequency = frequency + 1 where word in ({preparedWords}) returning *";
					var contains = connection.Query<ProfaneWord>(query);

					foundList = contains.Select(i => i.Word).ToList();
				}
				catch (Exception ex)
				{
					return new Failure() { Description = ex.Message };
				}
				
			}
			watch.Stop();

			lock(Locker)
			{
				TotalTime.Add(watch.Elapsed);
				TimesCalled++;
			}
			return new CheckResultSuccess() { WordList = foundList, TimeTaken = watch.Elapsed };
		}

		public static OneOf<OverallStatisticSuccess, Failure> GetOverallStatistic()
		{
			using (var connection = new NpgsqlConnection(ConnectionString))
			{
				try
				{
					var query = $"select * from Profanities where frequency != 0 order by frequency desc limit 10 ";
					var words = connection.Query<ProfaneWord>(query);

					return new OverallStatisticSuccess()
					{
						TotalTime = TotalTime,
						TimesCalled = TimesCalled,
						PopularWords = words.ToList()
					};
				}
				catch (Exception ex)
				{
					return new Failure() { Description = ex.Message };
				}
				
			}
		}

		public static OneOf<Success, Failure> AddWord(string Word)
		{
			using (var connection = new NpgsqlConnection(ConnectionString))
			{
				try
				{
					var contains = connection.Query<ProfaneWord>($"select * from Profanities where Word =  '{Word.Prepare()}'");
					if (contains.Count() > 0)
						return new Failure() { Description = "Word list already has the proposed word" };

					connection.Execute($"insert into Profanities(Word) values ('{Word.Prepare()}')");
				}
				catch (Exception ex)
				{
					return new Failure() { Description = ex.Message };
				}

				return new Success() { Description = "Successfully added the proposed word" };
			}
		}

		public static OneOf<Success, Failure> RemoveWord(string Word)
		{
			using (var connection = new NpgsqlConnection(ConnectionString))
			{
				try
				{
					connection.Execute($"delete from Profanities where Word = '{Word.Prepare()}'");
				}
				catch (Exception ex)
				{
					return new Failure() { Description = ex.Message };
				}

				return new Success() { Description = "Successfully deleted the proposed word" };
			}
		}
	}
}