using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CM.Benchmark;
using CM.Benchmark.Model;
using CM.Helpers;
using CM.Model;
using CM.Save;
using CM.Save.Model;
using log4net;

namespace CM
{
    public partial class MainForm : Form
    {
        private const string CONFIG_FILENAME = "config.json";
        private const string TEMP_SAV_FOLDER = @"Temp\Sav";

        private static readonly ILog LOG = LogManager.GetLogger(typeof(MainForm));
        private TacticBmResults bmResults;

        public MainForm()
        {
            InitializeComponent();
        }

        private void SaveUIState()
        {
            // Apply config.
            UIState state;
            try
            {
                state = GetUIState();
            }
            catch (Exception e)
            {
                return;
            }
            string json = Helper.Serialize(state);
            try
            {
                File.WriteAllText(CONFIG_FILENAME, json);
            }
            catch (Exception e)
            {
            }
        }

        private UIState GetUIState()
        {
            UIState state = new UIState()
            {
                AddTacticsFolder = dlgTacticsFolder.SelectedPath,
                AddGameSavesFolder = dlgGamesSavesFolder.SelectedPath,
                CMFolder = txtCmFolder.Text,
                TestCount = int.Parse(txtTestCount.Text),
                ThreadCount = int.Parse(txtThreadCount.Text),
                ThreadTimeoutSec = int.Parse(txtThreadTimeout.Text),
                HideCMWindow = chkHideCMWindow.Checked,
                MainFormLocation = Location,
                MainFormSize = Size,
                SplitterDistance = splitContainer.SplitterDistance,
                WindowState = WindowState,
                HumanTacticsInRows = rdbHumanTacticsInRows.Checked,
                MakePlayersAllPositioners = chkMakePlayersAllPositioners.Checked
            };
            if (!string.IsNullOrWhiteSpace(dlgTacticFiles.FileName)) state.AddTacticFiles = Path.GetDirectoryName(dlgTacticFiles.FileName);
            else state.AddTacticFiles = dlgTacticFiles.InitialDirectory;
            if (!string.IsNullOrWhiteSpace(dlgGameSaveFiles.FileName)) state.AddGameSaveFiles = Path.GetDirectoryName(dlgGameSaveFiles.FileName);
            else state.AddGameSaveFiles = dlgGameSaveFiles.InitialDirectory;
            if (!string.IsNullOrWhiteSpace(dlgSaveResults.FileName)) state.SaveResultsPath = Path.GetDirectoryName(dlgSaveResults.FileName);
            else state.SaveResultsPath = dlgSaveResults.InitialDirectory;
            if (!string.IsNullOrWhiteSpace(dlgLoadResults.FileName)) state.LoadResultsPath = Path.GetDirectoryName(dlgLoadResults.FileName);
            else state.LoadResultsPath = dlgLoadResults.InitialDirectory;
            if (!string.IsNullOrWhiteSpace(dlgExportResultsToHtml.FileName)) state.ExportResultsPath = Path.GetDirectoryName(dlgExportResultsToHtml.FileName);
            else state.ExportResultsPath = dlgExportResultsToHtml.InitialDirectory;
            return state;
        }

        private void LoadUIState()
        {
            // Load config file.
            string json = null;
            try
            {
                json = File.ReadAllText(CONFIG_FILENAME);
            }
            catch (Exception e)
            {
            }

            // Parse config.
            UIState state = new UIState();
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    state = Helper.Deserialize<UIState>(json);
                }
                catch (Exception e)
                {
                }
            }
            if (state == null) return;

            // Apply config.
            dlgTacticFiles.InitialDirectory = state.AddTacticFiles;
            dlgTacticsFolder.SelectedPath = state.AddTacticsFolder;
            dlgGameSaveFiles.InitialDirectory = state.AddGameSaveFiles;
            dlgGamesSavesFolder.SelectedPath = state.AddGameSavesFolder;
            txtCmFolder.Text = dlgCmFolder.SelectedPath = state.CMFolder;
            txtTestCount.Text = "" + state.TestCount;
            txtThreadCount.Text = "" + state.ThreadCount;
            txtThreadTimeout.Text = "" + state.ThreadTimeoutSec;
            chkHideCMWindow.Checked = state.HideCMWindow;
            Location = state.MainFormLocation;
            Size = state.MainFormSize;
            splitContainer.SplitterDistance = state.SplitterDistance;
            WindowState = state.WindowState;
            rdbHumanTacticsInRows.Checked = state.HumanTacticsInRows;
            rdbHumanTacticsInCols.Checked = !state.HumanTacticsInRows;
            chkMakePlayersAllPositioners.Checked = state.MakePlayersAllPositioners;
            dlgSaveResults.InitialDirectory = state.SaveResultsPath;
            dlgLoadResults.InitialDirectory = state.LoadResultsPath;
            dlgExportResultsToHtml.InitialDirectory = state.ExportResultsPath;
        }

        private void AddGameSavesFromFolder(ListBox listBox)
        {
            if (dlgGamesSavesFolder.ShowDialog(this) != DialogResult.OK) return;
            string folder = dlgGamesSavesFolder.SelectedPath;
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(this, $"Folder doesn't exist: {folder}", "Game saves folder doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<string> filenames = Directory.GetFiles(folder).Where(x => Path.GetExtension(x) == ".sav").ToList();
            AddGameSavesFromFiles(filenames);
        }
        private void AddGameSavesFromFiles(IList<string> filenames)
        {
            if (lstGameSaves.Tag == null) lstGameSaves.Tag = new List<string>();
            List<string> tag = (List<string>)lstGameSaves.Tag;
            tag =  tag.Union(filenames).ToList();
            tag = tag.OrderBy(x => x).ToList();
            lstGameSaves.BeginUpdate();
            lstGameSaves.Items.Clear();
            lstGameSaves.Items.AddRange(tag.Select(x => Path.GetFileNameWithoutExtension(x)).ToArray());
            lstGameSaves.Tag = tag;
            lstGameSaves.EndUpdate();
            UpdateListTotal(lstGameSaves);
        }

        private void AddTacticsFromFolder()
        {
            if (dlgTacticsFolder.ShowDialog(this) != DialogResult.OK) return;
            string folder = dlgTacticsFolder.SelectedPath;
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(this, $"Folder doesn't exist: {folder}", "Tactics folder doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<string> filenames = Directory.GetFiles(folder).Where(x => Path.GetExtension(x) == ".tct" || Path.GetExtension(x) == ".pct").ToList();
            AddTacticsFromFiles(filenames);
        }
        private void AddTacticsFromFiles(IList<string> filenames)
        {
            IList<Tactic> tactics = new List<Tactic>();
            IList<string> errorFilenames = new List<string>();
            foreach (string filename in filenames)
            {
                // Load file content.
                byte[] content;
                try
                {
                    content = File.ReadAllBytes(filename);
                }
                catch (Exception e)
                {
                    errorFilenames.Add(filename);
                    continue;
                }

                // Parse it.
                Tactic tactic;
                try
                {
                    tactic = Tactic.FromFile(Path.GetFileNameWithoutExtension(filename), content);
                }
                catch (Exception e)
                {
                    errorFilenames.Add(filename);
                    continue;
                }
                tactics.Add(tactic);
            }

            // Show errors.
            if (errorFilenames.Count > 0) MessageBox.Show(this, $"Failed to load these files:\n{string.Join("\n", errorFilenames)}", "Failed to load tactics files.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // Add loaded tactics to list box.
            AddTacticsList(lstHumanTactics, tactics.OrderBy(x => x.Name).ToList());
        }
        private void AddTacticsList(ListBox view, IList<Tactic> model)
        {
            view.BeginUpdate();
            view.Items.AddRange(model.ToArray());
            view.EndUpdate();
            UpdateListTotal(view);
        }

        private Label GetTotalLabel(ListBox view) => (view == lstHumanTactics) ? lblHumanTacticsTotal : lblGameSavesTotal;

        private void UpdateListTotal(ListBox view)
        {
            Label totalLabel = GetTotalLabel(view);
            totalLabel.Text = "" + view.Items.Count;
        }

        private void DeleteTacticsFromList(bool onlySelected)
        {
            lstHumanTactics.BeginUpdate();
            if (onlySelected)
            {
                HashSet<Tactic> tactics = new HashSet<Tactic>(lstHumanTactics.Items.Cast<Tactic>());
                tactics.ExceptWith(lstHumanTactics.SelectedItems.Cast<Tactic>());
                lstHumanTactics.Items.Clear();
                lstHumanTactics.Items.AddRange(tactics.OrderBy(x => x.Name).ToArray());
            }
            else lstHumanTactics.Items.Clear();
            lstHumanTactics.EndUpdate();
            UpdateListTotal(lstHumanTactics);
        }

        private void DeleteGameSavesFromList(bool onlySelected)
        {
            if (lstGameSaves.Tag == null) lstGameSaves.Tag = new List<string>();
            List<string> tag = (List<string>)lstGameSaves.Tag;

            lstGameSaves.BeginUpdate();
            if (onlySelected)
            {
                IList<int> selectedIndices = lstGameSaves.SelectedIndices.Cast<int>().OrderByDescending(x => x).ToList();
                foreach (int selectedIndex in selectedIndices) tag.RemoveAt(selectedIndex);
                lstGameSaves.Items.Clear();
                lstGameSaves.Items.AddRange(tag.Select(x => Path.GetFileNameWithoutExtension(x)).ToArray());
                lstGameSaves.Tag = tag;
            }
            else
            {
                lstGameSaves.Items.Clear();
                lstGameSaves.Tag = new List<string>();
            }
            lstGameSaves.EndUpdate();
            UpdateListTotal(lstGameSaves);
        }

        private void PopulateBmResults()
        {
            cmbOutputSortBy.BeginUpdate();
            cmbOutputSortBy.Items.Clear();
            cmbOutputSortBy.Items.Add("Name");
            cmbOutputSortBy.Items.Add("Overall");
            for (int i = 0; i < bmResults.TestNames.Count - 1; i++) cmbOutputSortBy.Items.Add(bmResults.TestNames[i]);
            cmbOutputSortBy.SelectedIndex = 0;
            cmbOutputSortBy.EndUpdate();
        }

        private void ResortBmResults()
        {
            if (bmResults == null) return;
            bmResults.SortByColumn = cmbOutputSortBy.SelectedIndex == 0 ? -1 : (cmbOutputSortBy.SelectedIndex == 1 ? (bmResults.TestNames.Count - 1) : (cmbOutputSortBy.SelectedIndex - 2));
            webGrid.DocumentText = bmResults.ToHtml(rdbHumanTacticsInRows.Checked);
        }

        private void StartBenchmark()
        {
            // Parse UI input.
            UIState uiState;
            try
            {
                uiState = GetUIState();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Benchmark settings are invalid.", "Invalid benchmark settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prepare UI.
            IList<string> gameSaves = ((IList<string>)lstGameSaves.Tag).ToList();
            IList<Tactic> humanTactics = lstHumanTactics.Items.Cast<Tactic>().ToList();
            btnStart.Enabled = false;
            prgBenchmark.Value = 0;
            int totalBmRuns = humanTactics.Count * gameSaves.Count * uiState.TestCount;
            prgBenchmark.Maximum = totalBmRuns;
            lblBMRunningCount.Text = "Generating saves";
            lblBMDoneCount.Text = $"0 / {humanTactics.Count * gameSaves.Count}";

            // Create .sav files.
            LOG.DebugFormat("Begin generating {0} saves.", totalBmRuns);
            try { Directory.Delete(TEMP_SAV_FOLDER, true); } catch { }
            try { Directory.CreateDirectory(TEMP_SAV_FOLDER); } catch { }
            string[,] savFilenames = new string[gameSaves.Count, humanTactics.Count];
            bmResults = new TacticBmResults("Game saves", gameSaves.Select(x => Path.GetFileNameWithoutExtension(x)).ToList(), humanTactics.Select(x => x.Name).ToList());
            Queue<Tuple<int, int>> remQu = new Queue<Tuple<int, int>>();
            BenchmarkSaveInfo[] bmSaveInfos = new BenchmarkSaveInfo[gameSaves.Count];
            Cursor.Current = Cursors.WaitCursor;
            for (int gameSaveIndex = 0; gameSaveIndex < gameSaves.Count; gameSaveIndex++)
            {
                // Load info necessary for benchmarking from .sav file.
                SaveReader saveReader = new SaveReader();
                saveReader.Load(gameSaves[gameSaveIndex], false);
                BenchmarkSaveInfo benchmarkSaveInfo = BenchmarkSaveInfo.Load(saveReader);
                bmSaveInfos[gameSaveIndex] = benchmarkSaveInfo;

                // Update players.
                if (uiState.MakePlayersAllPositioners)
                {
                    List<TStaff> staffs = saveReader.BlockToObjects<TStaff>("staff.dat");
                    List<TPlayer> players = saveReader.BlockToObjects<TPlayer>("player.dat");
                    foreach (TStaff staff in staffs.Where(x => x.ClubJob == benchmarkSaveInfo.HumanClub.ID && x.Player >= 0 && x.Player < players.Count))
                    {
                        TPlayer player = players[staff.Player];
                        if (player.Goalkeeper >= 20) continue;
                        player.Goalkeeper = 1;
                        player.Sweeper = player.Defender = player.DefensiveMidfielder = player.Midfielder = player.AttackingMidfielder = player.Attacker = player.WingBack = player.FreeRole = 20;
                        player.LeftSide = player.Central = player.RightSide = 20;
                    }
                    saveReader.ObjectsToBlock("player.dat", players);
                }

                for (int humanTacticIndex = 0; humanTacticIndex < humanTactics.Count; humanTacticIndex++)
                {
                    string savFilename = savFilenames[gameSaveIndex, humanTacticIndex] = Path.GetFullPath(Path.Combine(TEMP_SAV_FOLDER, $"{humanTacticIndex}vs{gameSaveIndex}.sav"));
                    Tactic humanTactic = humanTactics[humanTacticIndex];
                    
                    // Update human tactics.
                    saveReader.ReplaceHumanTactic(humanTactic);

                    saveReader.Write(savFilename, false);
                    for (int i = 0; i < uiState.TestCount; i++) remQu.Enqueue(new Tuple<int, int>(gameSaveIndex, humanTacticIndex));
                }
                lblBMDoneCount.Text = $"{(gameSaveIndex + 1) * humanTactics.Count} / {humanTactics.Count * gameSaves.Count}";
            }
            lblBMRunningCount.Text = "0";
            lblBMDoneCount.Text = $"0 / {totalBmRuns}";
            LOG.DebugFormat("End generating saves.");

            // Run benchmarking in background thread.
            Thread bgThread = new Thread(() =>
            {
                HashSet<Thread> threads = new HashSet<Thread>();
                IDictionary<CMBenchmarker, Tuple<int, int>> tacticsByBenchmarker = new ConcurrentDictionary<CMBenchmarker, Tuple<int, int>>();
                Queue<int> idQu = new Queue<int>();
                for (int id = 1; id <= uiState.ThreadCount; id++) idQu.Enqueue(id);
                while (true)
                {
                    lock (bmResults) if (!remQu.Any() && !threads.Any()) break;

                    // Remove complete threads.
                    int oldThreadCount = threads.Count;
                    foreach (Thread thread in threads.ToList()) if (!thread.IsAlive) threads.Remove(thread);
                    if (threads.Count != oldThreadCount) this.InvokeIfRequired(() =>
                    {
                        lblBMRunningCount.Text = "" + threads.Count;
                        int doneCount = totalBmRuns - (remQu.Count + threads.Count);
                        lblBMDoneCount.Text = $"{doneCount} / {totalBmRuns}";
                        prgBenchmark.Value = totalBmRuns - (remQu.Count + threads.Count);
                    });

                    // Start new threads.
                    oldThreadCount = threads.Count;
                    // Take available ID and remaining tactics.
                    int id = -1;
                    Tuple<int, int> tacticTuple = null;
                    lock (bmResults)
                        if (remQu.Any() && idQu.Any())
                        {
                            id = idQu.Dequeue();
                            tacticTuple = remQu.Dequeue();
                        }

                    if (tacticTuple != null)
                    {
                        // Create benchmarker.
                        CMBenchmarker bench = new CMBenchmarker()
                        {
                            Id = id,
                            CmFolder = uiState.CMFolder,
                            CmExeFilename = Path.GetFullPath(@"Template\cm0102_bm.exe"),
                            SavFilename = savFilenames[tacticTuple.Item1, tacticTuple.Item2],
                            TimeoutSec = uiState.ThreadTimeoutSec,
                            SaveInfo = bmSaveInfos[tacticTuple.Item1],
                            HideWindow = uiState.HideCMWindow
                        };
                        tacticsByBenchmarker[bench] = tacticTuple;

                        // Start benchmarker.
                        LOG.DebugFormat("Creating thread for benchmarker id {0} sav {1}", id, Path.GetFileName(bench.SavFilename));
                        Thread thread = new Thread((object obj) =>
                        {
                            CMBenchmarker bm = (CMBenchmarker)obj;
                            SingleSeasonBenchmarkResult season = null;
                            LOG.DebugFormat("Starting benchmarker id {0} sav {1}", bm.Id, Path.GetFileName(bm.SavFilename));
                            try
                            {
                                season = bench.Start();
                                LOG.DebugFormat("Benchmarker id {0} sav {1} completed.", bm.Id, Path.GetFileName(bm.SavFilename));
                            }
                            catch (Exception e)
                            {
                                LOG.Error(string.Format("Caught error from benchmarker id {0} sav {1}", bm.Id, Path.GetFileName(bm.SavFilename)), e);
                                this.InvokeIfRequired(() => MessageBox.Show(this, $"{e}", "Benchmark error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            }
                            lock (bmResults)
                            {
                                bmResults.Results[tacticsByBenchmarker[bm].Item1, tacticsByBenchmarker[bm].Item2].Append(season);
                                idQu.Enqueue(bm.Id);
                                LOG.DebugFormat("Returned benchmarker id {0} sav {1}", bm.Id, Path.GetFileName(bm.SavFilename));
                            }
                        });
                        threads.Add(thread);
                        thread.Start(bench);
                    }
                    if (threads.Count != oldThreadCount) this.InvokeIfRequired(() =>
                    {
                        lblBMRunningCount.Text = "" + threads.Count;
                        int doneCount = totalBmRuns - (remQu.Count + threads.Count);
                        lblBMDoneCount.Text = $"{doneCount} / {totalBmRuns}";
                        prgBenchmark.Value = totalBmRuns - (remQu.Count + threads.Count);
                    });

                    // Make polling delay.
                    Thread.Sleep(10);
                }

                // All benchmarkers completed. Update UI.
                this.InvokeIfRequired(() =>
                {
                    btnStart.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    bmResults.CalculateOverall();
                    PopulateBmResults();
                });
            });
            bgThread.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUIState();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUIState();
            rdbHumanTacticsInRows.CheckedChanged += HumanTacticsInRows_CheckedChanged;
            rdbHumanTacticsInCols.CheckedChanged += HumanTacticsInRows_CheckedChanged;
        }

        private void HumanTacticsInRows_CheckedChanged(object sender, EventArgs e)
        {
            if (bmResults == null) return;
            webGrid.DocumentText = bmResults.ToHtml(rdbHumanTacticsInRows.Checked);
        }

        private void btnOutputSave_Click(object sender, EventArgs e)
        {
            if (dlgSaveResults.ShowDialog() != DialogResult.OK) return;
            try
            {
                File.WriteAllText(dlgSaveResults.FileName, Helper.Serialize(bmResults));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Save benchmark results error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOutputLoad_Click(object sender, EventArgs e)
        {
            if (dlgLoadResults.ShowDialog() != DialogResult.OK) return;
            string content;
            try
            {
                content = File.ReadAllText(dlgLoadResults.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Load benchmark results I/O error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                bmResults = Helper.Deserialize<TacticBmResults>(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Load benchmark parsing error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopulateBmResults();
        }

        private void btnOutputExportHtml_Click(object sender, EventArgs e)
        {
            if (dlgExportResultsToHtml.ShowDialog() != DialogResult.OK) return;
            try
            {
                File.WriteAllText(dlgExportResultsToHtml.FileName, bmResults.ToHtml(rdbHumanTacticsInRows.Checked));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Export benchmark results error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbOutputSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResortBmResults();
        }

        private void btnCMFolderDlg_Click(object sender, EventArgs e)
        {
            if (dlgCmFolder.ShowDialog(this) != DialogResult.OK) return;
            string folder = dlgCmFolder.SelectedPath;
            txtCmFolder.Text = folder;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartBenchmark();
        }

        private void btnHumanAddFolder_Click(object sender, EventArgs e)
        {
            AddTacticsFromFolder();
        }
        private void btnHumanAddFiles_Click(object sender, EventArgs e)
        {
            if (dlgTacticFiles.ShowDialog(this) != DialogResult.OK) return;
            AddTacticsFromFiles(dlgTacticFiles.FileNames.ToList());
        }

        private void btnGameSaveAddFiles_Click(object sender, EventArgs e)
        {
            if (dlgGameSaveFiles.ShowDialog(this) != DialogResult.OK) return;
            AddGameSavesFromFiles(dlgGameSaveFiles.FileNames.ToList());
        }

        private void btnGameSaveAddFolder_Click(object sender, EventArgs e)
        {
            AddGameSavesFromFolder(lstGameSaves);
        }

        private void btnHumanDelete_Click(object sender, EventArgs e)
        {
            DeleteTacticsFromList(true);
        }
        private void btnHumanClear_Click(object sender, EventArgs e)
        {
            DeleteTacticsFromList(false);
        }

        private void btnGameSaveDelete_Click(object sender, EventArgs e)
        {
            DeleteGameSavesFromList(true);
        }
        private void btnGameSavesClear_Click(object sender, EventArgs e)
        {
            DeleteGameSavesFromList( false);
        }

        private void lstGameSaves_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete) DeleteGameSavesFromList(true);
        }
        private void lstHumanTactics_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete) DeleteTacticsFromList(true);
        }
    }
}
