using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
                DontReplaceTactics = chkDontReplaceAITactics.Checked,
                AddFolder = dlgTacticsFolder.SelectedPath,
                CMFolder = txtCmFolder.Text,
                BMSavFilename = txtBMSavFilename.Text,
                TestCount = int.Parse(txtTestCount.Text),
                ThreadCount = int.Parse(txtThreadCount.Text),
                ThreadTimeoutSec = int.Parse(txtThreadTimeout.Text),
                HideCMWindow = chkHideCMWindow.Checked,
                MainFormLocation = Location,
                MainFormSize = Size,
                SplitterDistance = splitContainer.SplitterDistance,
                WindowState = WindowState
            };
            if (!string.IsNullOrWhiteSpace(dlgTacticFiles.FileName)) state.AddFiles = Path.GetDirectoryName(dlgTacticFiles.FileName);
            else state.AddFiles = dlgTacticFiles.InitialDirectory;
            if (!string.IsNullOrWhiteSpace(dlgAddSavFilename.FileName)) state.AddSav = Path.GetDirectoryName(dlgAddSavFilename.FileName);
            else state.AddSav = dlgAddSavFilename.InitialDirectory;
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
            chkDontReplaceAITactics.Checked = state.DontReplaceTactics;
            dlgTacticFiles.InitialDirectory = state.AddFiles;
            dlgTacticsFolder.SelectedPath = state.AddFolder;
            dlgAddSavFilename.InitialDirectory = state.AddSav;
            txtCmFolder.Text = dlgCmFolder.SelectedPath = state.CMFolder;
            txtBMSavFilename.Text = state.BMSavFilename;
            txtTestCount.Text = "" + state.TestCount;
            txtThreadCount.Text = "" + state.ThreadCount;
            txtThreadTimeout.Text = "" + state.ThreadTimeoutSec;
            chkHideCMWindow.Checked = state.HideCMWindow;
            Location = state.MainFormLocation;
            Size = state.MainFormSize;
            splitContainer.SplitterDistance = state.SplitterDistance;
            WindowState = state.WindowState;
        }

        private void AddFromSavFile(ListBox listBox)
        {
            if (dlgAddSavFilename.ShowDialog(this) != DialogResult.OK) return;
            string savFilename = dlgAddSavFilename.FileName;
            if (!File.Exists(savFilename))
            {
                MessageBox.Show(this, $"File doesn't exist: {savFilename}", "Save file doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SaveReader saveReader = new SaveReader();
                IList<Tactic> tactics;
                try
                {
                    saveReader.Load(savFilename, false);
                    tactics = saveReader.ExtractAITactics();
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, $"Failed to load tactics from .sav file: {e}", "Load tactics failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AddTacticsList(listBox, tactics.OrderBy(x => x.Name).ToList());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AddFromFolder(ListBox listBox)
        {
            if (dlgTacticsFolder.ShowDialog(this) != DialogResult.OK) return;
            string folder = dlgTacticsFolder.SelectedPath;
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(this, $"Folder doesn't exist: {folder}", "Tactics folder doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<string> filenames = Directory.GetFiles(folder).Where(x => Path.GetExtension(x) == ".tct" || Path.GetExtension(x) == ".pct").ToList();
            AddFromFiles(listBox, filenames);
        }

        private void AddFromFiles(ListBox listBox)
        {
            if (dlgTacticFiles.ShowDialog(this) != DialogResult.OK) return;
            AddFromFiles(listBox, dlgTacticFiles.FileNames.ToList());
        }

        private void AddFromFiles(ListBox listBox, IList<string> filenames)
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
            AddTacticsList(listBox, tactics.OrderBy(x => x.Name).ToList());
        }

        private void AddTacticsList(ListBox view, IList<Tactic> model)
        {
            view.BeginUpdate();
            view.Items.AddRange(model.ToArray());
            view.EndUpdate();
            UpdateListTotal(view);
        }

        private Label GetTotalLabel(ListBox view) => (view == lstHumanTactics) ? lblHumanTacticsTotal : lblAITacticsTotal;

        private void UpdateListTotal(ListBox view)
        {
            Label totalLabel = GetTotalLabel(view);
            totalLabel.Text = "" + view.Items.Count;
        }

        private void DeleteItemsFromList(ListBox view, bool onlySelected)
        {
            view.BeginUpdate();
            if (onlySelected)
            {
                HashSet<Tactic> tactics = new HashSet<Tactic>(view.Items.Cast<Tactic>());
                tactics.ExceptWith(view.SelectedItems.Cast<Tactic>());
                view.Items.Clear();
                view.Items.AddRange(tactics.OrderBy(x => x.Name).ToArray());
            }
            else view.Items.Clear();
            view.EndUpdate();
            UpdateListTotal(view);
        }

        private bool ShowOpenFileDialog(OpenFileDialog dialog, TextBox textBox)
        {
            dialog.FileName = textBox.Text;
            if (dialog.ShowDialog(this) != DialogResult.OK) return false;
            textBox.Text = dialog.FileName;
            return true;
        }
        
        private void PopulateBmResults()
        {
            cmbOutputSortBy.BeginUpdate();
            cmbOutputSortBy.Items.Clear();
            cmbOutputSortBy.Items.Add("Name");
            cmbOutputSortBy.Items.Add("Overall");
            for (int i = 0; i < bmResults.AITactics.Count - 1; i++) cmbOutputSortBy.Items.Add(bmResults.AITactics[i]);
            cmbOutputSortBy.SelectedIndex = 0;
            cmbOutputSortBy.EndUpdate();
        }

        private void ResortBmResults()
        {
            bmResults.SortByColumn = cmbOutputSortBy.SelectedIndex == 0 ? -1 : (cmbOutputSortBy.SelectedIndex == 1 ? (bmResults.AITactics.Count - 1) : (cmbOutputSortBy.SelectedIndex - 2));
            webGrid.DocumentText = bmResults.ToHtml();
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
            IList<Tactic> aiTactics = uiState.DontReplaceTactics ? new Tactic[] { new Tactic() { Name = "NotReplaced" } }.ToList() : lstAITactics.Items.Cast<Tactic>().ToList();
            IList<Tactic> humanTactics = lstHumanTactics.Items.Cast<Tactic>().ToList();
            btnStart.Enabled = false;
            prgBenchmark.Value = 0;
            int totalBmRuns = humanTactics.Count * aiTactics.Count * uiState.TestCount;
            prgBenchmark.Maximum = totalBmRuns;
            lblBMRunningCount.Text = "Generating saves";
            lblBMDoneCount.Text = $"0 / {humanTactics.Count * aiTactics.Count}";

            // Load info necessary for benchmarking from .sav file.
            SaveReader saveReader = new SaveReader();
            saveReader.Load(uiState.BMSavFilename, false);
            BenchmarkSaveInfo benchmarkSaveInfo = BenchmarkSaveInfo.Load(saveReader);

            // Create .sav files.
            LOG.DebugFormat("Begin generating {0} saves.", totalBmRuns);
            try { Directory.Delete(TEMP_SAV_FOLDER, true); } catch { }
            try { Directory.CreateDirectory(TEMP_SAV_FOLDER); } catch { }
            string[,] savFilenames = new string[aiTactics.Count, humanTactics.Count];
            bmResults = new TacticBmResults(aiTactics.Select(x => x.Name).ToList(), humanTactics.Select(x => x.Name).ToList());
            Queue<Tuple<int, int>> remQu = new Queue<Tuple<int, int>>();
            Cursor.Current = Cursors.WaitCursor;
            for (int aiTacticIndex = 0; aiTacticIndex < aiTactics.Count; aiTacticIndex++)
            {
                for (int humanTacticIndex = 0; humanTacticIndex < humanTactics.Count; humanTacticIndex++)
                {
                    string savFilename = savFilenames[aiTacticIndex, humanTacticIndex] = Path.GetFullPath(Path.Combine(TEMP_SAV_FOLDER, $"{humanTacticIndex}vs{aiTacticIndex}.sav"));
                    Tactic humanTactic = humanTactics[humanTacticIndex];
                    Tactic aiTactic = aiTactics[aiTacticIndex];
                    saveReader.ReplaceHumanTactic(humanTactic);
                    if (!uiState.DontReplaceTactics) saveReader.ReplaceAITacticsWithSameOne(aiTactic);
                    saveReader.Write(savFilename, false);
                    for (int i = 0; i < uiState.TestCount; i++) remQu.Enqueue(new Tuple<int, int>(aiTacticIndex, humanTacticIndex));
                }
                lblBMDoneCount.Text = $"{(aiTacticIndex + 1) * humanTactics.Count} / {humanTactics.Count * aiTactics.Count}";
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
                            SaveInfo = benchmarkSaveInfo,
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
                File.WriteAllText(dlgExportResultsToHtml.FileName, bmResults.ToHtml());
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
            AddFromFolder(lstHumanTactics);
        }

        private void btnHumanAddFiles_Click(object sender, EventArgs e)
        {
            AddFromFiles(lstHumanTactics);
        }

        private void btnHumanAddSav_Click(object sender, EventArgs e)
        {
            AddFromSavFile(lstHumanTactics);
        }

        private void btnAIAddFiles_Click(object sender, EventArgs e)
        {
            AddFromFiles(lstAITactics);
        }

        private void btnAIAddFolder_Click(object sender, EventArgs e)
        {
            AddFromFolder(lstAITactics);
        }

        private void btnAIAddSav_Click(object sender, EventArgs e)
        {
            AddFromSavFile(lstAITactics);
        }

        private void btnBMSavFilenameDlg_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog(dlgBmSavFilename, txtBMSavFilename);
        }

        private void btnHumanDelete_Click(object sender, EventArgs e)
        {
            DeleteItemsFromList(lstHumanTactics, true);
        }

        private void btnHumanClear_Click(object sender, EventArgs e)
        {
            DeleteItemsFromList(lstHumanTactics, false);
        }

        private void btnAIDelete_Click(object sender, EventArgs e)
        {
            DeleteItemsFromList(lstAITactics, true);
        }

        private void btnAIClear_Click(object sender, EventArgs e)
        {
            DeleteItemsFromList(lstAITactics, false);
        }

        private void lstAITactics_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete) DeleteItemsFromList(lstAITactics, true);
        }

        private void lstHumanTactics_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete) DeleteItemsFromList(lstHumanTactics, true);
        }

        private void chkDontReplaceAITactics_CheckedChanged(object sender, EventArgs e)
        {
            lstAITactics.Enabled = btnAIAddFiles.Enabled = btnAIAddFolder.Enabled = btnAIAddSav.Enabled = btnAIDelete.Enabled = btnAIClear.Enabled = lblAITacticsTotal.Enabled = !chkDontReplaceAITactics.Checked;
        }
    }
}
