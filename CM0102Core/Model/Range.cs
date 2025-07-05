namespace CM.Model
{
    public class Range
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public Range() { }
        public Range(Range range) : this(range.Lower, range.Upper) { }
        public Range(int lower, int upper)
        {
            Lower = lower;
            Upper = upper;
        }

        public bool Matches(int val) => Lower <= val && val <= Upper;
        public bool Matches(double val) => Lower - 1e-9 <= val && val <= Upper + 1e-9;
    }
}
