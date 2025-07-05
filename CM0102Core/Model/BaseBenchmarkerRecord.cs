using System.Globalization;

namespace CM.Model
{
    public abstract class BaseBenchmarkerRecord
    {
        protected const char DELIMITER = ';';

        public int Seasons { get; set; }
        public double Scored { get; set; }
        public double Conceded { get; set; }
        public double Points { get; set; }
        public double Place { get; set; }
        public double GoalDiff { get => Scored - Conceded; }

        protected void LoadFromString(string[] tokens, int startIndex)
        {
            Seasons = int.Parse(tokens[startIndex]);
            Scored = double.Parse(tokens[startIndex + 1].Replace(",", "."), CultureInfo.InvariantCulture);
            Conceded = double.Parse(tokens[startIndex + 2].Replace(",", "."), CultureInfo.InvariantCulture);
            Points = double.Parse(tokens[startIndex + 3].Replace(",", "."), CultureInfo.InvariantCulture);
            Place = double.Parse(tokens[startIndex + 4].Replace(",", "."), CultureInfo.InvariantCulture);
        }

        protected BaseBenchmarkerRecord() { }

        public override string ToString() => $"{Seasons}{DELIMITER}{Scored.ToString().Replace(",", ".")}{DELIMITER}{Conceded.ToString().Replace(",", ".")}{DELIMITER}{Points.ToString().Replace(",", ".")}{DELIMITER}{Place.ToString().Replace(",", ".")}";
    }
}
