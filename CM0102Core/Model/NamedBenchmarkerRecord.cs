namespace CM.Model
{
    public class NamedBenchmarkerRecord : BaseBenchmarkerRecord
    {
        public static NamedBenchmarkerRecord FromString(string s)
        {
            string[] tokens = s.Split(DELIMITER);
            NamedBenchmarkerRecord result = new NamedBenchmarkerRecord()
            {
                Name = tokens[0]
            };
            result.LoadFromString(tokens, 1);
            return result;
        }

        public string Name { get; set; }

        public NamedBenchmarkerRecord() { }
        public NamedBenchmarkerRecord(string name)
        {
            Name = name;
        }

        public override string ToString() => $"{Name}{DELIMITER}" + base.ToString();
    }
}
