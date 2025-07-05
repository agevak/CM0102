using System.Globalization;

namespace CM.Model
{
    public abstract class BaseBenchmarkerRecord
    {
        protected const char DELIMITER = ';';

        public int Seasons { get; set; }
        public float Scored { get; set; }
        public float Conceded { get; set; }
        public float Points { get; set; }
        public float Place { get; set; }
        public float GoalDiff { get => Scored - Conceded; }

        protected void LoadFromString(string[] tokens, int startIndex)
        {
            Seasons = int.Parse(tokens[startIndex]);
            Scored = float.Parse(tokens[startIndex + 1].Replace(",", "."), CultureInfo.InvariantCulture);
            Conceded = float.Parse(tokens[startIndex + 2].Replace(",", "."), CultureInfo.InvariantCulture);
            Points = float.Parse(tokens[startIndex + 3].Replace(",", "."), CultureInfo.InvariantCulture);
            Place = float.Parse(tokens[startIndex + 4].Replace(",", "."), CultureInfo.InvariantCulture);
        }

        protected BaseBenchmarkerRecord() { }

        public override string ToString() => $"{Seasons}{DELIMITER}{Scored.ToString().Replace(",", ".")}{DELIMITER}{Conceded.ToString().Replace(",", ".")}{DELIMITER}{Points.ToString().Replace(",", ".")}{DELIMITER}{Place.ToString().Replace(",", ".")}";
    }
}
