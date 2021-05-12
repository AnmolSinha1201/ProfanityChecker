using System;
using System.Collections.Generic;

namespace Service
{
    public class IndividualStatistic
    {
        public int Frequency = 0;
    }

    public class WordListStatistic
    {
        public DateTime TotalTime;
        public int Frequency;
    }

    public class OverallStatistic
    {
        public List<string> PopularWords = new();
        public DateTime TotalTime;
        public int TimesCalled;
    }
}