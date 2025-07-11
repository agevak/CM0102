using System;
using System.Collections.Generic;
using CM.Save;
using CM.Save.Model;

namespace CM.Model
{
    public class CMDB
    {
        public string Filename { get; set; }
        public List<TStaff> Staffs { get; set; }
        public List<TPlayer> Players { get; set; }
        public List<TNation> Nations { get; set; }
        public List<TClub> Clubs { get; set; }
        public List<TComp> ClubCompetitions { get; set; }
        public List<TNames> FirstNames { get; set; }
        public List<TNames> SecondNames { get; set; }
        public List<TNames> CommonNames { get; set; }
        public List<TContract> Contracts { get; set; }
        public DateTime GameDate { get; set; }
        public TClub HumanClub { get; set; }
        public IList<EstimatedPlayer> EstimatedPlayers { get; set; }

        public IList<IList<TContract>> ContractsByStaffId { get; set; } = new List<IList<TContract>>();

        public void Initialize()
        {
            ContractsByStaffId.Clear();
            foreach (TStaff staff in Staffs) ContractsByStaffId.Add(new List<TContract>());
            foreach (TContract contract in Contracts) if (contract.ID >= 0) ContractsByStaffId[contract.ID].Add(contract);
            EstimatedPlayers = new List<EstimatedPlayer>();

            // Estimate all players.
            EstimatedPlayers = new List<EstimatedPlayer>();
            foreach (TStaff staff in Staffs)
            {
                if (staff.Player < 0 || staff.Player >= Players.Count) continue;
                TPlayer player = Players[staff.Player];
                EstimatedPlayer estPlayer = CreateEstimatedPlayer(staff);
                estPlayer.Estimate();
                EstimatedPlayers.Add(estPlayer);
            }

        }

        public string GetCurrencyUIString(int val)
        {
            string s = "" + val;
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (i != 0 && (s.Length - i - 1) % 3 == 2) result += " ";
                result += s[i];
            }
            return result;
        }
        public string GetStaffShortName(TStaff staff)
        {
            string result = "";
            if (staff.CommonName >= 0) result = AppendNamePart(result, SaveReader.GetString(CommonNames[staff.CommonName].Name));
            if (!string.IsNullOrWhiteSpace(result)) return result;
            if (staff.SecondName >= 0) result = AppendNamePart(result, SaveReader.GetString(SecondNames[staff.SecondName].Name));
            if (staff.FirstName >= 0) result = AppendNamePart(result, SaveReader.GetString(FirstNames[staff.FirstName].Name), ", ");
            return result;
        }
        public string GetStaffFullName(TStaff staff)
        {
            string result = "";
            if (staff.FirstName >= 0) result = AppendNamePart(result, SaveReader.GetString(FirstNames[staff.FirstName].Name));
            if (staff.SecondName >= 0) result = AppendNamePart(result, SaveReader.GetString(SecondNames[staff.SecondName].Name));
            return result;
        }
        private string AppendNamePart(string name, string part, string separator = " ")
        {
            if (string.IsNullOrWhiteSpace(part)) return name;
            if (name.Length > 0 && !name.EndsWith(" ")) name += separator;
            return name + part;
        }
        public string GetNationShortName(int id)
        {
            if (id < 0 || id >= Nations.Count) return $"{id}UnknownNation";
            return SaveReader.GetString(Nations[id].ShortName);
        }
        public string GetClubShortName(int id)
        {
            if (id < 0) return "";
            if (id >= Clubs.Count) return $"{id}UnknownClub";
            return SaveReader.GetString(Clubs[id].ShortName);
        }
        public EstimatedPlayer CreateEstimatedPlayer(TStaff staff)
        {
            TPlayer player = Players[staff.Player];
            return new EstimatedPlayer(staff, player, ContractsByStaffId[staff.ID])
            {
                ShortName = GetStaffShortName(staff),
                FullName = GetStaffFullName(staff),
                NationShortName = GetNationShortName(staff.Nation),
                ClubShortName = GetClubShortName(staff.ClubJob)
            };
        }
    }
}
