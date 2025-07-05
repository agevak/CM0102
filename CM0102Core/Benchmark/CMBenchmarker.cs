using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using CM.Benchmark.Model;
using CM.Save;
using CM.Save.Model;
using log4net;

namespace CM.Benchmark
{
    public class CMBenchmarker
    {
        private static readonly string TMP_FOLDER = "Temp";
        private static readonly string TEMPLATE_FOLDER = "Template";
        private static readonly string BENCHRESULT_FILENAME = "b.txt";
        private static readonly ILog LOG = LogManager.GetLogger(typeof(CMBenchmarker));

        public int Id { get; set; }
        public string CmFolder { get; set; }
        public string CmExeFilename { get; set; }
        public string SavFilename { get; set; }
        public int TimeoutSec { get; set; }
        public bool HideWindow { get; set; }
        public BenchmarkSaveInfo SaveInfo { get; set; }
        public SingleSeasonBenchmarkResult BmResult { get; private set; }
        public SingleSeasonBenchmarkDivisionResult DivisionResult { get; private set; }

        private string cmInstanceFolder;
        public Process process;
        private readonly object lockObj = new object();
        private bool exitFlag;
        private bool done;

        public CMBenchmarker() { }

        public bool IsDone()
        {
            lock (lockObj) return done;
        }

        public void Cancel()
        {
            lock (lockObj) exitFlag = true;
        }

        public SingleSeasonBenchmarkResult Start()
        {
            // Check input.
            if (string.IsNullOrWhiteSpace(CmFolder)) throw new InvalidOperationException("CmFolder must be not null/empty.");
            if (string.IsNullOrWhiteSpace(CmExeFilename)) throw new InvalidOperationException("CmExeFilename must be not null/empty.");
            if (string.IsNullOrWhiteSpace(SavFilename)) throw new InvalidOperationException("SavFilename must be not null/empty.");
            if (!Directory.Exists(CmFolder)) throw new InvalidOperationException($"{CmFolder} does not exist");
            if (!File.Exists(CmExeFilename)) throw new InvalidOperationException($"{CmExeFilename} does not exist");
            if (!File.Exists(SavFilename)) throw new InvalidOperationException($"{SavFilename} does not exist");
            if (Id <= 0) throw new InvalidOperationException($"Id must be >= 0.");
            if (TimeoutSec <= 0) throw new InvalidOperationException($"TimeoutMs must be >= 0.");

            while (true)
            {
                try
                {
                    StartSingleAttempt();
                    break;
                }
                catch (Exception e)
                {
                    LOG.Error($"Unexpected error in BM Id {Id}.", e);
                    Thread.Sleep(1000);
                }
            }
            lock (lockObj) done = true;
            return BmResult;
        }

        private SingleSeasonBenchmarkResult StartSingleAttempt()
        {
            // Copy CM to temporary folder.
            LOG.DebugFormat("Begin BM Id {0}.", Id);
            cmInstanceFolder = Path.Combine(TMP_FOLDER, "" + Id);
            if (!Directory.Exists(cmInstanceFolder) || !Directory.GetFiles(cmInstanceFolder, "*.exe").Any())
            {
                try
                {
                    Directory.Delete(cmInstanceFolder, true);
                }
                catch (Exception e) { }
                try
                {
                    Directory.CreateDirectory(cmInstanceFolder);
                    string cmDataFolder = Path.Combine(CmFolder, "Data");
                    string instanceDataFolder = Path.Combine(cmInstanceFolder, "Data");
                    Directory.CreateDirectory(instanceDataFolder);
                    foreach (string filename in Directory.GetFiles(cmDataFolder, "*.cfg")) File.Copy(filename, Path.Combine(instanceDataFolder, Path.GetFileName(filename)), true);
                    foreach (string filename in Directory.GetFiles(cmDataFolder, "*.fnt")) File.Copy(filename, Path.Combine(instanceDataFolder, Path.GetFileName(filename)), true);
                    foreach (string filename in Directory.GetFiles(cmDataFolder, "*.ldb")) File.Copy(filename, Path.Combine(instanceDataFolder, Path.GetFileName(filename)), true);
                    foreach (string filename in Directory.GetFiles(cmDataFolder, "*.t2k")) File.Copy(filename, Path.Combine(instanceDataFolder, Path.GetFileName(filename)), true);
                    foreach (string filename in Directory.GetFiles(cmDataFolder, "*.ttf")) File.Copy(filename, Path.Combine(instanceDataFolder, Path.GetFileName(filename)), true);
                    foreach (string filename in Directory.GetFiles(TEMPLATE_FOLDER)) File.Copy(filename, Path.Combine(cmInstanceFolder, Path.GetFileName(filename)), true);
                    File.Copy(CmExeFilename, Path.Combine(cmInstanceFolder, Path.GetFileName(CmExeFilename)), true);
                }
                catch (Exception e)
                {
                    LOG.Error(string.Format("Error preparing temp CM folder for BM Id {0}.", Id), e);
                    throw new Exception("Failed to prepare temporary CM folder.", e);
                }
            }

            // Prepare files.
            string resultFilename = Path.Combine(cmInstanceFolder, BENCHRESULT_FILENAME);
            try
            {
                File.Copy(Path.Combine(TEMPLATE_FOLDER, "game.cfg"), Path.Combine(cmInstanceFolder, Path.GetFileName("game.cfg")), true);
                foreach (string filename in Directory.GetFiles(cmInstanceFolder, "*.tmp")) File.Delete(filename);
                File.Delete(resultFilename);
            }
            catch (Exception e)
            {
                LOG.Error(string.Format("Error preparing files for BM Id {0}.", Id), e);
                throw new Exception("Failed to prepare files.", e);
            }

            // Start process.
            DateTime startDt = DateTime.Now;
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = Path.GetFullPath(Path.Combine(cmInstanceFolder, Path.GetFileName(CmExeFilename))),
                Arguments = $"-load {Path.GetFullPath(SavFilename)}",
                WorkingDirectory = Path.GetFullPath(cmInstanceFolder),
                UseShellExecute = false
            };
            if (HideWindow)
            {
                //startInfo.CreateNoWindow = true;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            startInfo.EnvironmentVariables["CM3_PREFS"] = Path.GetFullPath(cmInstanceFolder);
            startInfo.EnvironmentVariables["CM3_TEMP"] = Path.GetFullPath(cmInstanceFolder);
            LOG.DebugFormat("Starting process for BM Id {0} SavFilename {1} cmInstanceFolder {2}", Id, Path.GetFileName(SavFilename), cmInstanceFolder);
            process = Process.Start(startInfo);
            try
            {
                Thread.Sleep(400);
                LOG.DebugFormat("BM Id {0} started CM PID {1}.", Id, process.Id);
                InjectMemory();

                while (true)
                {
                    // Check exit flag.
                    lock (lockObj)
                        if (exitFlag)
                        {
                            LOG.DebugFormat("BM Id {0} is killing process {1} as per exit flag.", Id, process.Id);
                            return null;
                        }

                    // Hide window.
                    if (HideWindow && DateTime.Now - startDt < TimeSpan.FromMilliseconds(4000))
                    {
                        MoveWindow(process.MainWindowHandle, 5000, 5000, 0, 0, true);
                        ShowWindow(process.MainWindowHandle, 0);
                        Thread.Sleep(500);
                    }

                    // Check if CM loaded .sav.
                    while (DateTime.Now - startDt > TimeSpan.FromSeconds(30) && !Directory.GetFiles(cmInstanceFolder, $"*.tmp").Any())
                    {
                        LOG.WarnFormat("CM didn't start properly for BM Id {0}.", Id);
                        throw new Exception("CM didn't start properly.");
                    }

                    // Check if CM finished.
                    if (process.HasExited)
                    {
                        // Parse results.
                        LOG.DebugFormat("BM Id {0} results successfully.", Id);
                        Thread.Sleep(1000);
                        DivisionResult = ParseResult(resultFilename);
                        if (DivisionResult == null) throw new Exception($"Bad result file {BENCHRESULT_FILENAME}.");
                        BmResult = DivisionResult.ClubResults[SaveInfo.HumanClubId];
                        LOG.DebugFormat("Parsed BM Id {0} results successfully.", Id);
                        return BmResult;
                    }

                    // Check for time out.
                    if (DateTime.Now - startDt > TimeSpan.FromSeconds(TimeoutSec))
                    {
                        LOG.WarnFormat("CM process time out for BM Id {0}.", Id);
                        throw new Exception("Process timed out.");
                    }

                    // Make polling delay.
                    Thread.Sleep(100);
                }
            }
            finally
            {
                try
                {
                    if (BmResult == null || !process.HasExited) process.Kill();
                }
                catch (Exception e)
                {
                    LOG.Error($"BM Id {Id} failed to kill proces PID {process.Id}.", e);
                }
                Thread.Sleep(1000);
                LOG.DebugFormat("End BM Id {0}.", Id);
            }
        }

        private SingleSeasonBenchmarkDivisionResult ParseResult(string resultFilename)
        {
            SingleSeasonBenchmarkDivisionResult result = new SingleSeasonBenchmarkDivisionResult(SaveInfo.DivClubs.Select(x => x.ID).ToList());
            if (!File.Exists(resultFilename)) return null;
            using (StreamReader reader = new StreamReader(resultFilename, SaveReader.ENCODING))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) && !result.ClubResults.Values.Any(x => x == null)) break;
                    if (line.Length < 40 || !char.IsDigit(line[0])) continue;
                    string clubName = line.Substring(7, 28).Trim();
                    TClub club = SaveInfo.DivClubs.FirstOrDefault(x => SaveReader.GetString(x.ShortName).Trim() == clubName);
                    if (club == null) continue;
                    if (result.ClubResults[club.ID] != null) return null; // More than 1 season in the file.
                    string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int n = tokens.Length;
                    int points = int.Parse(tokens[n - 1]);
                    int goalAg = int.Parse(tokens[n - 2]) + int.Parse(tokens[n - 7]);
                    int goalFor = int.Parse(tokens[n - 3]) + int.Parse(tokens[n - 8]);
                    int played = int.Parse(tokens[n - 12]);
                    string placeStr = tokens[0];
                    for (int i = 0; i < placeStr.Length; i++)
                        if (!char.IsDigit(placeStr[i]))
                        {
                            placeStr = placeStr.Substring(0, i);
                            break;
                        }
                    int place = int.Parse(placeStr);

                    // Add to sums.
                    result.ClubResults[club.ID] = new SingleSeasonBenchmarkResult()
                    {
                        Scored = goalFor,
                        Conceded = goalAg,
                        Points = points,
                        Matches = played,
                        Place = place
                    };
                }
            }
            return result;
        }


        public void InjectMemory()
        {
            // Open process for memory access.
            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms)) bw.Write(SaveInfo.DivisionId);
                byte[] buf = ms.ToArray();
                if (!WriteProcessMemory(processHandle, new IntPtr(0x06036BD), buf, buf.Length, out IntPtr bytesWritten))
                    throw new Exception(string.Format("BM ID {0} failed to inject division ID into CM memory.", Id));
            }
            CloseHandle(processHandle);
            LOG.DebugFormat("BM ID {0} injected division ID {1} into CM memory.", Id, SaveInfo.DivisionId);
        }

        private const int PROCESS_VM_READ = 0x0010;
        private const int PROCESS_VM_WRITE = 0x0020;
        private const int PROCESS_VM_OPERATION = 0x0008;
        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const int MEM_COMMIT = 0x00001000;
        private const int PAGE_READWRITE = 0x04;

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MemoryBasicInformation lpBuffer, uint dwLength);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public uint AllocationProtect;
        public IntPtr RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }
}
