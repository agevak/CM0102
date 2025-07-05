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
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.GK] = 106f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DC] = 120f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DL] = 135f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DMC] = 140f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.DML] = 140f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.MC] = 140f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.ML] = 100f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.AMC] = 140f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.AML] = 100f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.FC] = 150f;
            POSITION_PRE_NORM_MAX_RATING[(int)PlayerPosition.FL] = 100f;

            // GK.
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Agility] = 0.064f;
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Handling] = 0.3325f;
            WEIGHTS[(int)PlayerPosition.GK, (int)PlayerAttribute.Reflexes] = 0.12825f;
            // Reflexes limit.

            // DC.
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Aggression] = 0.024f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Jumping] = 0.031f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Marking] = 0.1935f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Pace] = 0.054f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Passing] = 0.101f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.Positioning] = 0.289f;
            WEIGHTS[(int)PlayerPosition.DC, (int)PlayerAttribute.WorkRate] = 0.047f;
            // Marking scale.

            // DL.
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Aggression] = 0.020f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Dribbling] = 0.227f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Jumping] = 0.067f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.RightFoot] = 0.092f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Movement] = 0.067f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Pace] = 0.056f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Passing] = 0.1824f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Positioning] = 0.4237f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Stamina] = 0.084f;
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Teamwork] = 0.105f; // Big change at ~10.
            WEIGHTS[(int)PlayerPosition.DL, (int)PlayerAttribute.Technique] = 0.175f;
            // Dribbling scale.
            // Movement limit.

            // DMC.
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Aggression] = 0.074f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Dribbling] = 0.1013f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Movement] = 0.1022f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Pace] = 0.058f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Passing] = 0.2223f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Positioning] = 0.456f;
            //WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Stamina] = 0.033f; // Make small weight just as common sense.
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Tackling] = 0.1018f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Technique] = 0.0625f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.Vision] = 0.1404f;
            WEIGHTS[(int)PlayerPosition.DMC, (int)PlayerAttribute.WorkRate] = 0.10f;
            // Dribbling scale.
            // Movement scale and limit.
            // Vision overflow.

            // DML.
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Aggression] = 0.020f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Dribbling] = 0.2407f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Jumping] = 0.062f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.RightFoot] = 0.067f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Movement] = 0.1397f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Pace] = 0.063f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Passing] = 0.1463f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Positioning] = 0.211f * 19.0f / 10.0f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Stamina] = 0.07f;
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Teamwork] = 0.115f; // Big change at ~10.
            WEIGHTS[(int)PlayerPosition.DML, (int)PlayerAttribute.Technique] = 0.17f;
            // Dribbling scale.
            // Movement limit.

            // MC.
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Dribbling] = 0.2432f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Jumping] = 0.065f; // Starts mostly at ~15.
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Movement] = 0.2246f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Pace] = 0.056f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Passing] = 0.1425f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Positioning] = 0.285f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Stamina] = 0.071f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.Technique] = 0.183f;
            WEIGHTS[(int)PlayerPosition.MC, (int)PlayerAttribute.WorkRate] = 0.075f;
            // Dribbling scale.
            // Movement limit.
            // Passing scale.

            // AMC.
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Crossing] = 0.100f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Dribbling] = 0.2837f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Jumping] = 0.053f; // Starts mostly at ~15.
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Movement] = 0.204f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Pace] = 0.058f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Passing] = 0.1691f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Positioning] = 0.3154f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Stamina] = 0.095f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.Technique] = 0.198f;
            WEIGHTS[(int)PlayerPosition.AMC, (int)PlayerAttribute.WorkRate] = 0.107f;
            // Dribbling scale.
            // Movement limit.

            // FC.
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Dribbling] = 0.1343f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Heading] = 0.07125f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Jumping] = 0.113f; // Starts at 15
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Movement] = 0.2727f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Pace] = 0.048f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Passing] = 0.16986f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Positioning] = 0.2318f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Stamina] = 0.093f;
            WEIGHTS[(int)PlayerPosition.FC, (int)PlayerAttribute.Technique] = 0.147f;
            // Dribbling scale.
            // Movement limit.

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
        public float[] RatingByPosition { get; set; } = new float[Enum.GetValues(typeof(PlayerPosition)).Length];
        public float[] PotentialRatingByPosition { get; set; } = new float[Enum.GetValues(typeof(PlayerPosition)).Length];
        public HashSet<PlayerPosition> ProperPositions { get; set; } = new HashSet<PlayerPosition>();
        public float BestRating { get; set; } = -1;
        public float BestPotentialRating { get; set; } = -1;
        public float BestRatingAtProperPosition { get; set; } = -1;
        public float BestPotentialRatingAtProperPosition { get; set; } = -1;
        public PlayerPosition BestRatedPosition { get; set; }
        public PlayerPosition BestPotentialRatedPosition { get; set; }
        public PlayerPosition BestRatedProperPosition { get; set; }
        public PlayerPosition BestPotentialRatedProperPosition { get; set; }
        public PlayerPosition AssignedPosition { get; set; }
        public PlayerPosition AssignedPotentialPosition { get; set; }
        public float AssignedPositionRating { get => RatingByPosition[(int)AssignedPosition]; }
        public float AssignedPositionPotentialRating { get => PotentialRatingByPosition[(int)AssignedPotentialPosition]; }

        public EstimatedPlayer() { }
        public EstimatedPlayer(TStaff staff, TPlayer player, IList<TContract> contracts)
        {
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
                        int ability = isPotential == 0 ? Player.CurrentAbility : Player.PotentialAbility;
                        float value = attribute.IsCaDependent() ? attribute.GetInMatchValue(intr, ability, Staff, Player) : intr;
                        bool handled = false;
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
                        if (!handled)
                        {
                            if (attribute.IsCaDependent()) rating += weight * value;
                            else rating += weight * (value - 1.0f) / 19.0f;
                        }
                    }
                    if (weigthSum > 0) rating /= weigthSum;
                    rating *= POSITION_PRE_NORM_MAX_RATING[(int)position];

                    RatingByPosition[(int)position] = rating;
                    if (rating > BestRating)
                    {
                        BestRating = rating;
                        BestRatedPosition = position;
                    }
                    if (position.IsPlayerProperForPosition(Player) && rating > BestRatingAtProperPosition)
                    {
                        BestRatingAtProperPosition = rating;
                        BestRatedProperPosition = position;
                    }
                }
            }
        }
    }
}
