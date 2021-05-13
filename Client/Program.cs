using System;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var examples = new Examples();
			examples.FileExample();
			examples.SentenceExample();
			examples.StatisticsExample();
			examples.AddAndRemoveExample();
		}
	}
}
