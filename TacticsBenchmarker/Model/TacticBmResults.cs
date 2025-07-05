using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Benchmark.Model;
using CM.Save.Model;

namespace CM.Model
{
    public class TacticBmResults
    {
        private static readonly string[] HTML_COLORS = new string[] { "red", "orange", "yellow", "yellowgreen", "lightgreen" };
        private const double CATEGORY_STEP = 0.05;

        public IList<string> AITactics { get; set; } = new List<string>();
        public IList<string> HumanTactics { get; set; } = new List<string>();
        public BunchBenchmarkResult[,] Results { get; set; }
        public int SortByColumn { get; set; } = -1;

        private IList<int> humanTacticIndexByOrder = new List<int>();

        public TacticBmResults() { }
        public TacticBmResults(IList<string> aITactics, IList<string> humanTactics) : this(aITactics, humanTactics, new BunchBenchmarkResult[aITactics.Count, humanTactics.Count]) { }
        public TacticBmResults(IList<string> aITactics, IList<string> humanTactics, BunchBenchmarkResult[,] results)
        {
            AITactics = aITactics;
            HumanTactics = humanTactics;
            Results = results;
            for (int aiIndex = 0; aiIndex < AITactics.Count; aiIndex++)
                for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
                    Results[aiIndex, humanIndex] = new BunchBenchmarkResult();
        }

        public void CalculateOverall()
        {
            AITactics.Add("Overall");
            BunchBenchmarkResult[,] oldResults = Results;
            Results = new BunchBenchmarkResult[AITactics.Count, HumanTactics.Count];
            int overallAiIndex = AITactics.Count - 1;
            for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
            {
                Results[overallAiIndex, humanIndex] = new BunchBenchmarkResult();
                for (int aiIndex = 0; aiIndex < AITactics.Count - 1; aiIndex++)
                {
                    Results[aiIndex, humanIndex] = oldResults[aiIndex, humanIndex];
                    Results[overallAiIndex, humanIndex].Append(oldResults[aiIndex, humanIndex].Seasons);
                }
            }
        }

        public string ToHtml()
        {
            Sort();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("table, th, td {");
            sb.AppendLine("  border: 1px solid black;");
            sb.AppendLine("  border-collapse: collapse;");
            sb.AppendLine("}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th rowspan=3>AI tactics</th>");
            sb.AppendLine($"<th colspan={HumanTactics.Count * 4}>Human tactics</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            for (int humanOrder = 0; humanOrder < HumanTactics.Count; humanOrder++)
                sb.AppendLine($"<th colspan=4>{HumanTactics[humanTacticIndexByOrder[humanOrder]]}</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
                sb.AppendLine("<th>For</th><th>Ag</th><th>Pts</th><th>Plc</th>");
            sb.AppendLine("</tr>");
            for (int aiIndex = 0; aiIndex < AITactics.Count; aiIndex++)
            {
                double bestPoints = 0, worstPoints = 100;
                for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
                {
                    bestPoints = Math.Max(bestPoints, Results[aiIndex, humanIndex].AvgPoints);
                    worstPoints = Math.Min(worstPoints, Results[aiIndex, humanIndex].AvgPoints);
                }

                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{AITactics[aiIndex]}</td>");
                for (int humanOrder = 0; humanOrder < HumanTactics.Count; humanOrder++)
                {
                    int humanIndex = humanTacticIndexByOrder[humanOrder];
                    BunchBenchmarkResult res = Results[aiIndex, humanIndex];
                    int category = Math.Max(0, Math.Min(HTML_COLORS.Length - 1, HTML_COLORS.Length - 1 - (int)((bestPoints - res.AvgPoints) / CATEGORY_STEP + 1e-6)));
                    string clr = $" bgcolor='{HTML_COLORS[category]}'";
                    sb.AppendLine($"<td {clr}>{res.AvgScored:0.00}</td><td {clr}>{res.AvgConceded:0.00}</td><td {clr}>{res.AvgPoints:0.00}</td><td {clr}>{res.AvgPlace:0.00}</td>");
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        private void Sort()
        {
            if (!humanTacticIndexByOrder.Any()) for(int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++) humanTacticIndexByOrder.Add(humanIndex);
            if (SortByColumn >= 0)
                humanTacticIndexByOrder = humanTacticIndexByOrder.OrderByDescending(i => i, Comparer<int>.Create((x, y) =>
                {
                    int res = Results[SortByColumn, x].AvgPoints.CompareTo(Results[SortByColumn, y].AvgPoints);
                    return res;
                })).ToList();
            else humanTacticIndexByOrder = humanTacticIndexByOrder.OrderBy(x => HumanTactics[x]).ToList();
        }
    }
}
