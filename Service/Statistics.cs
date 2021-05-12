using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Service
{
    public class IndividualStatistic
    {
        public int Frequency = 0;
    }

    public class OverallStatistic
    {
        public List<string> PopularWords = new();
        public DateTime TotalTime;
        public int TimesCalled;
    }
}