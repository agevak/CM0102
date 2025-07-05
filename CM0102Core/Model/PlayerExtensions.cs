using System.Text;
using CM.Save.Model;

namespace CM.Model
{
    public static class PlayerExtensions
    {
        public unsafe static sbyte GetPositionalAttribute(this TPlayer player, int i)
        {
            fixed (sbyte* p = &player.Goalkeeper) return *(p + i);
        }

        public static string GetPositionUIString(this TPlayer player)
        {
            StringBuilder pos = new StringBuilder();
            if (player.Goalkeeper >= 15) pos.Append("/GK");
            if (player.Sweeper >= 15) pos.Append("/SW");
            if (player.Defender >= 15) pos.Append("/D");
            if (player.DefensiveMidfielder >= 15) pos.Append("/DM");
            if (player.Midfielder >= 15) pos.Append("/M");
            if (player.AttackingMidfielder >= 15) pos.Append("/AM");
            if (player.Attacker >= 15) pos.Append("/F");
            if (pos.ToString().Length == 0 && player.WingBack >= 15) pos.Append("D/AM");

            StringBuilder side = new StringBuilder();
            if (player.RightSide >= 15) side.Append("R");
            if (player.LeftSide >= 15) side.Append("L");
            if (player.Central >= 15) side.Append("C");

            return pos.ToString().Trim(' ', '/') + " " + side.ToString().Trim(' ', '/');
        }
    }
}
