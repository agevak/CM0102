using System;
using CM.Save.Model;

namespace CM.Model
{
    public enum PlayerPosition
    {
        GK,
        DC,
        DL,
        DMC,
        DML,
        MC,
        ML,
        AMC,
        AML,
        FC,
        FL
    }

    public static class PlayerPositionExtensions
    {
        public static bool IsPlayerProperForPosition(this PlayerPosition pos, TPlayer player)
        {
            switch (pos)
            {
                case PlayerPosition.GK:
                    return player.Goalkeeper >= 20;
                case PlayerPosition.DC:
                    return (player.Sweeper >= 15 || player.Defender >= 15) && player.Central >= 15;
                case PlayerPosition.DL:
                    return (player.Defender >= 15 || player.WingBack >= 15) && Math.Max(player.LeftSide, player.RightSide) >= 15;
                case PlayerPosition.DMC:
                    return player.DefensiveMidfielder >= 15 && player.Central >= 15;
                case PlayerPosition.DML:
                    return (player.DefensiveMidfielder >= 15 || player.WingBack >= 15) && Math.Max(player.LeftSide, player.RightSide) >= 15;
                case PlayerPosition.MC:
                    return player.Midfielder >= 15 && player.Central >= 15;
                case PlayerPosition.ML:
                    return player.Midfielder >= 15 && Math.Max(player.LeftSide, player.RightSide) >= 15;
                case PlayerPosition.AMC:
                    return player.AttackingMidfielder >= 15 && player.Central >= 15;
                case PlayerPosition.AML:
                    return (player.AttackingMidfielder >= 15 || player.WingBack >= 15) && Math.Max(player.LeftSide, player.RightSide) >= 15;
                case PlayerPosition.FC:
                    return player.Attacker >= 15 && player.Central >= 15;
                case PlayerPosition.FL:
                    return player.Attacker >= 15 && Math.Max(player.LeftSide, player.RightSide) >= 15;
            }
            return false;
        }

        public static bool IsWinger(this PlayerPosition pos) => pos != PlayerPosition.GK && ((int)pos - 1) % 2 == 1;

        public static string GetUIName(this PlayerPosition pos)
        {
            if (pos > PlayerPosition.GK && (int)pos % 2 == 0) return $"{pos.ToString()}/R";
            else return pos.ToString();
        }
    }
}
