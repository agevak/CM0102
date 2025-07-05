using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.Benchmark.Model
{
    public class BunchBenchmarkResult
    {
        public IList<SingleSeasonBenchmarkResult> Seasons { get; set; } = new List<SingleSeasonBenchmarkResult>();
        public double AvgScored { get => Seasons.Average(x => x.Scored) / MatchesPerSeason; }
        public double AvgConceded { get => Seasons.Average(x => x.Conceded) / MatchesPerSeason; }
        public double AvgPoints { get => Seasons.Average(x => x.Points) / MatchesPerSeason; }
        public double AvgPlace { get => Seasons.Average(x => x.Place); }
        public int MatchesPerSeason { get => Seasons.First().Matches; }
        public double PointsDeviation
        {
            get
            {
                double mean = AvgPoints;
                return Math.Sqrt(Seasons.Average(x => Math.Pow(x.Points / (double)MatchesPerSeason - mean, 2)));
            }
        }

        public BunchBenchmarkResult() { }

        public void Append(SingleSeasonBenchmarkResult season) => Seasons.Add(season);

        public void Append(IList<SingleSeasonBenchmarkResult> seasons)
        {
            foreach (SingleSeasonBenchmarkResult season in seasons) Append(season);
        }

    }
}
