using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CM.Helpers;
using CM.Model;
using CM.Save;
using CM.Save.Model;
using Microsoft.VisualBasic;

namespace CM
{
    public partial class MainForm : Form
    {
        private const string CONFIG_FILENAME = "config.json";

        private SaveReader saveReader;

        public MainForm()
        {
            InitializeComponent();

            lstSavTactics.BeginUpdate();
            for (int i = 0; i < Tactic.AI_PACK_FILENAMES.Length; i++)
                lstSavTactics.Items.Add(new ListViewItem(new string[] { "" + (i + 1), Tactic.AI_PACK_FILENAMES[i], Tactic.AI_PACK_DEFAULT_NAMES[i], Tactic.AI_PACK_DEFAULT_NAMES[i], "" }));
            lstSavTactics.EndUpdate();
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
            return new UIState()
            {
                LoadFolder = dlgLoadFolder.SelectedPath,
                LoadSav = dlgLoadSav.FileName,
                LoadAIPack = dlgLoadAIPack.SelectedPath,
                SaveSav = dlgSaveSav.FileName,
                SaveAIPack = dlgSaveAIPack.SelectedPath,
                SaveTct = dlgSaveTct.SelectedPath,
                UpdateTitle = chkUpdateName.Checked,
                MainFormLocation = Location,
                MainFormSize = Size
            };
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
                catch (Exception e) { }
            }
            if (state == null) return;

            // Apply config.
            txtLoadFolder.Text = dlgLoadFolder.SelectedPath = state.LoadFolder;
            dlgLoadSav.FileName = state.LoadSav;
            dlgLoadAIPack.SelectedPath = state.LoadAIPack;
            dlgSaveSav.FileName = state.SaveSav;
            dlgSaveAIPack.SelectedPath = state.SaveAIPack;
            dlgSaveTct.SelectedPath = state.SaveTct;
            chkUpdateName.Checked = state.UpdateTitle;
            Location = state.MainFormLocation;
            Size = state.MainFormSize;
        }

        private void LoadSav()
        {
            string savFilename = dlgLoadSav.FileName;
            if (!File.Exists(savFilename))
            {
                MessageBox.Show(this, $"File doesn't exist: {savFilename}", "Save file doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                saveReader = new SaveReader();
                saveReader.Load(savFilename, true);
                IList<Tactic> tactics = saveReader.ExtractAITactics();
                lstSavTactics.BeginUpdate();
                for (int i = 0; i < lstSavTactics.Items.Count; i++)
                {
                    ListViewItem item = lstSavTactics.Items[i];
                    Tactic tactic = tactics[i];
                    item.SubItems[3].Text = tactic.Name;
                    item.SubItems[4].Text = "";
                    item.Tag = tactic;
                }
                lstSavTactics.EndUpdate();
                btnSaveSav.Enabled = btnSaveAIPack.Enabled = btnSaveTct.Enabled = btnResetSavTacticNames.Enabled = true;
                ResetSavItemsModified();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void LoadFolder()
        {
            string folder = dlgLoadFolder.SelectedPath;
            if (string.IsNullOrWhiteSpace(folder)) return;
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(this, $"Folder doesn't exist: {folder}", "Tactics folder doesn't exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                IList<Tactic> tactics = new List<Tactic>();
                IList<string> filenames = new List<string>();
                IList<string> errorFilenames = new List<string>();
                foreach (string filename in Directory.GetFiles(folder).Where(x => Path.GetExtension(x) == ".tct" || Path.GetExtension(x) == ".pct").OrderBy(x => x))
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
                    filenames.Add(filename);
                    tactics.Add(tactic);
                }

                // Show errors.
                if (errorFilenames.Count > 0) MessageBox.Show(this, $"Failed to load these files:\n{string.Join("\n", errorFilenames.Select(x => Path.GetFileName(x)))}",
                    "Failed to load tactics files.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Populate loaded tactics to list box.
                lstFolderTactics.BeginUpdate();
                lstFolderTactics.Items.Clear();
                for (int i = 0; i < tactics.Count; i++) lstFolderTactics.Items.Add(new ListViewItem(Path.GetFileName(filenames[i])) { Tag = tactics[i] });
                lstFolderTactics.EndUpdate();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SaveToFiles(FolderBrowserDialog dialog, bool useCurNames, bool isTct)
        {
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                foreach (ListViewItem item in lstSavTactics.Items)
                {
                    Tactic tactic = (Tactic)item.Tag;
                    File.WriteAllBytes(Path.Combine(dialog.SelectedPath,
                        Path.GetFileNameWithoutExtension(useCurNames ? tactic.Name : item.SubItems[1].Text) + (isTct ? ".tct" : ".pct")),
                        tactic.ToTacticFile(isTct ? Tactic.TCT_MARKER : Tactic.PCT_MARKER));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Save file error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSavTactic(ListViewItem savItem, ListViewItem folderItem, bool updateName)
        {
            Tactic newTactic = (Tactic)folderItem.Tag;
            savItem.Tag = newTactic;
            savItem.SubItems[4].Text = folderItem.Text;
            if (updateName) savItem.SubItems[3].Text = newTactic.Name;
            savItem.Font = new Font(savItem.Font, FontStyle.Bold);
        }

        private void ResetSavItemsModified()
        {
            foreach (ListViewItem item in lstSavTactics.Items)
            {
                item.SubItems[4].Text = "";
                item.Font = new Font(item.Font, FontStyle.Regular);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUIState();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUIState();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (dlgLoadFolder.ShowDialog(this) != DialogResult.OK) return;
            txtLoadFolder.Text = dlgLoadFolder.SelectedPath;
            LoadFolder();
        }

        private void btnLoadSav_Click(object sender, EventArgs e)
        {
            if (dlgLoadSav.ShowDialog(this) != DialogResult.OK) return;
            LoadSav();
        }

        private void btnRefreshFolder_Click(object sender, EventArgs e)
        {
            LoadFolder();
        }

        private void btnSaveSav_Click(object sender, EventArgs e)
        {
            if (dlgSaveSav.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                foreach (ListViewItem item in lstSavTactics.Items)
                {
                    Tactic tactic = (Tactic)item.Tag;
                    tactic.Name = item.SubItems[3].Text;
                }
                saveReader.ReplaceAITactics(lstSavTactics.Items.Cast<ListViewItem>().Select(x => (Tactic)x.Tag).ToList());
                saveReader.Write(dlgSaveSav.FileName, saveReader.WasCompressed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{ex}", "Save file error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ResetSavItemsModified();
        }

        private void btnSaveAIPack_Click(object sender, EventArgs e)
        {
            SaveToFiles(dlgSaveAIPack, false, false);
        }

        private void btnSaveTct_Click(object sender, EventArgs e)
        {
            SaveToFiles(dlgSaveAIPack, true, true);
        }

        private void lstFolderTactics_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lstFolderTactics.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void lstSavTactics_DragDrop(object sender, DragEventArgs e)
        {
            Point p = lstSavTactics.PointToClient(new Point(e.X, e.Y));
            ListViewItem selectedItem = lstSavTactics.GetItemAt(p.X, p.Y);
            if (selectedItem == null) return;
            ListViewItem folderItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            UpdateSavTactic(selectedItem, folderItem, chkUpdateName.Checked);
        }

        private void lstSavTactics_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem))) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void btnAutoAssign_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem folderItem in lstFolderTactics.Items)
            {
                string filename = folderItem.Text;
                int index = -1;
                while (true)
                {
                    index = Array.IndexOf(Tactic.AI_PACK_FILENAMES, filename, index + 1);
                    if (index < 0) break;
                    UpdateSavTactic(lstSavTactics.Items[index], folderItem, false);
                }
            }
        }

        private void lstSavTactics_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditSavTacticName.Enabled = btnSaveSav.Enabled && lstSavTactics.SelectedItems.Count == 1;
        }

        private void btnEditSavTacticName_Click(object sender, EventArgs e)
        {
            if (lstSavTactics.SelectedItems.Count != 1) return;
            ListViewItem item = lstSavTactics.SelectedItems[0];
            string newName = Interaction.InputBox("Tactic name:", "Edit tactic name", item.SubItems[3].Text);
            if (string.IsNullOrWhiteSpace(newName) || newName == item.SubItems[3].Text) return;
            newName = Tactic.TrimName(newName);
            item.SubItems[3].Text = newName;
            item.Font = new Font(item.Font, FontStyle.Bold);
        }

        private void btnResetSavTacticName_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstSavTactics.Items)
            {
                item.SubItems[3].Text = item.SubItems[2].Text;
                if (string.IsNullOrWhiteSpace(item.SubItems[4].Text)) item.Font = new Font(item.Font, FontStyle.Regular);
            }
        }
    }
}
