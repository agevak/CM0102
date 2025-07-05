namespace CM.Model
{
    public static class ContractExtensions
    {
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
