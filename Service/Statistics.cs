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
        public List<string> PopularWords { get; set; }= new();
        public TimeSpan TotalTime { get; set; }
        public int TimesCalled { get; set; }
    }
}