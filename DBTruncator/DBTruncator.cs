using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CM.Save;
using CM.Save.Model;

namespace CM
{
    public class DBTruncator
    {
        private const string INPUT_FOLDER = "Input";
        private const string OUTPUT_FOLDER = "Output";

        private readonly SaveReader saveReader = new SaveReader();
        private readonly IDictionary<int, int> idMapping = new Dictionary<int, int>();

        public static void Main(string[] args)
        {
            new DBTruncator().Start();
        }

        private void Start()
        {
            // Load .sav file.
            string savInputFolder = Path.Combine(INPUT_FOLDER, "Save");
            IList<string> savFilenames = new List<string>();
            if (Directory.Exists(savInputFolder)) savFilenames = Directory.GetFiles(savInputFolder, "*.sav");
            if (savFilenames.Count != 1) FinishWithError($"{savInputFolder} folder must contain exactly 1 .sav file.");
            string savFilename = savFilenames.First();
            try
            {
                saveReader.Load(savFilename, true);
            }
            catch (Exception e)
            {
                FinishWithError($"Failed to load and parse {savFilename}", e);
            }
            Console.WriteLine($"Using save file {savFilename}");

            // Load DB.
            string dbInputFolder = Path.Combine(INPUT_FOLDER, "Data");
            byte[] indexData, clubData, natClubData, nationData, staffFileContent;
            string dbFilename = null;
            try
            {
                dbFilename = Path.Combine(dbInputFolder, "index.dat");
                indexData = File.ReadAllBytes(dbFilename);
                dbFilename = Path.Combine(dbInputFolder, "club.dat");
                clubData = File.ReadAllBytes(dbFilename);
                dbFilename = Path.Combine(dbInputFolder, "nat_club.dat");
                natClubData = File.ReadAllBytes(dbFilename);
                dbFilename = Path.Combine(dbInputFolder, "nation.dat");
                nationData = File.ReadAllBytes(dbFilename);
                dbFilename = Path.Combine(dbInputFolder, "staff.dat");
                staffFileContent = File.ReadAllBytes(dbFilename);
                //byte[] staffHistoryData = File.ReadAllBytes("staff_history.dat");
                //byte[] staffCompHistoryData = File.ReadAllBytes("staff_comp_history.dat");
            }
            catch (Exception e)
            {
                FinishWithError($"Failed to load DB file {dbFilename}", e);
                return;
            }

            // Extract blocks from DB.
            List<TIndex> indexList = saveReader.BlockToObjects<TIndex>(indexData.ToList().GetRange(8, indexData.Length - 8).ToArray());
            List<string> indexListNames = indexList.Select(x => SaveReader.GetString(x.Name)).ToList();
            TIndex staffBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff.dat" && x.FileType == 6);
            TIndex nonPlayersBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff.dat" && x.FileType == 9);
            TIndex playersBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff.dat" && x.FileType == 10);
            TIndex staffPreferenceBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff.dat" && x.FileType == 22);
            TIndex clubBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "club.dat");
            TIndex nationBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "nation.dat");
            TIndex staffHistoryBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff_history.dat");
            TIndex staffCompHistoryBlockIndex = indexList.FirstOrDefault(x => SaveReader.GetString(x.Name) == "staff_comp_history.dat");
            byte[] staffData = new byte[staffBlockIndex.Count * Marshal.SizeOf(typeof(TStaff))];
            byte[] nonPlayersData = new byte[nonPlayersBlockIndex.Count * Marshal.SizeOf(typeof(TNonPlayer))];
            byte[] playersData = new byte[playersBlockIndex.Count * Marshal.SizeOf(typeof(TPlayer))];
            byte[] staffPreferencesData = new byte[staffPreferenceBlockIndex.Count * Marshal.SizeOf(typeof(TPreferences))];
            Array.Copy(staffFileContent, staffBlockIndex.Offset, staffData, 0, staffData.Length);
            Array.Copy(staffFileContent, nonPlayersBlockIndex.Offset, nonPlayersData, 0, nonPlayersData.Length);
            Array.Copy(staffFileContent, playersBlockIndex.Offset, playersData, 0, playersData.Length);
            Array.Copy(staffFileContent, staffPreferenceBlockIndex.Offset, staffPreferencesData, 0, staffPreferencesData.Length);

            // Parse DB blocks.
            List<TStaff> staff = saveReader.BlockToObjects<TStaff>(staffData);
            List<TNonPlayer> nonPlayers = saveReader.BlockToObjects<TNonPlayer>(nonPlayersData);
            List<TPlayer> players = saveReader.BlockToObjects<TPlayer>(playersData);
            List<TPreferences> staffPreferences = saveReader.BlockToObjects<TPreferences>(staffPreferencesData);
            List<TClub> clubs = saveReader.BlockToObjects<TClub>(clubData);
            List<TClub> natClubs = saveReader.BlockToObjects<TClub>(natClubData);
            List<TNation> nations = saveReader.BlockToObjects<TNation>(nationData);
            List<TComp> clubComps = saveReader.BlockToObjects<TComp>("club_comp.dat");
            //List<TStaffHistory> staffHistories = saveReader.BlockToObjects<TStaffHistory>(staffHistoryData);
            //List<TStaffCompHistory> staffCompHistories = saveReader.BlockToObjects<TStaffCompHistory>(staffCompHistoryData);
            Console.WriteLine($"Found {staff.Count} staff ({staff.Where(x => x.Player >= 0).Count()} players) before truncate.");
            List<string> nationNames = nations.Select(x => SaveReader.GetString(x.Name)).ToList();

            // Determine human club name.
            string humanClubName = GetHumanClubName();
            Console.WriteLine($"Detected human club name: {humanClubName}");
            TClub humanClub = clubs.FirstOrDefault(x => SaveReader.GetString(x.ShortName) == humanClubName);
            Console.WriteLine($"Found human club ID: {humanClub.ID}");

            // Determine division and its clubs.
            List<TClub> divisionClubs = clubs.Where(x => x.Division == humanClub.Division).ToList();
            //List<TClub> divisionClubs = clubs.Where(x => x.Nation == humanClub.Nation).ToList();
            HashSet<int> divisionClubIds = new HashSet<int>(divisionClubs.Select(x => x.ID));
            TComp humanClubComp = clubComps.FirstOrDefault(x => x.ID == humanClub.Division);
            Console.WriteLine($"Human club's nation: {humanClub.Nation}, division: {humanClub.Division} {SaveReader.GetString(humanClubComp.ShortName)}, {divisionClubs.Count} clubs.");

            // Truncate staff.
            Random rnd = new Random();
            List<TStaff> divisionStaff = staff.Where(x => divisionClubIds.Contains(x.ClubJob)).ToList();
            Console.WriteLine($"{divisionStaff.Count} staff ({divisionStaff.Where(x => x.Player >= 0).Count()} players) in the human club's division.");
            IList<TStaff> extraDivisionStuff = staff.Where(x => !divisionClubIds.Contains(x.ClubJob) && (x.Nation == humanClub.Nation & x.ID % 100 < 5)).ToList();
            Console.WriteLine($"Adding extra {extraDivisionStuff.Count} staff ({extraDivisionStuff.Where(x => x.Player >= 0).Count()} players).");
            List<TStaff> remainingStaff = divisionStaff.Concat(extraDivisionStuff).ToList();

            // TODO
            /*var list = staff.Where(x => x.ClubJob < 0 && x.Player >= 0).ToList();
            remainingStaff = staff.ToList();*/

            Console.WriteLine($"Total kept staff: {remainingStaff.Count} ({remainingStaff.Where(x => x.Player >= 0).Count()} players).");

            // Reassing record IDs.
            IDictionary<int, int> nonPlayerIdMapping = new Dictionary<int, int>();
            IDictionary<int, int> playerIdMapping = new Dictionary<int, int>();
            IDictionary<int, int> staffPreferencesIdMapping = new Dictionary<int, int>();
            idMapping.Clear();
            for (int newId = 0; newId < remainingStaff.Count; newId++) // Staff.
            {
                TStaff curStaff = remainingStaff[newId];
                idMapping[curStaff.ID] = newId;
                nonPlayerIdMapping[curStaff.NonPlayer] = playerIdMapping[curStaff.Player] = staffPreferencesIdMapping[curStaff.StaffPreferences] = -1;
                curStaff.ID = newId;

                // TODO
                //curStaff.IntApps = curStaff.IntGoals = 0; // Clear internation caps, so the game will decide itself if to load this staff or not.
                if (curStaff.ClubJob < 0 && curStaff.IntApps <= 0) curStaff.IntApps = 1; // Force loading many free agents.
            }
            nonPlayerIdMapping.Remove(-1);
            playerIdMapping.Remove(-1);
            staffPreferencesIdMapping.Remove(-1);
            List<TNonPlayer> remainingNonPlayers = nonPlayers.Where(x => nonPlayerIdMapping.ContainsKey(x.ID)).ToList();
            for (int newId = 0; newId < remainingNonPlayers.Count; newId++) // Non-players.
            {
                nonPlayerIdMapping[remainingNonPlayers[newId].ID] = newId;
                remainingNonPlayers[newId].ID = newId;
            }
            List<TPlayer> remainingPlayers = players.Where(x => playerIdMapping.ContainsKey(x.ID)).ToList();
            for (int newId = 0; newId < remainingPlayers.Count; newId++) // Players.
            {
                playerIdMapping[remainingPlayers[newId].ID] = newId;
                remainingPlayers[newId].ID = newId;
            }
            List<TPreferences> remainingStaffPreferences = staffPreferences.Where(x => idMapping.ContainsKey(x.StaffPreferencesID)).ToList();
            for (int newId = 0; newId < remainingStaffPreferences.Count; newId++) // Staff preferences.
            {
                TPreferences staffPref = remainingStaffPreferences[newId];
                staffPreferencesIdMapping[staffPref.StaffPreferencesID] = newId;
                UpdateId(ref staffPref.StaffFavouriteStaff1);
                UpdateId(ref staffPref.StaffFavouriteStaff2);
                UpdateId(ref staffPref.StaffFavouriteStaff3);
                UpdateId(ref staffPref.StaffDislikedStaff1);
                UpdateId(ref staffPref.StaffDislikedStaff2);
                UpdateId(ref staffPref.StaffDislikedStaff3);
                staffPref.StaffPreferencesID = newId;
            }
            foreach (TStaff curStaff in remainingStaff) // Staff foreign keys.
            {
                UpdateId(ref curStaff.NonPlayer, nonPlayerIdMapping);
                UpdateId(ref curStaff.Player, playerIdMapping);
                UpdateId(ref curStaff.StaffPreferences, staffPreferencesIdMapping);
            }
            List<TStaffHistory> remainingStaffHistories = new List<TStaffHistory>();
            List<TStaffCompHistory> remainingStaffCompHistories = new List<TStaffCompHistory>();
            /*List<TStaffHistory> remainingStaffHistories = staffHistories.Where(x => idMapping.ContainsKey(x.StaffID)).ToList();
            remainingStaffHistories.Clear();
            for (int newId = 0; newId < remainingStaffHistories.Count; newId++)
            {
                TStaffHistory staffHistory = remainingStaffHistories[newId];
                UpdateId(ref staffHistory.StaffID);
                staffHistory.ID = newId;
            }
            List<TStaffCompHistory> remainingStaffCompHistories = staffCompHistories.Where(x => x.WinnerID == -1 && x.RunnersUpID == -1 && x.ThirdPlaceID == -1).ToList();
            remainingStaffCompHistories.Clear();
            for (int newId = 0; newId < remainingStaffCompHistories.Count; newId++)
            {
                TStaffCompHistory staffCompHistory = remainingStaffCompHistories[newId];
                staffCompHistory.ID = newId;
            }*/
            foreach (TClub club in clubs.Concat(natClubs)) // Clubs.
            {
                UpdateId(ref club.AssistantManager);
                UpdateId(ref club.Chairman);
                UpdateIds(club.Coaches);
                UpdateIds(club.Directors);
                UpdateId(ref club.DisStaff1);
                UpdateId(ref club.DisStaff2);
                UpdateId(ref club.DisStaff3);
                UpdateId(ref club.FavStaff1);
                UpdateId(ref club.FavStaff2);
                UpdateId(ref club.FavStaff3);
                UpdateId(ref club.Manager);
                UpdateIds(club.Physios);
                UpdateIds(club.Scouts);
                UpdateIds(club.Squad);
                UpdateIds(club.TeamSelected);
            }

            // Fix nations.
            IDictionary<int, int> nationStaffCounts = new Dictionary<int, int>();
            foreach (TStaff curStaff in remainingStaff)
            {
                if (!nationStaffCounts.ContainsKey(curStaff.Nation)) nationStaffCounts[curStaff.Nation] = 1;
                else nationStaffCounts[curStaff.Nation]++;
            }
            foreach (TNation nation in nations)
                if (nationStaffCounts.ContainsKey(nation.ID))
                    nation.NumberStaff = nationStaffCounts[nation.ID];
                else nation.NumberStaff = 0;

            // Build new DB blocks.
            byte[] newStaffData = saveReader.ObjectsToBlock(remainingStaff);
            byte[] newNonPlayersData = saveReader.ObjectsToBlock(remainingNonPlayers);
            byte[] newPlayersData = saveReader.ObjectsToBlock(remainingPlayers);
            byte[] newStaffPreferencesData = saveReader.ObjectsToBlock(remainingStaffPreferences);
            byte[] newStaffFileContent = new byte[newStaffData.Length + newNonPlayersData.Length + newPlayersData.Length + newStaffPreferencesData.Length];
            staffBlockIndex.Count = remainingStaff.Count;
            nonPlayersBlockIndex.Offset = newStaffData.Length;
            nonPlayersBlockIndex.Count = remainingNonPlayers.Count;
            playersBlockIndex.Offset = nonPlayersBlockIndex.Offset + newNonPlayersData.Length;
            playersBlockIndex.Count = remainingPlayers.Count;
            staffPreferenceBlockIndex.Offset = playersBlockIndex.Offset + newPlayersData.Length;
            staffPreferenceBlockIndex.Count = remainingStaffPreferences.Count;
            Array.Copy(newStaffData, 0, newStaffFileContent, staffBlockIndex.Offset, newStaffData.Length);
            Array.Copy(newNonPlayersData, 0, newStaffFileContent, nonPlayersBlockIndex.Offset, newNonPlayersData.Length);
            Array.Copy(newPlayersData, 0, newStaffFileContent, playersBlockIndex.Offset, newPlayersData.Length);
            Array.Copy(newStaffPreferencesData, 0, newStaffFileContent, staffPreferenceBlockIndex.Offset, newStaffPreferencesData.Length);
            byte[] newClubData = saveReader.ObjectsToBlock(clubs);
            byte[] newNatClubData = saveReader.ObjectsToBlock(natClubs);
            byte[] newNationData = saveReader.ObjectsToBlock(nations);
            byte[] newStaffHistoryData = saveReader.ObjectsToBlock(remainingStaffHistories);
            staffHistoryBlockIndex.Count = remainingStaffHistories.Count;
            byte[] newStaffCompHistoryData = saveReader.ObjectsToBlock(remainingStaffCompHistories);
            staffCompHistoryBlockIndex.Count = remainingStaffCompHistories.Count;
            byte[] newIndexData = new byte[indexData.Length];
            Array.Copy(saveReader.ObjectsToBlock(indexList), 0, newIndexData, 8, indexData.Length - 8);

            // Save new DB.
            string dbOutputFolder = Path.Combine(OUTPUT_FOLDER, "Data");
            try { Directory.CreateDirectory(dbOutputFolder); } catch { }
            try
            {
                dbFilename = Path.Combine(dbOutputFolder, "index.dat");
                File.WriteAllBytes(dbFilename, newIndexData);
                dbFilename = Path.Combine(dbOutputFolder, "staff.dat");
                File.WriteAllBytes(dbFilename, newStaffFileContent);
                dbFilename = Path.Combine(dbOutputFolder, "club.dat");
                File.WriteAllBytes(dbFilename, newClubData);
                dbFilename = Path.Combine(dbOutputFolder, "nat_club.dat");
                File.WriteAllBytes(dbFilename, newNatClubData);
                dbFilename = Path.Combine(dbOutputFolder, "nation.dat");
                File.WriteAllBytes(dbFilename, newNationData);
                dbFilename = Path.Combine(dbOutputFolder, "staff_history.dat");
                File.WriteAllBytes(dbFilename, newStaffHistoryData);
                dbFilename = Path.Combine(dbOutputFolder, "staff_comp_history.dat");
                //File.WriteAllBytes("staff_comp_history.new", newStaffCompHistoryData);
                File.WriteAllBytes(dbFilename, newStaffCompHistoryData);
            }
            catch (Exception e)
            {
                FinishWithError($"Failed to write DB file {dbFilename}", e);
            }

            Console.WriteLine("Finished succesfully.");
        }

        private void FinishWithError(string message, Exception e = null)
        {
            Console.WriteLine(message);
            if (e != null) Console.WriteLine("\n" + e);
            Environment.Exit(1);
        }

        private void UpdateId(ref int id, IDictionary<int, int> mapping = null)
        {
            mapping = mapping ?? idMapping;
            if (mapping.ContainsKey(id)) id = mapping[id];
            else id = -1;
        }
        private void UpdateIds(int[] ids, IDictionary<int, int> mapping = null)
        {
            for (int i = 0; i < ids.Length; i++) UpdateId(ref ids[i], mapping);
        }

        private string GetHumanClubName()
        {
            Block generalBlock = saveReader.FindBlock("general.dat");
            string humanClubName = SaveReader.GetString(generalBlock.dataBuffer, 0x824);
            return humanClubName;
        }
    }
}