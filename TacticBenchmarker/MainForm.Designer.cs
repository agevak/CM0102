namespace CM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.pnlOutputGrid = new System.Windows.Forms.Panel();
            this.webGrid = new System.Windows.Forms.WebBrowser();
            this.pnlOutputControl = new System.Windows.Forms.Panel();
            this.rdbHumanTacticsInCols = new System.Windows.Forms.RadioButton();
            this.rdbHumanTacticsInRows = new System.Windows.Forms.RadioButton();
            this.lblHumanTacticsInRowsCaption = new System.Windows.Forms.Label();
            this.btnOutputExportExcel = new System.Windows.Forms.Button();
            this.btnOutputExportHtml = new System.Windows.Forms.Button();
            this.btnOutputSave = new System.Windows.Forms.Button();
            this.btnOutputLoad = new System.Windows.Forms.Button();
            this.lblOutputExport = new System.Windows.Forms.Label();
            this.cmbOutputSortBy = new System.Windows.Forms.ComboBox();
            this.lblOutputSortBy = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grbBenchmark = new System.Windows.Forms.GroupBox();
            this.chkMakePlayersAllPositioners = new System.Windows.Forms.CheckBox();
            this.chkHideCMWindow = new System.Windows.Forms.CheckBox();
            this.lblBMRunningCount = new System.Windows.Forms.Label();
            this.lblBMDoneCount = new System.Windows.Forms.Label();
            this.lblBMRunningCountCaption = new System.Windows.Forms.Label();
            this.lblBMDoneCountCaption = new System.Windows.Forms.Label();
            this.btnCMFolderDlg = new System.Windows.Forms.Button();
            this.prgBenchmark = new System.Windows.Forms.ProgressBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtThreadTimeout = new System.Windows.Forms.TextBox();
            this.txtThreadCount = new System.Windows.Forms.TextBox();
            this.txtTestCount = new System.Windows.Forms.TextBox();
            this.txtCmFolder = new System.Windows.Forms.TextBox();
            this.lblThreadTimeout = new System.Windows.Forms.Label();
            this.lblThreadCount = new System.Windows.Forms.Label();
            this.lblTestCount = new System.Windows.Forms.Label();
            this.lblExeFilename = new System.Windows.Forms.Label();
            this.pnlGameSaves = new System.Windows.Forms.Panel();
            this.btnGameSaveAddFolder = new System.Windows.Forms.Button();
            this.btnGameSaveAddFiles = new System.Windows.Forms.Button();
            this.lblGameSavesTotal = new System.Windows.Forms.Label();
            this.btnGameSaveDelete = new System.Windows.Forms.Button();
            this.btnGameSaveClear = new System.Windows.Forms.Button();
            this.lblAITactics = new System.Windows.Forms.Label();
            this.lstGameSaves = new System.Windows.Forms.ListBox();
            this.pnlHumanTactics = new System.Windows.Forms.Panel();
            this.btnHumanAddFolder = new System.Windows.Forms.Button();
            this.btnHumanAddFiles = new System.Windows.Forms.Button();
            this.lblHumanTacticsTotal = new System.Windows.Forms.Label();
            this.lstHumanTactics = new System.Windows.Forms.ListBox();
            this.lblHumanTactics = new System.Windows.Forms.Label();
            this.btnHumanDelete = new System.Windows.Forms.Button();
            this.btnHumanClear = new System.Windows.Forms.Button();
            this.dlgGamesSavesFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgLoadResults = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveResults = new System.Windows.Forms.SaveFileDialog();
            this.dlgExportResultsToHtml = new System.Windows.Forms.SaveFileDialog();
            this.dlgExportResultsToExcel = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dlgGameSaveFiles = new System.Windows.Forms.OpenFileDialog();
            this.dlgCmFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgTacticsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgTacticFiles = new System.Windows.Forms.OpenFileDialog();
            this.pnlOutput.SuspendLayout();
            this.pnlOutputGrid.SuspendLayout();
            this.pnlOutputControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grbBenchmark.SuspendLayout();
            this.pnlGameSaves.SuspendLayout();
            this.pnlHumanTactics.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Add.png");
            this.imageList.Images.SetKeyName(1, "Clear.png");
            this.imageList.Images.SetKeyName(2, "Remove.png");
            this.imageList.Images.SetKeyName(3, "Start.png");
            this.imageList.Images.SetKeyName(4, "OpenFile.png");
            this.imageList.Images.SetKeyName(5, "Save.png");
            // 
            // pnlOutput
            // 
            this.pnlOutput.Controls.Add(this.pnlOutputGrid);
            this.pnlOutput.Controls.Add(this.pnlOutputControl);
            this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutput.Location = new System.Drawing.Point(0, 0);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(948, 288);
            this.pnlOutput.TabIndex = 3;
            // 
            // pnlOutputGrid
            // 
            this.pnlOutputGrid.Controls.Add(this.webGrid);
            this.pnlOutputGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputGrid.Location = new System.Drawing.Point(0, 42);
            this.pnlOutputGrid.Name = "pnlOutputGrid";
            this.pnlOutputGrid.Size = new System.Drawing.Size(948, 246);
            this.pnlOutputGrid.TabIndex = 1;
            // 
            // webGrid
            // 
            this.webGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webGrid.Location = new System.Drawing.Point(0, 0);
            this.webGrid.MinimumSize = new System.Drawing.Size(20, 20);
            this.webGrid.Name = "webGrid";
            this.webGrid.Size = new System.Drawing.Size(948, 246);
            this.webGrid.TabIndex = 0;
            // 
            // pnlOutputControl
            // 
            this.pnlOutputControl.Controls.Add(this.rdbHumanTacticsInCols);
            this.pnlOutputControl.Controls.Add(this.rdbHumanTacticsInRows);
            this.pnlOutputControl.Controls.Add(this.lblHumanTacticsInRowsCaption);
            this.pnlOutputControl.Controls.Add(this.btnOutputExportExcel);
            this.pnlOutputControl.Controls.Add(this.btnOutputExportHtml);
            this.pnlOutputControl.Controls.Add(this.btnOutputSave);
            this.pnlOutputControl.Controls.Add(this.btnOutputLoad);
            this.pnlOutputControl.Controls.Add(this.lblOutputExport);
            this.pnlOutputControl.Controls.Add(this.cmbOutputSortBy);
            this.pnlOutputControl.Controls.Add(this.lblOutputSortBy);
            this.pnlOutputControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOutputControl.Location = new System.Drawing.Point(0, 0);
            this.pnlOutputControl.Name = "pnlOutputControl";
            this.pnlOutputControl.Size = new System.Drawing.Size(948, 42);
            this.pnlOutputControl.TabIndex = 1;
            // 
            // rdbHumanTacticsInCols
            // 
            this.rdbHumanTacticsInCols.AutoSize = true;
            this.rdbHumanTacticsInCols.Location = new System.Drawing.Point(406, 10);
            this.rdbHumanTacticsInCols.Name = "rdbHumanTacticsInCols";
            this.rdbHumanTacticsInCols.Size = new System.Drawing.Size(64, 17);
            this.rdbHumanTacticsInCols.TabIndex = 2;
            this.rdbHumanTacticsInCols.TabStop = true;
            this.rdbHumanTacticsInCols.Text = "columns";
            this.rdbHumanTacticsInCols.UseVisualStyleBackColor = true;
            // 
            // rdbHumanTacticsInRows
            // 
            this.rdbHumanTacticsInRows.AutoSize = true;
            this.rdbHumanTacticsInRows.Checked = true;
            this.rdbHumanTacticsInRows.Location = new System.Drawing.Point(353, 10);
            this.rdbHumanTacticsInRows.Name = "rdbHumanTacticsInRows";
            this.rdbHumanTacticsInRows.Size = new System.Drawing.Size(47, 17);
            this.rdbHumanTacticsInRows.TabIndex = 1;
            this.rdbHumanTacticsInRows.TabStop = true;
            this.rdbHumanTacticsInRows.Text = "rows";
            this.rdbHumanTacticsInRows.UseVisualStyleBackColor = true;
            // 
            // lblHumanTacticsInRowsCaption
            // 
            this.lblHumanTacticsInRowsCaption.AutoSize = true;
            this.lblHumanTacticsInRowsCaption.Location = new System.Drawing.Point(255, 12);
            this.lblHumanTacticsInRowsCaption.Name = "lblHumanTacticsInRowsCaption";
            this.lblHumanTacticsInRowsCaption.Size = new System.Drawing.Size(92, 13);
            this.lblHumanTacticsInRowsCaption.TabIndex = 51;
            this.lblHumanTacticsInRowsCaption.Text = "Human tactics in: ";
            // 
            // btnOutputExportExcel
            // 
            this.btnOutputExportExcel.ImageList = this.imageList;
            this.btnOutputExportExcel.Location = new System.Drawing.Point(795, 7);
            this.btnOutputExportExcel.Name = "btnOutputExportExcel";
            this.btnOutputExportExcel.Size = new System.Drawing.Size(43, 25);
            this.btnOutputExportExcel.TabIndex = 8;
            this.btnOutputExportExcel.Text = "Excel";
            this.btnOutputExportExcel.UseVisualStyleBackColor = true;
            this.btnOutputExportExcel.Visible = false;
            // 
            // btnOutputExportHtml
            // 
            this.btnOutputExportHtml.ImageList = this.imageList;
            this.btnOutputExportHtml.Location = new System.Drawing.Point(732, 6);
            this.btnOutputExportHtml.Name = "btnOutputExportHtml";
            this.btnOutputExportHtml.Size = new System.Drawing.Size(47, 25);
            this.btnOutputExportHtml.TabIndex = 7;
            this.btnOutputExportHtml.Text = "HTML";
            this.btnOutputExportHtml.UseVisualStyleBackColor = true;
            this.btnOutputExportHtml.Click += new System.EventHandler(this.btnOutputExportHtml_Click);
            // 
            // btnOutputSave
            // 
            this.btnOutputSave.ImageIndex = 5;
            this.btnOutputSave.ImageList = this.imageList;
            this.btnOutputSave.Location = new System.Drawing.Point(575, 6);
            this.btnOutputSave.Name = "btnOutputSave";
            this.btnOutputSave.Size = new System.Drawing.Size(75, 25);
            this.btnOutputSave.TabIndex = 4;
            this.btnOutputSave.Text = "Save";
            this.btnOutputSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOutputSave.UseVisualStyleBackColor = true;
            this.btnOutputSave.Click += new System.EventHandler(this.btnOutputSave_Click);
            // 
            // btnOutputLoad
            // 
            this.btnOutputLoad.ImageIndex = 4;
            this.btnOutputLoad.ImageList = this.imageList;
            this.btnOutputLoad.Location = new System.Drawing.Point(494, 6);
            this.btnOutputLoad.Name = "btnOutputLoad";
            this.btnOutputLoad.Size = new System.Drawing.Size(75, 25);
            this.btnOutputLoad.TabIndex = 3;
            this.btnOutputLoad.Text = "Load";
            this.btnOutputLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOutputLoad.UseVisualStyleBackColor = true;
            this.btnOutputLoad.Click += new System.EventHandler(this.btnOutputLoad_Click);
            // 
            // lblOutputExport
            // 
            this.lblOutputExport.AutoSize = true;
            this.lblOutputExport.Location = new System.Drawing.Point(685, 12);
            this.lblOutputExport.Name = "lblOutputExport";
            this.lblOutputExport.Size = new System.Drawing.Size(40, 13);
            this.lblOutputExport.TabIndex = 2;
            this.lblOutputExport.Text = "Export:";
            // 
            // cmbOutputSortBy
            // 
            this.cmbOutputSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputSortBy.FormattingEnabled = true;
            this.cmbOutputSortBy.Items.AddRange(new object[] {
            "Name",
            "Overall"});
            this.cmbOutputSortBy.Location = new System.Drawing.Point(61, 10);
            this.cmbOutputSortBy.Name = "cmbOutputSortBy";
            this.cmbOutputSortBy.Size = new System.Drawing.Size(177, 21);
            this.cmbOutputSortBy.TabIndex = 0;
            this.cmbOutputSortBy.SelectedIndexChanged += new System.EventHandler(this.cmbOutputSortBy_SelectedIndexChanged);
            // 
            // lblOutputSortBy
            // 
            this.lblOutputSortBy.AutoSize = true;
            this.lblOutputSortBy.Location = new System.Drawing.Point(12, 12);
            this.lblOutputSortBy.Name = "lblOutputSortBy";
            this.lblOutputSortBy.Size = new System.Drawing.Size(43, 13);
            this.lblOutputSortBy.TabIndex = 0;
            this.lblOutputSortBy.Text = "Sort by:";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grbBenchmark);
            this.splitContainer.Panel1.Controls.Add(this.pnlGameSaves);
            this.splitContainer.Panel1.Controls.Add(this.pnlHumanTactics);
            this.splitContainer.Panel1MinSize = 265;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlOutput);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(952, 561);
            this.splitContainer.SplitterDistance = 265;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.TabStop = false;
            // 
            // grbBenchmark
            // 
            this.grbBenchmark.Controls.Add(this.chkMakePlayersAllPositioners);
            this.grbBenchmark.Controls.Add(this.chkHideCMWindow);
            this.grbBenchmark.Controls.Add(this.lblBMRunningCount);
            this.grbBenchmark.Controls.Add(this.lblBMDoneCount);
            this.grbBenchmark.Controls.Add(this.lblBMRunningCountCaption);
            this.grbBenchmark.Controls.Add(this.lblBMDoneCountCaption);
            this.grbBenchmark.Controls.Add(this.btnCMFolderDlg);
            this.grbBenchmark.Controls.Add(this.prgBenchmark);
            this.grbBenchmark.Controls.Add(this.btnStart);
            this.grbBenchmark.Controls.Add(this.txtThreadTimeout);
            this.grbBenchmark.Controls.Add(this.txtThreadCount);
            this.grbBenchmark.Controls.Add(this.txtTestCount);
            this.grbBenchmark.Controls.Add(this.txtCmFolder);
            this.grbBenchmark.Controls.Add(this.lblThreadTimeout);
            this.grbBenchmark.Controls.Add(this.lblThreadCount);
            this.grbBenchmark.Controls.Add(this.lblTestCount);
            this.grbBenchmark.Controls.Add(this.lblExeFilename);
            this.grbBenchmark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbBenchmark.Location = new System.Drawing.Point(577, 0);
            this.grbBenchmark.Name = "grbBenchmark";
            this.grbBenchmark.Size = new System.Drawing.Size(371, 261);
            this.grbBenchmark.TabIndex = 36;
            this.grbBenchmark.TabStop = false;
            this.grbBenchmark.Text = "Benchmarking";
            // 
            // chkMakePlayersAllPositioners
            // 
            this.chkMakePlayersAllPositioners.AutoSize = true;
            this.chkMakePlayersAllPositioners.Location = new System.Drawing.Point(9, 71);
            this.chkMakePlayersAllPositioners.Name = "chkMakePlayersAllPositioners";
            this.chkMakePlayersAllPositioners.Size = new System.Drawing.Size(155, 17);
            this.chkMakePlayersAllPositioners.TabIndex = 4;
            this.chkMakePlayersAllPositioners.Text = "Make players all positioners";
            this.chkMakePlayersAllPositioners.UseVisualStyleBackColor = true;
            // 
            // chkHideCMWindow
            // 
            this.chkHideCMWindow.AutoSize = true;
            this.chkHideCMWindow.Location = new System.Drawing.Point(9, 176);
            this.chkHideCMWindow.Name = "chkHideCMWindow";
            this.chkHideCMWindow.Size = new System.Drawing.Size(111, 17);
            this.chkHideCMWindow.TabIndex = 8;
            this.chkHideCMWindow.Text = "Hide CM windows";
            this.chkHideCMWindow.UseVisualStyleBackColor = true;
            // 
            // lblBMRunningCount
            // 
            this.lblBMRunningCount.AutoSize = true;
            this.lblBMRunningCount.Location = new System.Drawing.Point(203, 236);
            this.lblBMRunningCount.Name = "lblBMRunningCount";
            this.lblBMRunningCount.Size = new System.Drawing.Size(13, 13);
            this.lblBMRunningCount.TabIndex = 17;
            this.lblBMRunningCount.Text = "0";
            // 
            // lblBMDoneCount
            // 
            this.lblBMDoneCount.AutoSize = true;
            this.lblBMDoneCount.Location = new System.Drawing.Point(48, 236);
            this.lblBMDoneCount.Name = "lblBMDoneCount";
            this.lblBMDoneCount.Size = new System.Drawing.Size(30, 13);
            this.lblBMDoneCount.TabIndex = 16;
            this.lblBMDoneCount.Text = "0 / 0";
            // 
            // lblBMRunningCountCaption
            // 
            this.lblBMRunningCountCaption.AutoSize = true;
            this.lblBMRunningCountCaption.Location = new System.Drawing.Point(147, 236);
            this.lblBMRunningCountCaption.Name = "lblBMRunningCountCaption";
            this.lblBMRunningCountCaption.Size = new System.Drawing.Size(50, 13);
            this.lblBMRunningCountCaption.TabIndex = 15;
            this.lblBMRunningCountCaption.Text = "Running:";
            // 
            // lblBMDoneCountCaption
            // 
            this.lblBMDoneCountCaption.AutoSize = true;
            this.lblBMDoneCountCaption.Location = new System.Drawing.Point(6, 236);
            this.lblBMDoneCountCaption.Name = "lblBMDoneCountCaption";
            this.lblBMDoneCountCaption.Size = new System.Drawing.Size(36, 13);
            this.lblBMDoneCountCaption.TabIndex = 14;
            this.lblBMDoneCountCaption.Text = "Done:";
            // 
            // btnCMFolderDlg
            // 
            this.btnCMFolderDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCMFolderDlg.ImageIndex = 4;
            this.btnCMFolderDlg.ImageList = this.imageList;
            this.btnCMFolderDlg.Location = new System.Drawing.Point(342, 40);
            this.btnCMFolderDlg.Name = "btnCMFolderDlg";
            this.btnCMFolderDlg.Size = new System.Drawing.Size(23, 22);
            this.btnCMFolderDlg.TabIndex = 1;
            this.btnCMFolderDlg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnCMFolderDlg, "Select CM BM executable");
            this.btnCMFolderDlg.UseVisualStyleBackColor = true;
            this.btnCMFolderDlg.Click += new System.EventHandler(this.btnCMFolderDlg_Click);
            // 
            // prgBenchmark
            // 
            this.prgBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBenchmark.Location = new System.Drawing.Point(9, 205);
            this.prgBenchmark.Name = "prgBenchmark";
            this.prgBenchmark.Size = new System.Drawing.Size(294, 23);
            this.prgBenchmark.TabIndex = 9;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.ImageIndex = 3;
            this.btnStart.ImageList = this.imageList;
            this.btnStart.Location = new System.Drawing.Point(309, 203);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(59, 25);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnStart, "Start benchmarking");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtThreadTimeout
            // 
            this.txtThreadTimeout.Location = new System.Drawing.Point(139, 146);
            this.txtThreadTimeout.Name = "txtThreadTimeout";
            this.txtThreadTimeout.Size = new System.Drawing.Size(122, 20);
            this.txtThreadTimeout.TabIndex = 7;
            this.txtThreadTimeout.Text = "120";
            // 
            // txtThreadCount
            // 
            this.txtThreadCount.Location = new System.Drawing.Point(159, 121);
            this.txtThreadCount.Name = "txtThreadCount";
            this.txtThreadCount.Size = new System.Drawing.Size(102, 20);
            this.txtThreadCount.TabIndex = 6;
            this.txtThreadCount.Text = "1";
            // 
            // txtTestCount
            // 
            this.txtTestCount.Location = new System.Drawing.Point(165, 96);
            this.txtTestCount.Name = "txtTestCount";
            this.txtTestCount.Size = new System.Drawing.Size(96, 20);
            this.txtTestCount.TabIndex = 5;
            this.txtTestCount.Text = "1";
            // 
            // txtCmFolder
            // 
            this.txtCmFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmFolder.Location = new System.Drawing.Point(9, 42);
            this.txtCmFolder.Name = "txtCmFolder";
            this.txtCmFolder.ReadOnly = true;
            this.txtCmFolder.Size = new System.Drawing.Size(327, 20);
            this.txtCmFolder.TabIndex = 0;
            this.txtCmFolder.TabStop = false;
            // 
            // lblThreadTimeout
            // 
            this.lblThreadTimeout.AutoSize = true;
            this.lblThreadTimeout.Location = new System.Drawing.Point(6, 149);
            this.lblThreadTimeout.Name = "lblThreadTimeout";
            this.lblThreadTimeout.Size = new System.Drawing.Size(127, 13);
            this.lblThreadTimeout.TabIndex = 5;
            this.lblThreadTimeout.Text = "Season run timeout (sec):";
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Location = new System.Drawing.Point(6, 124);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(147, 13);
            this.lblThreadCount.TabIndex = 4;
            this.lblThreadCount.Text = "Max parallel processes to run:";
            // 
            // lblTestCount
            // 
            this.lblTestCount.AutoSize = true;
            this.lblTestCount.Location = new System.Drawing.Point(6, 99);
            this.lblTestCount.Name = "lblTestCount";
            this.lblTestCount.Size = new System.Drawing.Size(153, 13);
            this.lblTestCount.TabIndex = 3;
            this.lblTestCount.Text = "Seasons to run per tactics pair:";
            // 
            // lblExeFilename
            // 
            this.lblExeFilename.AutoSize = true;
            this.lblExeFilename.Location = new System.Drawing.Point(6, 26);
            this.lblExeFilename.Name = "lblExeFilename";
            this.lblExeFilename.Size = new System.Drawing.Size(66, 13);
            this.lblExeFilename.TabIndex = 0;
            this.lblExeFilename.Text = "CM location:";
            // 
            // pnlGameSaves
            // 
            this.pnlGameSaves.Controls.Add(this.btnGameSaveAddFolder);
            this.pnlGameSaves.Controls.Add(this.btnGameSaveAddFiles);
            this.pnlGameSaves.Controls.Add(this.lblGameSavesTotal);
            this.pnlGameSaves.Controls.Add(this.btnGameSaveDelete);
            this.pnlGameSaves.Controls.Add(this.btnGameSaveClear);
            this.pnlGameSaves.Controls.Add(this.lblAITactics);
            this.pnlGameSaves.Controls.Add(this.lstGameSaves);
            this.pnlGameSaves.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGameSaves.Location = new System.Drawing.Point(285, 0);
            this.pnlGameSaves.Name = "pnlGameSaves";
            this.pnlGameSaves.Size = new System.Drawing.Size(292, 261);
            this.pnlGameSaves.TabIndex = 38;
            // 
            // btnGameSaveAddFolder
            // 
            this.btnGameSaveAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGameSaveAddFolder.ImageIndex = 0;
            this.btnGameSaveAddFolder.ImageList = this.imageList;
            this.btnGameSaveAddFolder.Location = new System.Drawing.Point(174, 233);
            this.btnGameSaveAddFolder.Name = "btnGameSaveAddFolder";
            this.btnGameSaveAddFolder.Size = new System.Drawing.Size(57, 23);
            this.btnGameSaveAddFolder.TabIndex = 3;
            this.btnGameSaveAddFolder.Text = "folder";
            this.btnGameSaveAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnGameSaveAddFolder, "Add from folder");
            this.btnGameSaveAddFolder.UseVisualStyleBackColor = true;
            this.btnGameSaveAddFolder.Click += new System.EventHandler(this.btnGameSaveAddFolder_Click);
            // 
            // btnGameSaveAddFiles
            // 
            this.btnGameSaveAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGameSaveAddFiles.ImageIndex = 0;
            this.btnGameSaveAddFiles.ImageList = this.imageList;
            this.btnGameSaveAddFiles.Location = new System.Drawing.Point(113, 233);
            this.btnGameSaveAddFiles.Name = "btnGameSaveAddFiles";
            this.btnGameSaveAddFiles.Size = new System.Drawing.Size(55, 23);
            this.btnGameSaveAddFiles.TabIndex = 2;
            this.btnGameSaveAddFiles.Text = ".sav";
            this.btnGameSaveAddFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnGameSaveAddFiles, "Add .sav file");
            this.btnGameSaveAddFiles.UseVisualStyleBackColor = true;
            this.btnGameSaveAddFiles.Click += new System.EventHandler(this.btnGameSaveAddFiles_Click);
            // 
            // lblGameSavesTotal
            // 
            this.lblGameSavesTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGameSavesTotal.AutoSize = true;
            this.lblGameSavesTotal.Location = new System.Drawing.Point(3, 238);
            this.lblGameSavesTotal.Name = "lblGameSavesTotal";
            this.lblGameSavesTotal.Size = new System.Drawing.Size(13, 13);
            this.lblGameSavesTotal.TabIndex = 45;
            this.lblGameSavesTotal.Text = "0";
            this.toolTip.SetToolTip(this.lblGameSavesTotal, "Quantity");
            // 
            // btnGameSaveDelete
            // 
            this.btnGameSaveDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGameSaveDelete.ImageIndex = 2;
            this.btnGameSaveDelete.ImageList = this.imageList;
            this.btnGameSaveDelete.Location = new System.Drawing.Point(237, 233);
            this.btnGameSaveDelete.Name = "btnGameSaveDelete";
            this.btnGameSaveDelete.Size = new System.Drawing.Size(22, 22);
            this.btnGameSaveDelete.TabIndex = 5;
            this.btnGameSaveDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnGameSaveDelete, "Remove selected");
            this.btnGameSaveDelete.UseVisualStyleBackColor = true;
            this.btnGameSaveDelete.Click += new System.EventHandler(this.btnGameSaveDelete_Click);
            // 
            // btnGameSaveClear
            // 
            this.btnGameSaveClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGameSaveClear.ImageIndex = 1;
            this.btnGameSaveClear.ImageList = this.imageList;
            this.btnGameSaveClear.Location = new System.Drawing.Point(265, 233);
            this.btnGameSaveClear.Name = "btnGameSaveClear";
            this.btnGameSaveClear.Size = new System.Drawing.Size(22, 22);
            this.btnGameSaveClear.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnGameSaveClear, "Remove all");
            this.btnGameSaveClear.UseVisualStyleBackColor = true;
            this.btnGameSaveClear.Click += new System.EventHandler(this.btnGameSavesClear_Click);
            // 
            // lblAITactics
            // 
            this.lblAITactics.AutoSize = true;
            this.lblAITactics.Location = new System.Drawing.Point(3, 6);
            this.lblAITactics.Name = "lblAITactics";
            this.lblAITactics.Size = new System.Drawing.Size(101, 13);
            this.lblAITactics.TabIndex = 31;
            this.lblAITactics.Text = "Game saves to test:";
            // 
            // lstGameSaves
            // 
            this.lstGameSaves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstGameSaves.FormattingEnabled = true;
            this.lstGameSaves.Location = new System.Drawing.Point(3, 26);
            this.lstGameSaves.Name = "lstGameSaves";
            this.lstGameSaves.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstGameSaves.Size = new System.Drawing.Size(284, 199);
            this.lstGameSaves.TabIndex = 1;
            this.lstGameSaves.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstGameSaves_KeyUp);
            // 
            // pnlHumanTactics
            // 
            this.pnlHumanTactics.Controls.Add(this.btnHumanAddFolder);
            this.pnlHumanTactics.Controls.Add(this.btnHumanAddFiles);
            this.pnlHumanTactics.Controls.Add(this.lblHumanTacticsTotal);
            this.pnlHumanTactics.Controls.Add(this.lstHumanTactics);
            this.pnlHumanTactics.Controls.Add(this.lblHumanTactics);
            this.pnlHumanTactics.Controls.Add(this.btnHumanDelete);
            this.pnlHumanTactics.Controls.Add(this.btnHumanClear);
            this.pnlHumanTactics.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHumanTactics.Location = new System.Drawing.Point(0, 0);
            this.pnlHumanTactics.Name = "pnlHumanTactics";
            this.pnlHumanTactics.Size = new System.Drawing.Size(285, 261);
            this.pnlHumanTactics.TabIndex = 37;
            // 
            // btnHumanAddFolder
            // 
            this.btnHumanAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanAddFolder.ImageIndex = 0;
            this.btnHumanAddFolder.ImageList = this.imageList;
            this.btnHumanAddFolder.Location = new System.Drawing.Point(167, 233);
            this.btnHumanAddFolder.Name = "btnHumanAddFolder";
            this.btnHumanAddFolder.Size = new System.Drawing.Size(57, 23);
            this.btnHumanAddFolder.TabIndex = 2;
            this.btnHumanAddFolder.Text = "folder";
            this.btnHumanAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnHumanAddFolder, "Add from folder");
            this.btnHumanAddFolder.UseVisualStyleBackColor = true;
            this.btnHumanAddFolder.Click += new System.EventHandler(this.btnHumanAddFolder_Click);
            // 
            // btnHumanAddFiles
            // 
            this.btnHumanAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanAddFiles.ImageIndex = 0;
            this.btnHumanAddFiles.ImageList = this.imageList;
            this.btnHumanAddFiles.Location = new System.Drawing.Point(114, 233);
            this.btnHumanAddFiles.Name = "btnHumanAddFiles";
            this.btnHumanAddFiles.Size = new System.Drawing.Size(47, 23);
            this.btnHumanAddFiles.TabIndex = 1;
            this.btnHumanAddFiles.Text = ".tct";
            this.btnHumanAddFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnHumanAddFiles, "Add from .tct / .pct files");
            this.btnHumanAddFiles.UseVisualStyleBackColor = true;
            this.btnHumanAddFiles.Click += new System.EventHandler(this.btnHumanAddFiles_Click);
            // 
            // lblHumanTacticsTotal
            // 
            this.lblHumanTacticsTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHumanTacticsTotal.AutoSize = true;
            this.lblHumanTacticsTotal.Location = new System.Drawing.Point(12, 238);
            this.lblHumanTacticsTotal.Name = "lblHumanTacticsTotal";
            this.lblHumanTacticsTotal.Size = new System.Drawing.Size(13, 13);
            this.lblHumanTacticsTotal.TabIndex = 39;
            this.lblHumanTacticsTotal.Text = "0";
            this.toolTip.SetToolTip(this.lblHumanTacticsTotal, "Quantity");
            // 
            // lstHumanTactics
            // 
            this.lstHumanTactics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstHumanTactics.FormattingEnabled = true;
            this.lstHumanTactics.Location = new System.Drawing.Point(12, 26);
            this.lstHumanTactics.Name = "lstHumanTactics";
            this.lstHumanTactics.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstHumanTactics.Size = new System.Drawing.Size(267, 199);
            this.lstHumanTactics.TabIndex = 0;
            this.lstHumanTactics.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstHumanTactics_KeyUp);
            // 
            // lblHumanTactics
            // 
            this.lblHumanTactics.AutoSize = true;
            this.lblHumanTactics.Location = new System.Drawing.Point(9, 6);
            this.lblHumanTactics.Name = "lblHumanTactics";
            this.lblHumanTactics.Size = new System.Drawing.Size(110, 13);
            this.lblHumanTactics.TabIndex = 35;
            this.lblHumanTactics.Text = "Human tactics to test:";
            // 
            // btnHumanDelete
            // 
            this.btnHumanDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanDelete.ImageIndex = 2;
            this.btnHumanDelete.ImageList = this.imageList;
            this.btnHumanDelete.Location = new System.Drawing.Point(230, 233);
            this.btnHumanDelete.Name = "btnHumanDelete";
            this.btnHumanDelete.Size = new System.Drawing.Size(22, 22);
            this.btnHumanDelete.TabIndex = 4;
            this.btnHumanDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnHumanDelete, "Remove selected");
            this.btnHumanDelete.UseVisualStyleBackColor = true;
            this.btnHumanDelete.Click += new System.EventHandler(this.btnHumanDelete_Click);
            // 
            // btnHumanClear
            // 
            this.btnHumanClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanClear.ImageIndex = 1;
            this.btnHumanClear.ImageList = this.imageList;
            this.btnHumanClear.Location = new System.Drawing.Point(258, 233);
            this.btnHumanClear.Name = "btnHumanClear";
            this.btnHumanClear.Size = new System.Drawing.Size(22, 22);
            this.btnHumanClear.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnHumanClear, "Remove all");
            this.btnHumanClear.UseVisualStyleBackColor = true;
            this.btnHumanClear.Click += new System.EventHandler(this.btnHumanClear_Click);
            // 
            // dlgGamesSavesFolder
            // 
            this.dlgGamesSavesFolder.Description = "Select folder with .sav files";
            // 
            // dlgLoadResults
            // 
            this.dlgLoadResults.Filter = "Benchmark result files|*.json|All files|*.*";
            this.dlgLoadResults.Title = "Load benchmark results";
            // 
            // dlgSaveResults
            // 
            this.dlgSaveResults.Filter = "Benchmark result files|*.json|All files|*.*";
            this.dlgSaveResults.RestoreDirectory = true;
            this.dlgSaveResults.Title = "Save benchmark results";
            // 
            // dlgExportResultsToHtml
            // 
            this.dlgExportResultsToHtml.Filter = "HTML files|*.html|All files|*.*";
            this.dlgExportResultsToHtml.RestoreDirectory = true;
            this.dlgExportResultsToHtml.Title = "Export benchmark results";
            // 
            // dlgExportResultsToExcel
            // 
            this.dlgExportResultsToExcel.Filter = "HTML files|*.html|All files|*.*";
            this.dlgExportResultsToExcel.RestoreDirectory = true;
            this.dlgExportResultsToExcel.Title = "Export benchmark results";
            // 
            // dlgGameSaveFiles
            // 
            this.dlgGameSaveFiles.Filter = "CM save game files|*.sav|All files|*.*";
            this.dlgGameSaveFiles.Multiselect = true;
            this.dlgGameSaveFiles.Title = "Pick .tct / .pct files";
            // 
            // dlgCmFolder
            // 
            this.dlgCmFolder.Description = "Select CM folder";
            // 
            // dlgTacticsFolder
            // 
            this.dlgTacticsFolder.Description = "Select folder with .pct and .tct files";
            // 
            // dlgTacticFiles
            // 
            this.dlgTacticFiles.Filter = "CM tactic files|*.tct;*.pct|All files|*.*";
            this.dlgTacticFiles.Multiselect = true;
            this.dlgTacticFiles.Title = "Pick .tct / .pct files";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(952, 561);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(968, 600);
            this.Name = "MainForm";
            this.Text = "Tactics Benchmarker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutputGrid.ResumeLayout(false);
            this.pnlOutputControl.ResumeLayout(false);
            this.pnlOutputControl.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grbBenchmark.ResumeLayout(false);
            this.grbBenchmark.PerformLayout();
            this.pnlGameSaves.ResumeLayout(false);
            this.pnlGameSaves.PerformLayout();
            this.pnlHumanTactics.ResumeLayout(false);
            this.pnlHumanTactics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Panel pnlOutputGrid;
        private System.Windows.Forms.WebBrowser webGrid;
        private System.Windows.Forms.FolderBrowserDialog dlgGamesSavesFolder;
        private System.Windows.Forms.OpenFileDialog dlgLoadResults;
        private System.Windows.Forms.SaveFileDialog dlgSaveResults;
        private System.Windows.Forms.SaveFileDialog dlgExportResultsToHtml;
        private System.Windows.Forms.SaveFileDialog dlgExportResultsToExcel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grbBenchmark;
        private System.Windows.Forms.CheckBox chkHideCMWindow;
        private System.Windows.Forms.Label lblBMRunningCount;
        private System.Windows.Forms.Label lblBMDoneCount;
        private System.Windows.Forms.Label lblBMRunningCountCaption;
        private System.Windows.Forms.Label lblBMDoneCountCaption;
        private System.Windows.Forms.Button btnCMFolderDlg;
        private System.Windows.Forms.ProgressBar prgBenchmark;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtThreadTimeout;
        private System.Windows.Forms.TextBox txtThreadCount;
        private System.Windows.Forms.TextBox txtTestCount;
        private System.Windows.Forms.TextBox txtCmFolder;
        private System.Windows.Forms.Label lblThreadTimeout;
        private System.Windows.Forms.Label lblThreadCount;
        private System.Windows.Forms.Label lblTestCount;
        private System.Windows.Forms.Label lblExeFilename;
        private System.Windows.Forms.Panel pnlGameSaves;
        private System.Windows.Forms.Button btnGameSaveAddFolder;
        private System.Windows.Forms.Button btnGameSaveAddFiles;
        private System.Windows.Forms.Label lblGameSavesTotal;
        private System.Windows.Forms.Button btnGameSaveDelete;
        private System.Windows.Forms.Button btnGameSaveClear;
        private System.Windows.Forms.Label lblAITactics;
        private System.Windows.Forms.ListBox lstGameSaves;
        private System.Windows.Forms.Panel pnlHumanTactics;
        private System.Windows.Forms.Button btnHumanAddFolder;
        private System.Windows.Forms.Button btnHumanAddFiles;
        private System.Windows.Forms.Label lblHumanTacticsTotal;
        private System.Windows.Forms.ListBox lstHumanTactics;
        private System.Windows.Forms.Label lblHumanTactics;
        private System.Windows.Forms.Button btnHumanDelete;
        private System.Windows.Forms.Button btnHumanClear;
        private System.Windows.Forms.Panel pnlOutputControl;
        private System.Windows.Forms.Button btnOutputExportExcel;
        private System.Windows.Forms.Button btnOutputExportHtml;
        private System.Windows.Forms.Button btnOutputSave;
        private System.Windows.Forms.Button btnOutputLoad;
        private System.Windows.Forms.Label lblOutputExport;
        private System.Windows.Forms.ComboBox cmbOutputSortBy;
        private System.Windows.Forms.Label lblOutputSortBy;
        private System.Windows.Forms.OpenFileDialog dlgGameSaveFiles;
        private System.Windows.Forms.FolderBrowserDialog dlgCmFolder;
        private System.Windows.Forms.RadioButton rdbHumanTacticsInCols;
        private System.Windows.Forms.RadioButton rdbHumanTacticsInRows;
        private System.Windows.Forms.Label lblHumanTacticsInRowsCaption;
        private System.Windows.Forms.CheckBox chkMakePlayersAllPositioners;
        private System.Windows.Forms.FolderBrowserDialog dlgTacticsFolder;
        private System.Windows.Forms.OpenFileDialog dlgTacticFiles;
    }
}

