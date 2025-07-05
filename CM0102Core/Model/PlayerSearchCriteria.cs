namespace CM.Model
{
    public class PlayerSearchCriteria
    {
        public const int BEST_REGARDLESS_POSITION = -2;
        public const int BEST_AT_PROPER_POSITION_POSITION = -1;

        public string Name { get; set; }
        public int Nation { get; set; }
        public int Based { get; set; }
        public int Club { get; set; }
        public Range Age { get; set; }
        public Range Ca { get; set; }
        public Range Pa { get; set; }
        public Range WorldReputation { get; set; }
        public bool[] Positions { get; set; }
        public bool[] Sides { get; set; }
        public int RatedPosition { get; set; }
        public Range Rating { get; set; }
        public Range PotentialRating { get; set; }
        public AttributeValueType AttributeValueType { get; set; }
        public Range[] AttributeRanges { get; set; }
        public int ContractStatus { get; set; }
        public int TransferStatus { get; set; }
        public bool OnlyNotNeededByClub { get; set; }
        public Range Price { get; set; }

        public PlayerSearchCriteria()
        {
            ResetAll();
        }

        public void ResetAll()
        {
            ResetMain();
            ResetAttributes();
        }
        public void ResetMain()
        {
            Name = "";
            Nation = -1;
            Based = -1;
            Club = -1;
            Age = new Range(0, 99);
            Ca = new Range(0, 200);
            Pa = new Range(0, 200);
            WorldReputation = new Range(0, 10000);
            Positions = new bool[8];
            Sides = new bool[3];
            RatedPosition = BEST_REGARDLESS_POSITION;
            Rating = new Range(0, 999);
            PotentialRating = new Range(0, 999);
            ContractStatus = -1;
            TransferStatus = -1;
            OnlyNotNeededByClub = false;
            Price = new Range(0, 1000000000);
        }
        public void ResetAttributes()
        {
            AttributeValueType = AttributeValueType.Normalized;
            AttributeRanges = new Range[PlayerAttributeExtensions.GetAll(false).Count];
            for (int i = 0; i < AttributeRanges.Length; i++) AttributeRanges[i] = ((PlayerAttribute)i).IsCaDependent() ? new Range(-128, 127) : new Range(0, 20);
        }
    }
}
