using CM.Helpers;

namespace CM.Benchmark.Model
{
    public class SingleSeasonBenchmarkResult
    {
        public int Scored { get; set; }
        public int Conceded { get; set; }
        public int Points { get; set; }
        public int Place { get; set; }
        public int Matches { get; set; }

        public SingleSeasonBenchmarkResult() { }

        public override string ToString() => Helper.Serialize(this);
    }
}
