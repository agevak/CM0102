using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Benchmark.Model;

namespace CM.Model
{
    public class TacticBmResults
    {
        private static readonly string[] HTML_COLORS = new string[] { "red", "orange", "yellow", "yellowgreen", "lightgreen" };
        private const double CATEGORY_STEP = 0.05;

        public string TestNamesTitle { get; set; }
        public IList<string> TestNames { get; set; } = new List<string>();
        public IList<string> HumanTactics { get; set; } = new List<string>();
        public BunchBenchmarkResult[,] Results { get; set; }
        public int SortByColumn { get; set; } = -1;

        private IList<int> humanTacticIndexByOrder = new List<int>();

        public TacticBmResults() { }
        public TacticBmResults(string testNamesTitle, IList<string> testNames, IList<string> humanTactics) : this(testNamesTitle, testNames, humanTactics, new BunchBenchmarkResult[testNames.Count, humanTactics.Count]) { }
        public TacticBmResults(string testNamesTitle, IList<string> testNames, IList<string> humanTactics, BunchBenchmarkResult[,] results)
        {
            TestNamesTitle = testNamesTitle;
            TestNames = testNames;
            HumanTactics = humanTactics;
            Results = results;
            for (int testIndex = 0; testIndex < TestNames.Count; testIndex++)
                for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
                    Results[testIndex, humanIndex] = new BunchBenchmarkResult();
        }

        public void CalculateOverall()
        {
            TestNames.Add("Overall");
            BunchBenchmarkResult[,] oldResults = Results;
            Results = new BunchBenchmarkResult[TestNames.Count, HumanTactics.Count];
            int overallTestIndex = TestNames.Count - 1;
            for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
            {
                Results[overallTestIndex, humanIndex] = new BunchBenchmarkResult();
                for (int testIndex = 0; testIndex < TestNames.Count - 1; testIndex++)
                {
                    Results[testIndex, humanIndex] = oldResults[testIndex, humanIndex];
                    Results[overallTestIndex, humanIndex].Append(oldResults[testIndex, humanIndex].Seasons);
                }
            }
        }

        public string ToHtml(bool humanTacticsInRows)
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
            if (humanTacticsInRows)
            {
                sb.AppendLine($"<th rowspan=3>Human tactics</th>");
                sb.AppendLine($"<th colspan={TestNames.Count * 4}>{TestNamesTitle}</th>");
            }
            else
            {
                sb.AppendLine($"<th rowspan=3>{TestNamesTitle}</th>");
                sb.AppendLine($"<th colspan={HumanTactics.Count * 4}>Human tactics</th>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            if (humanTacticsInRows)
                for (int testIndex = 0; testIndex < TestNames.Count; testIndex++)
                    sb.AppendLine($"<th colspan=4>{TestNames[testIndex]}</th>");
            else
                for (int humanOrder = 0; humanOrder < HumanTactics.Count; humanOrder++)
                    sb.AppendLine($"<th colspan=4>{HumanTactics[humanTacticIndexByOrder[humanOrder]]}</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            for (int i = 0; i < (humanTacticsInRows ? TestNames.Count : HumanTactics.Count); i++)
                sb.AppendLine("<th>For</th><th>Ag</th><th>Pts</th><th>Plc</th>");
            sb.AppendLine("</tr>");

            Tuple<double, double>[] testPointsInterval = new Tuple<double, double>[TestNames.Count];
            for (int testIndex = 0; testIndex < TestNames.Count; testIndex++)
            {
                double bestPoints = 0, worstPoints = 100;
                for (int humanIndex = 0; humanIndex < HumanTactics.Count; humanIndex++)
                {
                    bestPoints = Math.Max(bestPoints, Results[testIndex, humanIndex].AvgPoints);
                    worstPoints = Math.Min(worstPoints, Results[testIndex, humanIndex].AvgPoints);
                }
                testPointsInterval[testIndex] = new Tuple<double, double>(worstPoints, bestPoints);
            }

            if (humanTacticsInRows)
            {
                for (int humanOrder = 0; humanOrder < HumanTactics.Count; humanOrder++)
                {
                    int humanIndex = humanTacticIndexByOrder[humanOrder];
                    sb.AppendLine("<tr>");
                    sb.AppendLine($"<td><strong>{HumanTactics[humanIndex]}</strong></td>");
                    for (int testIndex = 0; testIndex < TestNames.Count; testIndex++)
                    {
                        double worstPoints = testPointsInterval[testIndex].Item1, bestPoints = testPointsInterval[testIndex].Item2;
                        BunchBenchmarkResult res = Results[testIndex, humanIndex];
                        int category = Math.Max(0, Math.Min(HTML_COLORS.Length - 1, HTML_COLORS.Length - 1 - (int)((bestPoints - res.AvgPoints) / CATEGORY_STEP + 1e-6)));
                        string clr = $" bgcolor='{HTML_COLORS[category]}'";
                        sb.AppendLine($"<td {clr}>{res.AvgScored:0.00}</td><td {clr}>{res.AvgConceded:0.00}</td><td {clr}>{res.AvgPoints:0.00}</td><td {clr}>{res.AvgPlace:0.00}</td>");
                    }
                    sb.AppendLine("</tr>");
                }
            }
            else
            {
                for (int testIndex = 0; testIndex < TestNames.Count; testIndex++)
                {
                    double worstPoints = testPointsInterval[testIndex].Item1, bestPoints = testPointsInterval[testIndex].Item2;
                    sb.AppendLine("<tr>");
                    sb.AppendLine($"<td><strong>{TestNames[testIndex]}</strong></td>");
                    for (int humanOrder = 0; humanOrder < HumanTactics.Count; humanOrder++)
                    {
                        int humanIndex = humanTacticIndexByOrder[humanOrder];
                        BunchBenchmarkResult res = Results[testIndex, humanIndex];
                        int category = Math.Max(0, Math.Min(HTML_COLORS.Length - 1, HTML_COLORS.Length - 1 - (int)((bestPoints - res.AvgPoints) / CATEGORY_STEP + 1e-6)));
                        string clr = $" bgcolor='{HTML_COLORS[category]}'";
                        sb.AppendLine($"<td {clr}>{res.AvgScored:0.00}</td><td {clr}>{res.AvgConceded:0.00}</td><td {clr}>{res.AvgPoints:0.00}</td><td {clr}>{res.AvgPlace:0.00}</td>");
                    }
                    sb.AppendLine("</tr>");
                }
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
