using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CM.Save.Model
{
    public class Tactic
    {
        public static readonly string[] AI_PACK_FILENAMES = new string[] {
            "352_v1.pct", "442_v1.pct", "532_v1.pct", "442_v4.pct", "532_v2.pct", "532_v3.pct", "532_v4.pct", "352_v2.pct", "532_v5.pct", "532_v6.pct", "532_v7.pct", "532_v9.pct", "442_v8.pct",
            "sweeper_v1.pct", "442_push.pct", "defensive_counter.pct", "4312_v1.pct", "424_v1.pct", "442_v10.pct", "442_wide.pct", "4132.pct", "343_default.pct", "451_default.pct", "442_wide.pct",
            "sweeper_default.pct", "451_defensive.pct", "343_defensive.pct", "451_norway.pct", "442_default.pct", "442_defensive_default.pct", "442_push.pct", "442_diamond_default.pct",
            "343_default.pct", "352_default.pct", "352_defensive_default.pct", "352_attacking_default.pct", "41212_default.pct", "424_default.pct", "433_default.pct", "451_default.pct",
            "532_default.pct", "532_defensive_default.pct", "532_attacking_default.pct", "sweeper_default.pct", "4132.pct" };
        public static readonly string[] AI_PACK_DEFAULT_NAMES = new string[] {
            "352_v1", "442_v1", "532_v1", "442_v4", "532_v2", "532_v3", "532_v4", "352_v2", "532_v5", "532_v6", "532_v7", "532_v9", "442_v8", "sweeper_v1", "442_push", "defensive_counter",
            "4312_v1", "424_v1", "442_v10", "442_wide", "4132", "343_default", "451_default", "442_wide", "sweeper_default", "451_defensive", "343_defensive", "451_norway", "4-4-2",
            "4-4-2 Defensive", "4-4-2 Attacking", "4-4-2 Diamond", "3-4-3", "3-5-2", "3-5-2 Defensive", "3-5-2 Attacking", "4-1-2-1-2", "4-2-4", "4-3-3", "4-5-1", "5-3-2", "5-3-2 Defensive",
            "5-3-2 Attacking", "Sweeper", "4-1-3-2" };

        public static readonly byte[] PCT_MARKER = new byte[] { 0x73, 0xB9, 0xF4, 0x07 };
        public static readonly byte[] TCT_MARKER = new byte[] { 0x5E, 0xEC, 0x98, 0x00 };
        public const int NAME_MAX_LENGTH = 50;

        public string Name { get; set; }
        public byte[] First250 { get; set; }
        public byte[] Crc { get; set; } = new byte[] { 0, 0, 0, 0 };
        public byte[] Next1115 { get; set; }
        public byte[] Next88 { get; set; }
        public byte[] Last11 { get; set; }

        public byte[] GetNameAsBytes() => Encoding.GetEncoding("ISO-8859-1").GetBytes(Name).Concat(new byte[] { 0 }).ToArray();
        public byte[] ToTctFile() => ToTacticFile(TCT_MARKER);
        public byte[] ToPctFile() => ToTacticFile(PCT_MARKER);
        public byte[] ToTacticFile(byte[] fileMarker)
        {
            return fileMarker.Concat(First250).Concat(Crc).Concat(Next1115).Concat(Next88).Concat(Last11).Concat(new byte[] { 0xF2, 0xFF, 0xFF, 0xF2 }).ToArray();
        }

        public static string TrimName(string name) => (name.Length <= NAME_MAX_LENGTH) ? name : name.Substring(0, NAME_MAX_LENGTH);

        public static Tactic FromFile(string name, byte[] content)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name must be not null/empty.");
            name = TrimName(name);
            if (content == null) throw new ArgumentNullException("content must be not null.");
            if (content.Length != 1476 && content.Length != 1432 && content.Length != 1428) throw new ArgumentException("content must contain exacty 1428, 1432 or 1476 bytes.");
            using (BinaryReader br = new BinaryReader(new MemoryStream(content)))
            {
                byte[] marker = br.ReadBytes(4);
                //if (!Enumerable.SequenceEqual(marker, PCT_MARKER) && !Enumerable.SequenceEqual(marker, TCT_MARKER)) throw new ArgumentException("No valid marker.");
                Tactic tactic = new Tactic()
                {
                    Name = name,
                    First250 = br.ReadBytes(250),
                    Crc = br.ReadBytes(4),
                    Next1115 = br.ReadBytes(1115)
                };
                if (tactic.Next1115[tactic.Next1115.Length - 2] == 0)
                {
                    tactic.Next88 = new byte[88];
                    using (BinaryWriter bw88 = new BinaryWriter(new MemoryStream(tactic.Next88)))
                        for (int j = 0; j < 11; j++)
                        {
                            bw88.Write(br.ReadBytes(4));
                            bw88.Write(new byte[] { 0xA0, 0x00, 0x00, 0x00 });
                        }
                }
                else tactic.Next88 = br.ReadBytes(88);
                tactic.Last11 = br.ReadBytes(11);
                return tactic;
            }
        }

        public void WriteToSavFile(BinaryWriter bw)
        {
            long basePos = bw.BaseStream.Position;
            bw.Write(GetNameAsBytes());
            bw.BaseStream.Seek(basePos + NAME_MAX_LENGTH + 1, SeekOrigin.Begin);
            bw.Write(First250);
            bw.Write(Next1115);
            bw.BaseStream.Seek(basePos + 0x188B, SeekOrigin.Begin);
            bw.Write(Next88);
            bw.BaseStream.Seek(basePos + 0x5B4, SeekOrigin.Begin);
            bw.Write(Last11);
            // Set unmodified flag to true (if false, there is an asterisk in the game).
            bw.BaseStream.Seek(basePos + 0x187F, SeekOrigin.Begin);
            bw.Write((byte)1);
        }

        public override string ToString() => Name;
    }
}
