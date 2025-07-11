using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using CM.Benchmark;
using CM.Benchmark.Model;
using CM.Model;
using CM.Save;
using CM.Save.Model;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace CM
{
    public class Benchmarker
    {
        private const string SAV_FOLDER = "Saves";
        private const string RESULTS_FOLDER = "Results";
        private const string CM_EXE_FILENAME = @"Template\cm0102_bm.exe";

        private static readonly string CM_FOLDER = ConfigurationManager.AppSettings["CMFolder"];
        private static readonly int SEASONS_PER_TEST = int.Parse(ConfigurationManager.AppSettings["SeasonsPerTest"]);
        private static readonly int MAX_THREADS = int.Parse(ConfigurationManager.AppSettings["MaxThreads"]);
        private static readonly int TEST_TIMEOUT_SEC = int.Parse(ConfigurationManager.AppSettings["TestTimeoutS"]);
        private static readonly bool HIDE_WINDOW = bool.Parse(ConfigurationManager.AppSettings["HideWindow"]);
        private static readonly bool UPDATE_ATTRIBUTES_FOR_CONSISTENCY = bool.Parse(ConfigurationManager.AppSettings["UpdateAttributesForConsistency"]);

        private readonly object lockObj = new object();
        private int exitFlag;
        private Tactic aiTactic;
        private readonly IDictionary<string, int> runningThreadsDict = new Dictionary<string, int>();
        private readonly IDictionary<string, IList<SingleSeasonBenchmarkDivisionResult>> testResults = new Dictionary<string, IList<SingleSeasonBenchmarkDivisionResult>>();
        private readonly IDictionary<string, NamedBenchmarkerRecord> recordsDict = new Dictionary<string, NamedBenchmarkerRecord>();
        private IList<Tuple<CMBenchmarker, string>> benchmarkers = new List<Tuple<CMBenchmarker, string>>();
        private readonly IDictionary<string, BenchmarkSaveInfo> bmSaveInfos = new Dictionary<string, BenchmarkSaveInfo>();
        private readonly Queue<int> freeBmIds = new Queue<int>();
        private readonly ILog log = LogManager.GetLogger(typeof(Benchmarker));

        public static void Main(string[] args)
        {
            new Benchmarker().Start();
        }

        private void Start()
        {
            // Load records from persistence.
            IList<NamedBenchmarkerRecord> records = new List<NamedBenchmarkerRecord>();
            string resFilename = Path.Combine(RESULTS_FOLDER, "repository.csv");
            if (File.Exists(resFilename))
            {
                records = File.ReadAllLines(resFilename).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => NamedBenchmarkerRecord.FromString(x)).ToList();
            }
            foreach (string name in Directory.GetFiles(SAV_FOLDER, "*.sav").Select(x => Path.GetFileNameWithoutExtension(x)))
            {
                runningThreadsDict[name] = 0;
                testResults[name] = new List<SingleSeasonBenchmarkDivisionResult>();
                recordsDict[name] = new NamedBenchmarkerRecord(name);
            }
            foreach (NamedBenchmarkerRecord record in records) recordsDict[record.Name] = record;

            // Load orig .sav files and tactics.
            LoadSaveInfos();
            LoadAITactics();

            // Run exit flag thread in background.
            Thread exitThread = new Thread(() =>
            {
                string cmd = Console.ReadLine();
                cmd = "a";
                Console.WriteLine("Raising exit flag.");
                lock (lockObj) exitFlag = cmd == "a" ? 2 : 1;
                Console.WriteLine(string.Format("Exit flag raised. Waiting for benchmarkers to {0}.", exitFlag == 2 ? "cancel" : "finish"));
            });
            exitThread.Start();

            DateTime lastPrintProgressDt = DateTime.MinValue;
            for (int id = 1; id <= MAX_THREADS; id++) freeBmIds.Enqueue(id);
            while (true)
            {
                lock (lockObj)
                {
                    // Print progress.
                    if (DateTime.Now - lastPrintProgressDt >= TimeSpan.FromSeconds(1))
                    {
                        //Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        foreach (KeyValuePair<string, IList<SingleSeasonBenchmarkDivisionResult>> kvp in testResults.Where(x => !recordsDict.ContainsKey(x.Key)))
                            Console.WriteLine($"{kvp.Key} {kvp.Value.Count} / {SEASONS_PER_TEST}    ");
                        Console.WriteLine($"Running {benchmarkers.Count}    ");
                        lastPrintProgressDt = DateTime.Now;
                    }

                    // Remove finished BMs.
                    bool shouldPersist = false;
                    IList<Tuple<CMBenchmarker, string>> remainingBenchmarkers = new List<Tuple<CMBenchmarker, string>>();
                    foreach (Tuple<CMBenchmarker, string> tuple in benchmarkers)
                    {
                        CMBenchmarker benchmarker = tuple.Item1;
                        if (!benchmarker.IsDone())
                        {
                            remainingBenchmarkers.Add(tuple);
                            continue;
                        }
                        log.DebugFormat("Returning BM Id {0} to queue.", benchmarker.Id);
                        freeBmIds.Enqueue(benchmarker.Id);
                        string name = tuple.Item2;
                        if (exitFlag == 0)
                        {
                            runningThreadsDict[name]--;
                            IList<SingleSeasonBenchmarkDivisionResult> seasons = testResults[name];
                            seasons.Add(benchmarker.DivisionResult);
                            if (seasons.Count >= SEASONS_PER_TEST)
                            {
                                BenchmarkSaveInfo savInfo = bmSaveInfos[name];
                                NamedBenchmarkerRecord record = recordsDict[name];
                                record.Seasons = SEASONS_PER_TEST;
                                record.Scored = (float)seasons.Average(x => x.ClubResults[savInfo.HumanClubId].Scored) / benchmarker.BmResult.Matches;
                                record.Conceded = (float)seasons.Average(x => x.ClubResults[savInfo.HumanClubId].Conceded) / benchmarker.BmResult.Matches;
                                record.Points = (float)seasons.Average(x => x.ClubResults[savInfo.HumanClubId].Points) / benchmarker.BmResult.Matches;
                                record.Place = (float)seasons.Average(x => x.ClubResults[savInfo.HumanClubId].Place);
                                log.InfoFormat("Finished testing {0}. Played {1} seasons.", record.Name, seasons.Count);
                                shouldPersist = true;
                            }
                        }
                    }
                    if (shouldPersist) PersistRecords();
                    benchmarkers = remainingBenchmarkers;

                    // Check exit flag.
                    if (exitFlag > 0)
                    {
                        if (!benchmarkers.Any())
                        {
                            Console.WriteLine("Finished by user request.");
                            break;
                        }
                        if (exitFlag == 2) foreach (Tuple<CMBenchmarker, string> tuple in benchmarkers) tuple.Item1.Cancel();
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        continue;
                    }

                    // Run new BMs.
                    TimeSpan pollingDelay = TimeSpan.FromMilliseconds(100);
                    while (benchmarkers.Count < MAX_THREADS)
                    {
                        // Try to use already started tests. If none, try to generate new test.
                        NamedBenchmarkerRecord record = GenerateExistingTest() ?? GenerateNewTest();
                        if (record == null)
                        {
                            pollingDelay = TimeSpan.FromMilliseconds(1000);
                            break;
                        }

                        // Geneate .sav file.
                        string savFilename = GenerateSavFile(record);

                        // Run new BM.
                        CMBenchmarker benchmarker = new CMBenchmarker()
                        {
                            CmFolder = CM_FOLDER,
                            CmExeFilename = CM_EXE_FILENAME,
                            TimeoutSec = TEST_TIMEOUT_SEC,
                            SavFilename = savFilename,
                            SaveInfo = bmSaveInfos[record.Name],
                            HideWindow = HIDE_WINDOW
                        };
                        benchmarker.Id = freeBmIds.Dequeue();
                        log.DebugFormat("Took BM Id {0} from queue.", benchmarker.Id);
                        runningThreadsDict[record.Name]++;
                        benchmarkers.Add(new Tuple<CMBenchmarker, string>(benchmarker, record.Name));
                        Thread thread = new Thread(() => benchmarker.Start());
                        thread.Start();
                    }
                    if (!benchmarkers.Any())
                    {
                        Console.WriteLine("All tests done.");
                        try { exitThread.Abort(); } catch { }
                        break;
                    }

                    // Make polling delay.
                    Thread.Sleep(pollingDelay);
                }
            }
            Environment.Exit(0);
        }

        private void PersistRecords()
        {
            // Save repository.
            string folder = RESULTS_FOLDER;
            Directory.CreateDirectory(folder);
            File.WriteAllLines(Path.Combine(folder, "repository.csv"), recordsDict.Values.Where(x => x.Seasons > 0).OrderBy(x => x.Name).Select(x => x.ToString()));

            // Save every club results.
            foreach (string name in testResults.Keys)
                if (testResults[name].Any())
                {
                    // Build division club ID - name mapping.
                    IDictionary<int, string> clubNameById = new Dictionary<int, string>();
                    foreach (TClub club in bmSaveInfos[name].DivClubs) clubNameById[club.ID] = SaveReader.GetString(club.ShortName);

                    // Aggregate each club results.
                    int matchesPerSeason = testResults[name].First().ClubResults.Values.First().Matches;
                    IDictionary<int, NamedBenchmarkerRecord> recordByClub = new Dictionary<int, NamedBenchmarkerRecord>();
                    foreach (SingleSeasonBenchmarkDivisionResult season in testResults[name])
                    {
                        foreach (int clubId in season.ClubResults.Keys)
                        {
                            SingleSeasonBenchmarkResult clubSeasonResult = season.ClubResults[clubId];
                            if (!recordByClub.ContainsKey(clubId)) recordByClub[clubId] = new NamedBenchmarkerRecord(clubNameById[clubId]);
                            NamedBenchmarkerRecord clubRecord = recordByClub[clubId];
                            clubRecord.Seasons++;
                            clubRecord.Scored += clubSeasonResult.Scored;
                            clubRecord.Conceded += clubSeasonResult.Conceded;
                            clubRecord.Points += clubSeasonResult.Points;
                            clubRecord.Place += clubSeasonResult.Place;
                        }
                    }
                    foreach (int clubId in recordByClub.Keys)
                    {
                        NamedBenchmarkerRecord clubRecord = recordByClub[clubId];
                        clubRecord.Scored /= clubRecord.Seasons;// * matchesPerSeason;
                        clubRecord.Conceded /= clubRecord.Seasons;// * matchesPerSeason;
                        clubRecord.Points /= clubRecord.Seasons;// * matchesPerSeason;
                        clubRecord.Place /= clubRecord.Seasons;// * matchesPerSeason;
                    }

                    // Save.
                    File.WriteAllLines(Path.Combine(folder, name + ".txt"), recordByClub.Values.OrderBy(x => x.Name).Select(x => x.ToString()), SaveReader.ENCODING);
                }
        }

        private void LoadSaveInfos()
        {
            foreach (string name in recordsDict.Keys) if (recordsDict[name].Seasons == 0)
            {
                string savFilename = Path.Combine(SAV_FOLDER, name + ".sav");
                SaveReader saveReader = new SaveReader();
                saveReader.Load(savFilename, false);
                bmSaveInfos[name] = BenchmarkSaveInfo.Load(saveReader);
            }
        }

        private void LoadAITactics()
        {
            string folder = "AITactics";
            string filename = Path.Combine(folder, "ai.pct");
            if (File.Exists(filename)) aiTactic = Tactic.FromFile("ai", File.ReadAllBytes(filename));
        }

        private NamedBenchmarkerRecord GenerateExistingTest()
        {
            foreach (string name in runningThreadsDict.Keys)
                if (recordsDict[name].Seasons < SEASONS_PER_TEST && runningThreadsDict[name] > 0 && testResults[name].Count + runningThreadsDict[name] < SEASONS_PER_TEST)
                    return recordsDict[name];
            return null;
        }

        private NamedBenchmarkerRecord GenerateNewTest()
        {
            foreach (string name in recordsDict.Keys)
                if (recordsDict[name].Seasons == 0 && runningThreadsDict[name] <= 0 && !testResults[name].Any())
                    return recordsDict[name];
            return null;
        }

        private string GenerateSavFile(NamedBenchmarkerRecord record)
        {
            string filename = $@"Temp\\Saves\\{record.Name}.sav";
            if (File.Exists(filename)) return filename;
            log.DebugFormat("Begin generating {0}", filename);
            Directory.CreateDirectory(Path.GetDirectoryName(filename));

            // Update tactics.
            SaveReader saveReader = new SaveReader();
            saveReader.Load(Path.Combine(SAV_FOLDER, record.Name + ".sav"), false);
            if (aiTactic != null) saveReader.ReplaceAITacticsWithSameOne(aiTactic);

            // Update attributes for all players.
            List<TStaff> staffs = saveReader.BlockToObjects<TStaff>("staff.dat");
            List<TPlayer> players = saveReader.BlockToObjects<TPlayer>("player.dat");
            for (int i = 0; i < staffs.Count; i++) if (staffs[i].ID != i)
                {
                    Console.WriteLine("Staff IDs are not sequential.");
                    Environment.Exit(1);
                }
            for (int i = 0; i < players.Count; i++) if (players[i].ID != i)
                {
                    Console.WriteLine("Player IDs are not sequential.");
                    Environment.Exit(1);
                }
            if (UPDATE_ATTRIBUTES_FOR_CONSISTENCY)
                foreach (TPlayer player in players)
                {
                    player.InjuryProneness = 1;
                    player.Dirtiness = 1;
                    player.NaturalFitness = 20;
                    player.Consistency = 20;
                    player.ImportantMatches = 20;
                }
            foreach (TStaff staff in staffs)
            {
                staff.YearOfBirth = 25;
            }
            saveReader.ObjectsToBlock("player.dat", players);
            saveReader.ObjectsToBlock("staff.dat", staffs);

            // Save generated .sav file.
            saveReader.Write(filename, false);
            log.DebugFormat("Done generating {0}", filename);
            return filename;
        }
    }
}