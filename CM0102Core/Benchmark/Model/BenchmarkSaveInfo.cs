using System.Collections.Generic;
using System;
using CM.Save;
using System.Linq;
using CM.Save.Model;

namespace CM.Benchmark.Model
{
    public class BenchmarkSaveInfo
    {
        public string HumanClubName { get; set; }
        public int HumanClubId { get; set; }
        public int DivisionId { get; set; }
        
        public TClub HumanClub { get; set; }
        public List<TClub> DivClubs { get; set; }
        public List<string> DivClubNames { get; set; }

        public static BenchmarkSaveInfo Load(SaveReader saveReader)
        {
            // Extract blocks.
            List<TClub> clubs = saveReader.BlockToObjects<TClub>("club.dat");

            // Detect human club and league.
            BenchmarkSaveInfo result = new BenchmarkSaveInfo();
            TClub humanClub = saveReader.GetHumanClub();
            result.HumanClubName = SaveReader.GetString(humanClub.ShortName);
            if (humanClub == null) throw new Exception($"Human club not found by name: {result.HumanClubName}");
            result.HumanClubId = humanClub.ID;
            result.DivisionId = humanClub.Division;

            result.HumanClub = humanClub;
            result.DivClubs = clubs.Where(x => x.Division == humanClub.Division).ToList();
            result.DivClubNames = result.DivClubs.Select(x => SaveReader.GetString(x.ShortName)).ToList();

            return result;
        }
    }
}
