using System;
using System.Collections.Generic;
using CM.Model;

namespace CM.DB.Model
{
    public class PlayerBenchmarkRecord : BaseBenchmarkerRecord
    {
        public static PlayerBenchmarkRecord FromString(string s)
        {
            string[] tokens = s.Split(DELIMITER);
            PlayerBenchmarkRecord result = new PlayerBenchmarkRecord()
            {
                Position = (PlayerPosition)Enum.Parse(typeof(PlayerPosition), tokens[0]),
                Attribute = PlayerAttributeExtensions.FromString(tokens[1]),
                Value = int.Parse(tokens[2])
            };
            result.LoadFromString(tokens, 3);
            return result;
        }

        public PlayerPosition Position { get; set; }
        public int Attribute { get; set; }
        public int Value { get; set; }
        public IList<Tuple<int, int, int, int>> AllSeasons { get; set; } = new List<Tuple<int, int, int, int>>();

        public PlayerBenchmarkRecord() { }
        public PlayerBenchmarkRecord(PlayerPosition position, int attribute, int value)
        {
            Position = position;
            Attribute = attribute;
            Value = value;
        }

        public override string ToString() => $"{Position.ToString()}{DELIMITER}{PlayerAttributeExtensions.ToString(Attribute)}{DELIMITER}{Value}{DELIMITER}" + base.ToString();
    }
}
