using System;
using System.Collections.Generic;
using System.Linq;
using CM.Save.Model;

namespace CM.Model
{
    public enum PlayerAttribute : int
    {
        Acceleration,
        Aggression,
        Agility,
        Anticipation,
        Balance,
        Bravery,
        Consistency,
        Corners,
        Crossing,
        Decisions,
        Dirtiness,
        Dribbling,
        Finishing,
        Flair,
        FreeKicks,
        Handling,
        Heading,
        ImportantMatches,
        InjuryProneness,
        Jumping,
        Leadership,
        LeftFoot,
        LongShots,
        Marking,
        Movement,
        NaturalFitness,
        OneOnOnes,
        Pace,
        Passing,
        Penalties,
        Positioning,
        Reflexes,
        RightFoot,
        Stamina,
        Strength,
        Tackling,
        Teamwork,
        Technique,
        ThrowIns,
        Versatility,
        Vision,
        WorkRate,
        Adaptability, // TStaff begins.
        Ambition,
        Determination,
        Loyality,
        Pressure,
        Professionalism,
        Sportsmanship,
        Temperament
}

public static class PlayerAttributeExtensions
    {
        public static PlayerAttribute[] CA_DEPENDENT_ATTRIBUTES = new PlayerAttribute[]
        {
            PlayerAttribute.Anticipation, PlayerAttribute.Crossing, PlayerAttribute.Decisions, PlayerAttribute.Dribbling, PlayerAttribute.Finishing, PlayerAttribute.Handling, PlayerAttribute.Heading,
            PlayerAttribute.LongShots, PlayerAttribute.Marking, PlayerAttribute.Movement, PlayerAttribute.OneOnOnes, PlayerAttribute.Passing, PlayerAttribute.Penalties, PlayerAttribute.Positioning,
            PlayerAttribute.Reflexes, PlayerAttribute.Tackling, PlayerAttribute.ThrowIns, PlayerAttribute.Vision
        };
        public static readonly bool[] IS_CA_DEPENDENT_ARRAY;
        public static readonly int[] NORM_MAX_IN_MATCH_VALUE = new int[Enum.GetValues(typeof(PlayerAttribute)).Length];

        static PlayerAttributeExtensions()
        {
            // Initialize CA dependent and indepenent.
            IS_CA_DEPENDENT_ARRAY = new bool[Enum.GetValues(typeof(PlayerAttribute)).Length];
            foreach (PlayerAttribute attribute in CA_DEPENDENT_ATTRIBUTES) IS_CA_DEPENDENT_ARRAY[(byte)attribute] = true;

            // Initialize normalization bounds.
            foreach (PlayerAttribute attribute in Enum.GetValues(typeof(PlayerAttribute))) NORM_MAX_IN_MATCH_VALUE[(int)attribute] = 20;
            /*NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Anticipation] = 14;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Crossing] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Decisions] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Dribbling] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Finishing] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Handling] = 22;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Heading] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.LongShots] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Marking] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Movement] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.OneOnOnes] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Passing] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Penalties] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Positioning] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Reflexes] = 25;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Tackling] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.ThrowIns] = 30;
            NORM_MAX_IN_MATCH_VALUE[(int)PlayerAttribute.Vision] = 30;*/
        }

        public static string ToString(int attribute) => attribute >= 0 ? ((PlayerAttribute)attribute).ToString() : "All";
        public static int FromString(string s) => s != "All" ? (int)Enum.Parse(typeof(PlayerAttribute), s) : -1;
        public static string SplitWords(string s)
        {
            string ret = "";
            foreach (char c in s)
            {
                if (ret.Length == 0) ret += c;
                else
                {
                    if (char.IsUpper(c)) ret += " ";
                    ret += char.ToLower(c);
                }
            }
            return ret;
        }
        public static string GetUIName(this PlayerAttribute attribute)
        {
            switch (attribute)
            {
                case PlayerAttribute.FreeKicks: return "Set pieces";
                case PlayerAttribute.ImportantMatches: return "Imp matches";
                case PlayerAttribute.InjuryProneness: return "Injury prone";
                case PlayerAttribute.Leadership: return "Influence";
                case PlayerAttribute.Movement: return "Off the ball";
                case PlayerAttribute.NaturalFitness: return "Nat fitness";
                case PlayerAttribute.Vision: return "Creativity";
                default: return SplitWords(ToString((int)attribute));
            }
        }

        public static IList<int> GetAll(bool includeNull)
        {
            IList<int> allAttributes = (Enum.GetValues(typeof(PlayerAttribute)) as PlayerAttribute[]).Select(x => (int)x).ToList();
            if (includeNull) allAttributes.Add(-1);
            return allAttributes;
        }
        public static bool IsCaDependent(this PlayerAttribute a) => IS_CA_DEPENDENT_ARRAY[(byte)a];

        public unsafe static sbyte GetIntrinsic(this PlayerAttribute a, TStaff staff, TPlayer player)
        {
            if (a < PlayerAttribute.Adaptability) fixed (sbyte* p = &player.Acceleration) return *(p + (byte)a);
            else fixed (byte* s = &staff.Adaptability) return (sbyte)*(s + (a - PlayerAttribute.Adaptability));
        }
        public unsafe static void Set(this PlayerAttribute a, TStaff staff, TPlayer player, sbyte value)
        {
            if (a < PlayerAttribute.Adaptability) fixed (sbyte* p = &player.Acceleration) *(p + (byte)a) = value;
            else fixed (byte* s = &staff.Adaptability) *(s + (a - PlayerAttribute.Adaptability)) = (byte)value;
        }

        public static int GetMaxNormInMatchValue(this PlayerAttribute a) => NORM_MAX_IN_MATCH_VALUE[(int)a];
        public static sbyte FindIntrinsicForCaNormValue(this PlayerAttribute a, float normValue, short ability, TStaff staff = null, TPlayer player = null)
        {
            if (!a.IsCaDependent()) return (sbyte)Math.Min(20, Math.Max(1, (int)(normValue + 1e-9)));
            int l = -110, r = 110;
            while (l < r)
            {
                int m = l + r;
                if (m >= 0) m /= 2; else m = (m - 1) / 2;
                float curNormValue = a.GetFloatNormValue((sbyte)m, ability, staff, player);
                if (curNormValue >= normValue) r = m;
                else l = m + 1;
            }
            return (sbyte)l;
        }
        public static sbyte FindIntrinsicForInMatchValue(this PlayerAttribute a, float inMatchValue, short ability, TStaff staff = null, TPlayer player = null)
        {
            if (!a.IsCaDependent()) return (sbyte)Math.Min(20, Math.Max(1, (int)(inMatchValue + 1e-9)));
            int l = -110, r = 110;
            while (l < r)
            {
                int m = l + r;
                if (m >= 0) m /= 2; else m = (m - 1) / 2;
                float curInMatch = a.GetCaDepInMatchValue((sbyte)m, ability, staff, player);
                if (curInMatch >= inMatchValue) r = m;
                else l = m + 1;
            }
            return (sbyte)l;
        }
        public static float GetValue(this PlayerAttribute a, AttributeValueType valueType, TStaff staff, TPlayer player)
        {
            switch (valueType)
            {
                case AttributeValueType.Normalized: return a.GetFloatNormValue(a.GetIntrinsic(staff, player), player.CurrentAbility, staff, player);
                case AttributeValueType.PotentialNormalized: return a.GetFloatNormValue(a.GetIntrinsic(staff, player), player.PotentialAbility, staff, player);
                default: return a.GetIntrinsic(staff, player);
            }
        }
        public static int GetNormValue(this PlayerAttribute a, TStaff staff, TPlayer player) => a.GetNormValue(a.GetIntrinsic(staff, player), player.CurrentAbility, staff, player);
        public static int GetNormValue(this PlayerAttribute a, sbyte intrinsic, int ability, TStaff staff = null, TPlayer player = null)
        {
            if (!a.IsCaDependent()) return intrinsic;
            float v = a.GetFloatNormValue(intrinsic, ability, staff, player);
            return Math.Max(1, (int)(v + 0.5 + 1e-9));
        }
        public static float GetFloatNormValue(this PlayerAttribute a, TStaff staff, TPlayer player) => a.GetFloatNormValue(a.GetIntrinsic(staff, player), player.CurrentAbility, staff, player);
        public static float GetFloatNormValue(this PlayerAttribute a, sbyte intrinsic, int ability, TStaff staff = null, TPlayer player = null)
        {
            if (!a.IsCaDependent()) return intrinsic;
            float v = a.GetCaDepInMatchValue(intrinsic, ability, staff, player);
            //v = 1 + v * 19f / NORM_MAX_IN_MATCH_VALUE[(int)a];
            return v;
        }
        public static float GetInMatchValue(this PlayerAttribute a, TStaff staff, TPlayer player) => a.GetInMatchValue(a.GetIntrinsic(staff, player), player.CurrentAbility, staff, player);
        public static float GetInMatchValue(this PlayerAttribute a, sbyte intrinsic, int ability, TStaff staff = null, TPlayer player = null)
        {
            if (a.IsCaDependent()) return a.GetCaDepInMatchValue(intrinsic, ability, staff, player);
            else return a.GetNonCaDepInMatchValue(intrinsic, ability, player, staff);
        }
        public static float GetCaDepInMatchValue(this PlayerAttribute a, sbyte intrinsic, int ability, TStaff staff = null, TPlayer player = null)
        {
            int originalAbility = ability;
            ability = (byte)(ability / 2 + 80);
            float intrinsicFloat = intrinsic;
            float v = (2 * intrinsicFloat + ability) * 0.1f;
            bool deservesBonus = ability > 150;
            switch (a)
            {
                case PlayerAttribute.Anticipation:
                    v = Math.Max(0, v + 0.23f) / 2 + 5;
                    break;
                case PlayerAttribute.Crossing:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 16, 1.25f);
                    break;
                case PlayerAttribute.Finishing:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 16, 1.25f);
                    break;
                case PlayerAttribute.Heading:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 18, 1.2f);
                    break;
                case PlayerAttribute.Marking:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 16, 1.2f);
                    break;
                case PlayerAttribute.Movement:
                    intrinsicFloat = intrinsic;
                    if (staff != null && staff.YearOfBirth > 36) intrinsicFloat *= 0.75f;
                    else if (player != null && player.DefensiveMidfielder == 20) intrinsicFloat *= 0.85f;
                    v = (2 * intrinsicFloat + ability) * 0.1f;
                    v = (v + 0.13f) * 0.65f + 7;
                    break;
                case PlayerAttribute.Passing:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 18, 1.25f);
                    break;
                case PlayerAttribute.Positioning:
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 16, 1.2f);
                    break;
                case PlayerAttribute.Vision:
                    intrinsicFloat = intrinsic;
                    if (intrinsic > 10)
                    {
                        if (originalAbility < 80) intrinsicFloat = 10 + (intrinsic - 10) * 0.75f;
                        else if (originalAbility < 125) intrinsicFloat = 10 + (intrinsic - 10) * 0.85f;
                    }
                    v = (2 * intrinsicFloat + ability) * 0.1f;
                    v = ApplyExpBonusToInMatchValue(deservesBonus, v, 17, 1.2f);
                    break;
            }
            v = Math.Max(0, v);
            return v;
        }
        private static float GetNonCaDepInMatchValue(this PlayerAttribute a, sbyte intrinsic, int ability, TPlayer player = null, TStaff staff = null)
        {
            float v = intrinsic;
            switch (a)
            {
                case PlayerAttribute.Acceleration:
                    v = intrinsic * 2.0f / 3 + 0.2f;
                    v = ApplyExpBonusToInMatchValue(true, v, 12, 1.25f);
                    break;
                case PlayerAttribute.Agility:
                    v = intrinsic * 0.5f + 0.2f;
                    break;
                case PlayerAttribute.Aggression:
                    v = intrinsic * 0.5f;
                    break;
                case PlayerAttribute.Balance:
                    v = intrinsic * 0.5f + 0.2f;
                    break;
                case PlayerAttribute.Bravery:
                    v = intrinsic * 0.5f + 0.2f;
                    break;
                case PlayerAttribute.Corners:
                    v = intrinsic * 0.5f + 0.2f;
                    break;
                case PlayerAttribute.Flair:
                    v = intrinsic;
                    break;
                case PlayerAttribute.FreeKicks:
                    v = intrinsic * 0.57f + 0.2f;
                    break;
                case PlayerAttribute.Jumping:
                    v = intrinsic;
                    break;
                case PlayerAttribute.Pace:
                    v = intrinsic * 2.0f / 3 + 0.2f;
                    v = ApplyExpBonusToInMatchValue(true, v, 12, 1.25f);
                    break;
                case PlayerAttribute.Strength:
                    v = intrinsic * 0.5f + 0.2f;
                    break;
                case PlayerAttribute.Stamina:
                    v = intrinsic * 0.65f + 7.25f;
                    break;
                case PlayerAttribute.Teamwork: // Not implemented.
                    v = intrinsic;
                    break;
                case PlayerAttribute.Technique:
                    v = intrinsic;
                    break;
                case PlayerAttribute.Versatility:
                    v = intrinsic;
                    break;
            }
            v = Math.Max(0, v);
            return v;
        }
        private static float ApplyExpBonusToInMatchValue(bool deservesBonus, float val, int threshold, float power)
        {
            if (deservesBonus && val > threshold) val = threshold + (float)Math.Pow(val - threshold, power);
            return val;
        }
    }
}
