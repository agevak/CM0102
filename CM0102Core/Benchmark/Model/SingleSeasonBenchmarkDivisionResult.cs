using System.Collections.Generic;
using System.Linq;

namespace CM.Benchmark.Model
{
    public class SingleSeasonBenchmarkDivisionResult
    {
        public IDictionary<int, SingleSeasonBenchmarkResult> ClubResults { get; set; }

        public SingleSeasonBenchmarkDivisionResult() { }
        public SingleSeasonBenchmarkDivisionResult(IList<int> clubIds)
        {
            ClubResults = new Dictionary<int, SingleSeasonBenchmarkResult>();
            foreach (int clubId in clubIds) ClubResults[clubId] = null;
        }

        public void Reset()
        {
            foreach (int id in ClubResults.Keys.ToList()) ClubResults[id] = null;
        }
    }
}
