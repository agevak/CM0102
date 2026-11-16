using CM.Save.Model;

namespace CM.Model
{
    public static class ContractExtensions
    {
        // The game reads a player's squad number from his contract for that club: byte +0x3A
        // of the 80-byte contract record (bits 32..39 of Unknown18_1), stored negated
        // (-10 = shirt 10, 0 = none); see 0x85A760 in the game exe. TPlayer.SquadNumber is only
        // used for staff created after the database.
        public static int GetSquadNumber(this TContract contract)
        {
            return System.Math.Abs((int)unchecked((sbyte)(byte)(contract.Unknown18_1 >> 32)));
        }
        public static void SetSquadNumber(this TContract contract, int number)
        {
            ulong value = unchecked((byte)(sbyte)(-number));
            contract.Unknown18_1 = (contract.Unknown18_1 & ~(0xFFUL << 32)) | (value << 32);
        }

        public static TransferStatus GetTransferStatus(int status)
        {
            if ((status & 1) != 0) return TransferStatus.ListedByClub;
            else if ((status & 8) != 0) return TransferStatus.ListedByRequest;
            else if ((status & 2) != 0) return TransferStatus.ListedForLoan;
            else return TransferStatus.Unknown; // status == 4
        }
        public static string GetTransferStatusUIString(int status)
        {
            if ((status & 1) != 0) return "Listed by club";
            else if ((status & 8) != 0) return "Listed by request";
            else if ((status & 2) != 0) return "List for loan";
            else if (status == 4) return "Unknown";
            return $"{status}UnkownTransferStatus";
        }
        public static SquadStatus GetSquadStatus(int status)
        {
            if ((status & 240) == 0) return SquadStatus.Uncertain;
            else if ((status & 224) == 0) return SquadStatus.Indispensable;
            else if ((status & 208) == 0) return SquadStatus.FirstTeam;
            else if ((status & 192) == 0) return SquadStatus.SquadRotation;
            else if ((status & 176) == 0) return SquadStatus.Backup;
            else if ((status & 160) == 0) return SquadStatus.HotProspect;
            else if ((status & 144) == 0) return SquadStatus.DecentYoung;
            else if ((status & 128) == 0) return SquadStatus.NotNeeded;
            else if ((status & 112) == 0) return SquadStatus.OnTrial;
            else return SquadStatus.Uncertain;
        }
        public static string GetSquadStatusUIString(int status)
        {
            if ((status & 240) == 0) return "Uncertain";
            else if ((status & 224) == 0) return "Indispensable";
            else if ((status & 208) == 0) return "First team";
            else if ((status & 192) == 0) return "Squad rotation";
            else if ((status & 176) == 0) return "Backup";
            else if ((status & 160) == 0) return "Hot prospect";
            else if ((status & 144) == 0) return "Decent young";
            else if ((status & 128) == 0) return "Not needed";
            else if ((status & 112) == 0) return "On trial";
            else return $"{status}UnkownSquadStatus";
        }
    }
}
