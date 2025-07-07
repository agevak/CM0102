using System;
using System.Collections.Generic;
using System.Linq;
using CM.Save.Model;

namespace CM.Model
{
    public class EstimatedPlayer
    {
        public static readonly bool[,] IS_MAJOR_ATTRIBUTE = new bool[Enum.GetValues(typeof(PlayerPosition)).Length, PlayerAttributeExtensions.GetAll(false).Count];
        public static readonly bool[,] IS_MINOR_ATTRIBUTE = new bool[Enum.GetValues(typeof(PlayerPosition)).Length, PlayerAttributeExtensions.GetAll(false).Count];
        public static readonly float[,] WEIGHTS = new float[Enum.GetValues(typeof(PlayerPosition)).Length, PlayerAttributeExtensions.GetAll(false).Count];
        public static readonly IList<PlayerAttribute>[] NON_ZERO_WEIGHT_ATTRIBUTES = new IList<PlayerAttribute>[Enum.GetValues(typeof(PlayerPosition)).Length];
        private static readonly float[] POSITION_PRE_NORM_MAX_RATING = new float[Enum.GetValues(typeof(PlayerPosition)).Length];

        static EstimatedPlayer()
        {
            foreach (PlayerPosition position in Enum.GetValues(typeof(PlayerPosition))) POSITION_PRE_NORM_MAX_RATING[(int)position] = 100f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DL] = 115f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DML] = 115f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.MC] = 115f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.ML] = 115f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.AMC] = 115f;
            //POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.AML] = 100f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.FC] = 130f;
            //POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.FL] = 100f;

            // GK.
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Agility] = 0.069f;
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Handling] = 0.159f;
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Reflexes] = 0.060f;
            // Reflexes stops at ~19.

            // DC.
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Aggression] = 0.024f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Jumping] = 0.027f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Marking] = 0.115f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Pace] = 0.019f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Passing] = 0.031f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Positioning] = 0.114f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Teamwork] = 0.040f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.WorkRate] = 0.022f;
            // Marking slows down a lot at ~9.
            // Tackling seems to have small effect, but it starts only at very high values (> 30).

            // DL.
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Aggression] = 0.014f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Dribbling] = 0.143f; // At 20.
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Jumping] = 0.056f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.RightFoot] = 0.060f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Movement] = 0.055f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Pace] = 0.029f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Passing] = 0.072f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Positioning] = 0.195f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Stamina] = 0.048f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Teamwork] = 0.098f; // Big change at ~10.
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Technique] = 0.170f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.WorkRate] = 0.038f;
            // Dribbling scale accelerate up to ~20, then simmetrical brake.
            // Movement limit at ~30.

            // DMC.
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Aggression] = 0.030f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Dribbling] = 0.037f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Movement] = 0.057f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Pace] = 0.041f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Passing] = 0.087f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Positioning] = 0.200f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Stamina] = 0.017f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Teamwork] = 0.018f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Tackling] = 0.046f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Technique] = 0.047f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Vision] = 0.090f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.WorkRate] = 0.078f;
            // Dribbling scale has acceleration for Roda, but that's not huge and attribute is minor anyways.
            // Movement stops at ~30.
            // Movement began at ~15 for Roda. Ignored because minor anyways.
            // Vision overflow at ~16.

            // DML.
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Aggression] = 0.020f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Dribbling] = 0.141f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Jumping] = 0.052f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.RightFoot] = 0.077f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Movement] = 0.077f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Pace] = 0.029f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Passing] = 0.073f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Positioning] = 0.182f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Stamina] = 0.053f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Teamwork] = 0.112f; // Big change at ~10.
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Technique] = 0.168f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.WorkRate] = 0.035f;
            // TODO: Dribbling scale.
            // Movement limit at ~30.

            // MC.
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Dribbling] = 0.159f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Jumping] = 0.065f; // Starts mostly at ~15.
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Movement] = 0.154f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Pace] = 0.039f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Passing] = 0.064f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Positioning] = 0.117f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Stamina] = 0.080f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Teamwork] = 0.013f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Technique] = 0.187f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.WorkRate] = 0.055f;
            // TODO: Dribbling scale.
            // TODO: Movement limit. Assumed at ~30, but not proved.
            // TODO: Passing scale.

            // ML.
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Aggression] = 0.018f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Dribbling] = 0.131f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.RightFoot] = 0.077f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Movement] = 0.249f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Pace] = 0.012f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Passing] = 0.072f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Stamina] = 0.068f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Teamwork] = 0.099f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.Technique] = 0.158f;
            WEIGHTS[(int)PlayerPosition.ML, (int)PlayerAttribute.WorkRate] = 0.036f;
            // Dribbling scale is close to linear up to ~30-35, then slows down a lot.
            // Movement limit at ~30.

            // AMC.
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Crossing] = 0.025f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Dribbling] = 0.193f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Jumping] = 0.045f; // Starts mostly at ~15.
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Movement] = 0.155f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Pace] = 0.046f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Passing] = 0.067f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Positioning] = 0.124f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Stamina] = 0.079f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Teamwork] = 0.022f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Technique] = 0.203f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.WorkRate] = 0.078f;
            // TODO: Dribbling scale.
            // Movement limit at ~30 (seems to be a bit more than 30).

            // FC.
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Dribbling] = 0.080f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Heading] = 0.029f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Jumping] = 0.111f; // Starts at 15.
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Movement] = 0.177f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Pace] = 0.030f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Passing] = 0.063f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Positioning] = 0.092f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Stamina] = 0.083f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Teamwork] = 0.023f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Technique] = 0.141f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.WorkRate] = 0.040f;
            // TODO: Dribbling scale / limit. Close to linear, but almost stops after ~35.
            // TODO: Movement scale and limit. Limit at >30.

            // For all outfield positions:
            // Tackling seems to help if >= ~30.
            // Vision seems to help below overflow threshold ~14.

            // Populate manual weights.

            // Determine non zero weighted attributes.
            foreach (PlayerPosition position in Enum.GetValues(typeof(PlayerPosition)))
            {
                IList<PlayerAttribute> list = NON_ZERO_WEIGHT_ATTRIBUTES[(int)position] = new List<PlayerAttribute>();
                foreach (PlayerAttribute attribute in Enum.GetValues(typeof(PlayerAttribute))) if (WEIGHTS[(int)position, (int)attribute] > 1e-9) list.Add(attribute);
            }
            // Determine major and minor attributes.
            foreach (PlayerPosition position in Enum.GetValues(typeof(PlayerPosition)))
                foreach (PlayerAttribute attribute in Enum.GetValues(typeof(PlayerAttribute)))
                {
                    float weight = WEIGHTS[(int)position, (int)attribute];
                    if (weight <= 1e-9) continue;
                    if (weight >= 0.100f) IS_MAJOR_ATTRIBUTE[(int)position, (int)attribute] = true;
                    else IS_MINOR_ATTRIBUTE[(int)position, (int)attribute] = true;
                }
        }

        public TStaff Staff { get; set; }
        public TPlayer Player { get; set; }
        public IList<TContract> Contracts { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string NationShortName { get; set; }
        public string ClubShortName {  get; set; }
        public int Price { get; set; }
        public HashSet<PlayerPosition> ProperPositions { get; set; } = new HashSet<PlayerPosition>();
        public float[][] RatingByPosition { get; set; } = new float[2][] { new float[Enum.GetValues(typeof(PlayerPosition)).Length], new float[Enum.GetValues(typeof(PlayerPosition)).Length] };
        public float[] BestRating { get; set; } = new float[] { -1, -1 };
        public float[] BestRatingAtProperPosition { get; set; } = new float[] { -1, -1 };
        public PlayerPosition[] BestRatedPosition { get; set; } = new PlayerPosition[2];
        public PlayerPosition[] BestRatedProperPosition { get; set; } = new PlayerPosition[2];
        public PlayerPosition[] AssignedPosition { get; set; } = new PlayerPosition[2];
        public float[] AssignedPositionRating { get => new float[] { RatingByPosition[0][(int)AssignedPosition[0]], RatingByPosition[1][(int)AssignedPosition[1]]  }; }

        public EstimatedPlayer() { }
        public EstimatedPlayer(TStaff staff, TPlayer player, IList<TContract> contracts)
        {
            if (contracts == null) contracts = new List<TContract>();
            Staff = staff;
            Player = player;
            Contracts = contracts;
        }

        public void Estimate()
        {
            // Calculate value (price).
            Price = 0;
            TContract contract = Contracts.FirstOrDefault();
            if (contract != null)
            {
                Price = Staff.Value;
                if (contract.RelegationRC > 1 || contract.NonPlayingRC > 1 || contract.NonPromotionRC > 1) Price = Math.Min(Price, contract.ReleaseFee);
                if (contract.MinimumFeeRC > 0) Price = Math.Min(Price, contract.ReleaseFee);
            }

            for (int isPotential = 0; isPotential <= 1; isPotential++)
            {
                foreach (PlayerPosition position in Enum.GetValues(typeof(PlayerPosition)))
                {
                    if (position.IsPlayerProperForPosition(Player)) ProperPositions.Add(position);
                    float rating = 0, weigthSum = 0;
                    foreach (int attributeIndex in NON_ZERO_WEIGHT_ATTRIBUTES[(int)position])
                    {

                        PlayerAttribute attribute = (PlayerAttribute)attributeIndex;
                        float weight = WEIGHTS[(int)position, attributeIndex];
                        weigthSum += weight;
                        sbyte intr = attribute.GetIntrinsic(Staff, Player);
                        int ability = (isPotential == 1 && Staff.YearOfBirth <= 25) ? Math.Max(Player.CurrentAbility, Player.PotentialAbility - 15) : Player.CurrentAbility;
                        float value = attribute.IsCaDependent() ? attribute.GetInMatchValue(intr, ability, Staff, Player) : intr;
                        bool handled = false;

                        if (isPotential == 0 && (/*Staff.ID == 410 ||*/ Staff.ID == 543) && position == PlayerPosition.DC && attribute == PlayerAttribute.Positioning)
                            ;

                        if (attribute == PlayerAttribute.Jumping)
                        {
                            if (position == PlayerPosition.FC)
                            {
                                if (intr <= 10) rating += weight * (value - 1.0f) / 9.0f * 0.01f;
                                else if (intr <= 14) rating += weight * (value - 10.0f) / 4.0f * 0.06f;
                                else
                                {
                                    if (intr >= 15) rating += weight * 0.03f;
                                    if (intr >= 16) rating += weight * 0.05f;
                                    if (intr >= 17) rating += weight * 0.10f;
                                    if (intr >= 18) rating += weight * 0.15f;
                                    if (intr >= 19) rating += weight * 0.25f;
                                    if (intr >= 20) rating += weight * 0.35f;
                                }
                                handled = true;
                            }
                            else if (position == PlayerPosition.MC || position == PlayerPosition.AMC)
                            {
                                if (intr <= 14) rating += weight * (value - 1.0f) / 13.0f * 0.19f;
                                else
                                {
                                    if (intr >= 15) rating += weight * 0.03f;
                                    if (intr >= 16) rating += weight * 0.05f;
                                    if (intr >= 17) rating += weight * 0.09f;
                                    if (intr >= 18) rating += weight * 0.13f;
                                    if (intr >= 19) rating += weight * 0.21f;
                                    if (intr >= 20) rating += weight * 0.30f;
                                }
                                handled = true;
                            }
                        }
                        else if (attribute == PlayerAttribute.Teamwork)
                        {
                            if (position.IsWinger())
                            {
                                // Threshold is at ~ 5-10.
                                if (intr <= 5) rating += weight * (value - 1.0f) / 4.0f * 3.0f / 19.0f;
                                else if (intr <= 10) rating += weight * (value - 5.0f) / 5.0f * 10.0f / 19.0f;
                                else rating += weight * (value - 10.0f) / 9.0f * 6.0f / 19.0f;
                                handled = true;
                            }
                        }
                        else if (attribute == PlayerAttribute.RightFoot)
                        {
                            if (position.IsWinger())
                                value = intr = Math.Max(Player.LeftSide >= 15 ? Player.LeftFoot : Player.RightFoot, Player.RightSide >= 15 ? Player.RightSide : Player.LeftFoot);
                        }
                        else if (attribute == PlayerAttribute.Reflexes)
                        {
                            // Stops at ~19.
                            rating += weight * Math.Min(19.0f, value) / 19.0f;
                            handled = true;
                        }
                        else if (attribute == PlayerAttribute.Marking)
                        {
                            // Slows down a lot ~9. Stored weight is at 19. 2/3 of it spends at 0..9.
                            rating += Math.Min(9, value) / 9f * weight * 2f / 3f;
                            if (value > 9) rating += (Math.Min(19, value) - 9) / 10f * weight / 3f;
                            if (value > 19) rating += (Math.Min(30, value) - 19) / 11f * weight / 5f;
                            if (value > 30) rating += (value - 30) / 30f * weight / 5f;
                            handled = true;
                        }
                        else if (attribute == PlayerAttribute.Dribbling && position == PlayerPosition.DL)
                        {
                            // Accelerate up to ~20, then simmetrical brake.
                            if (value <= 20) rating += weight * value * value / 20.0f / 20.0f;
                            else
                            {
                                value = Math.Min(40, value);
                                rating += 2.0f * weight - weight * (40.0f - value) * (40.0f - value) / 20.0f / 20.0f;
                            }
                            handled = true;
                        }
                        else if (attribute == PlayerAttribute.Movement) // For DL, DML, DMC, ML, AMC.
                        {
                            // Stops at ~30.
                            rating += weight * Math.Min(30.0f, value) / 30.0f;
                            handled = true;
                        }
                        else if (attribute == PlayerAttribute.Vision)
                        {
                            // Cap with passing.
                            value = Math.Min(value, PlayerAttribute.Passing.GetInMatchValue(PlayerAttribute.Passing.GetIntrinsic(Staff, Player), ability, Staff, Player));

                            // Overflow at ~14. TODO: make patch.
                            rating += weight * Math.Min(14.0f, value) / 14.0f;
                            handled = true;
                        }
                        if (!handled)
                        {
                            if (attribute.IsCaDependent()) rating += weight * value / 19.0f;
                            else rating += weight * (value - 1.0f) / 19.0f;
                        }
                    }
                    if (weigthSum > 0) rating /= weigthSum;
                    rating *= POSITION_PRE_NORM_MAX_RATING[(int)position];

                    RatingByPosition[isPotential][(int)position] = rating;
                    if (rating > BestRating[isPotential])
                    {
                        BestRating[isPotential] = rating;
                        BestRatedPosition[isPotential] = position;
                    }
                    if (position.IsPlayerProperForPosition(Player) && rating > BestRatingAtProperPosition[isPotential])
                    {
                        BestRatingAtProperPosition[isPotential] = rating;
                        BestRatedProperPosition[isPotential] = position;
                    }
                }
            }
        }
    }
}
