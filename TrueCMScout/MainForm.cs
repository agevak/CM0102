using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CM.Model;
using CM.Save;
using CM.Scout.Model;
using CM.UI;
using CM.UI.Model;
using Microsoft.VisualBasic;
using static System.Windows.Forms.AxHost;

namespace CM.Scout
{
    public partial class MainForm : Form
    {
        private const string UI_STATE_FILE = "CMScout.json";

        private CMScoutUIState uiState = new CMScoutUIState();
        private CMDBCacher dbCacher = new CMDBCacher();

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadUIState()
        {
            try
            {
                string js = File.ReadAllText(UI_STATE_FILE);
                uiState = CMScoutUIState.FromString(js);
                Location = uiState.MainFormLocation;
                Size = uiState.MainFormSize;
                WindowState = uiState.WindowState;
                for (int i = 0; i < uiState.Tabs.Count; i++)
                {
                    PlayerSearchControl playerSearchControl = CreatePlayerSearchControl(uiState.Tabs[i]);
                    TabPage tabPage = AddTab(playerSearchControl, false);
                    playerSearchControl.Initialize();
                    playerSearchControl.UIStateChanged += PlayerSearchControl_UIStateChanged;
                }
                tbcTabs.SelectedIndex = uiState.SelectedTabIndex;
            }
            catch (Exception e)
            {
            }
        }
        private void SaveUIState()
        {
            uiState.MainFormLocation = Location;
            uiState.MainFormSize = Size;
            uiState.WindowState = WindowState;
            uiState.SelectedTabIndex = tbcTabs.SelectedIndex;
            string js = uiState.ToString();
            try
            {
                File.WriteAllText(UI_STATE_FILE, js);
            }
            catch (Exception e)
            {
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUIState();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveUIState();
        }

        private PlayerSearchControl CreatePlayerSearchControl(PlayerSearchControlUIState playerSearchControlUIState)
        {
            PlayerSearchControl playerSearchControl = new PlayerSearchControl()
            {
                DBCacher = dbCacher,
                Dock = DockStyle.Fill
            };
            playerSearchControl.UIState = playerSearchControlUIState;
            return playerSearchControl;
        }
        private void PlayerSearchControl_UIStateChanged(object sender, PlayerSearchControlUIStateChangedEventArgs e)
        {
            SaveUIState();
        }

        private TabPage AddTab(PlayerSearchControl playerSearchControl, bool updateUIState)
        {
            // Create TabPage.
            TabPage tabPage = new TabPage(playerSearchControl.UIState.Name + " X");

            // Add control buttons.
            Button btn = new Button()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(30, 2),
                Size = new Size(75, 23),
                Text = "Rename tab"
            };
            btn.Click += btnRenameTab_Click;
            tabPage.Controls.Add(btn);
            btn = new Button()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(120, 2),
                Size = new Size(75, 23),
                Text = "Delete tab"
            };
            btn.Click += btnDeleteTab_Click;
            tabPage.Controls.Add(btn);

            // Add PlayerSearchControl.
            tabPage.Controls.Add(playerSearchControl);
            tabPage.Tag = playerSearchControl;

            // Add to UIState.
            if (updateUIState) uiState.Tabs.Add(playerSearchControl.UIState);

            // Insert tab to TabControl.
            tbcTabs.TabPages.Insert(tbcTabs.TabPages.Count - 1, tabPage);
            return tabPage;
        }
        private void AddNewTab()
        {
            PlayerSearchControl playerSearchControl = CreatePlayerSearchControl(new PlayerSearchControlUIState() { Name = "Double click to rename" });
            TabPage newTabPage = AddTab(playerSearchControl, true);
            playerSearchControl.Initialize();
            tbcTabs.SelectedTab = newTabPage;
            playerSearchControl.UIStateChanged += PlayerSearchControl_UIStateChanged;
            SaveUIState();
        }
        private void DeleteTab(int index)
        {
            if (index < 0 || index >= tbcTabs.TabCount - 1) return;
            PlayerSearchControl playerSearchControl = (PlayerSearchControl)tbcTabs.SelectedTab.Tag;
            if (MessageBox.Show(this, $"This tab will be deleted:\n{playerSearchControl.UIState.Name}\nContinue?", "Delete tab", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.OK) return;
            tbcTabs.TabPages.RemoveAt(index);
            playerSearchControl.Close();
            uiState.Tabs.RemoveAt(index);
        }
        private void RenameTab(int tabIndex)
        {
            if (tabIndex < 0 || tabIndex >= tbcTabs.TabCount - 1) return;
            TabPage tab = tbcTabs.TabPages[tabIndex];
            if (tab == null) return;
            PlayerSearchControl playerSearchControl = (PlayerSearchControl)tab.Tag;
            string newName = Interaction.InputBox("Enter tab name:", "Edit tab name", playerSearchControl.UIState.Name);
            if (string.IsNullOrEmpty(newName)) return;
            playerSearchControl.UIState.Name = newName;
            tab.Text = playerSearchControl.UIState.Name + " X";
            SaveUIState();
        }

        private void btnDeleteTab_Click(object sender, EventArgs e)
        {
            DeleteTab(tbcTabs.SelectedIndex);
        }
        private void btnRenameTab_Click(object sender, EventArgs e)
        {
            RenameTab(tbcTabs.SelectedIndex);
        }
        private void tbcTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcTabs.SelectedTab == tabAddTab) AddNewTab();
        }
        private void tbcTabs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int tabIndex = GetTabIndexByHeaderPoint(e.Location, out bool xClicked);
            RenameTab(tabIndex);
        }
        private void tbcTabs_MouseClick(object sender, MouseEventArgs e)
        {
            int tabIndex = GetTabIndexByHeaderPoint(e.Location, out bool xClicked);
            if (tabIndex < 0) return;
            if (xClicked) DeleteTab(tabIndex);
            else
            {
                if (tabIndex < tbcTabs.TabPages.Count - 1) return;
                AddNewTab();
            }
        }
        private void tbcTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == tbcTabs.TabPages.Count - 1) e.Cancel = true;
        }
        private int GetTabIndexByHeaderPoint(Point point, out bool xClicked)
        {
            for (int i = 0; i < tbcTabs.TabPages.Count; i++)
            {
                Rectangle tabRect = tbcTabs.GetTabRect(i);
                if (tabRect.Contains(point))
                {
                    xClicked = i < tbcTabs.TabPages.Count - 1 && tabRect.Right - point.X <= 15;
                    return i;
                }
            }
            xClicked = false;
            return -1;
        }
    }
}
