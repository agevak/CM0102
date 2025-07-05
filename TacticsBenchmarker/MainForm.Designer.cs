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
            this.dlgAddSavFilename = new System.Windows.Forms.OpenFileDialog();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.pnlOutputGrid = new System.Windows.Forms.Panel();
            this.webGrid = new System.Windows.Forms.WebBrowser();
            this.pnlOutputControl = new System.Windows.Forms.Panel();
            this.btnOutputExportExcel = new System.Windows.Forms.Button();
            this.btnOutputExportHtml = new System.Windows.Forms.Button();
            this.btnOutputSave = new System.Windows.Forms.Button();
            this.btnOutputLoad = new System.Windows.Forms.Button();
            this.lblOutputExport = new System.Windows.Forms.Label();
            this.cmbOutputSortBy = new System.Windows.Forms.ComboBox();
            this.lblOutputSortBy = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grbBenchmark = new System.Windows.Forms.GroupBox();
            this.chkHideCMWindow = new System.Windows.Forms.CheckBox();
            this.lblBMRunningCount = new System.Windows.Forms.Label();
            this.lblBMDoneCount = new System.Windows.Forms.Label();
            this.lblBMRunningCountCaption = new System.Windows.Forms.Label();
            this.lblBMDoneCountCaption = new System.Windows.Forms.Label();
            this.btnBMSavFilenameDlg = new System.Windows.Forms.Button();
            this.txtBMSavFilename = new System.Windows.Forms.TextBox();
            this.lblBMSavFilename = new System.Windows.Forms.Label();
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
            this.pnlAITactics = new System.Windows.Forms.Panel();
            this.chkDontReplaceAITactics = new System.Windows.Forms.CheckBox();
            this.btnAIAddSav = new System.Windows.Forms.Button();
            this.btnAIAddFolder = new System.Windows.Forms.Button();
            this.btnAIAddFiles = new System.Windows.Forms.Button();
            this.lblAITacticsTotal = new System.Windows.Forms.Label();
            this.btnAIDelete = new System.Windows.Forms.Button();
            this.btnAIClear = new System.Windows.Forms.Button();
            this.lblAITactics = new System.Windows.Forms.Label();
            this.lstAITactics = new System.Windows.Forms.ListBox();
            this.pnlHumanTactics = new System.Windows.Forms.Panel();
            this.btnHumanAddSav = new System.Windows.Forms.Button();
            this.btnHumanAddFolder = new System.Windows.Forms.Button();
            this.btnHumanAddFiles = new System.Windows.Forms.Button();
            this.lblHumanTacticsTotal = new System.Windows.Forms.Label();
            this.lstHumanTactics = new System.Windows.Forms.ListBox();
            this.lblHumanTactics = new System.Windows.Forms.Label();
            this.btnHumanDelete = new System.Windows.Forms.Button();
            this.btnHumanClear = new System.Windows.Forms.Button();
            this.dlgTacticsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgLoadResults = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveResults = new System.Windows.Forms.SaveFileDialog();
            this.dlgExportResultsToHtml = new System.Windows.Forms.SaveFileDialog();
            this.dlgExportResultsToExcel = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dlgTacticFiles = new System.Windows.Forms.OpenFileDialog();
            this.dlgBmSavFilename = new System.Windows.Forms.OpenFileDialog();
            this.dlgCmFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlOutput.SuspendLayout();
            this.pnlOutputGrid.SuspendLayout();
            this.pnlOutputControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grbBenchmark.SuspendLayout();
            this.pnlAITactics.SuspendLayout();
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
            // dlgAddSavFilename
            // 
            this.dlgAddSavFilename.FileName = "a_benchmark.sav";
            this.dlgAddSavFilename.Filter = "CM save files|*.sav|All files|*.*";
            this.dlgAddSavFilename.Title = "Pick .sav file";
            // 
            // pnlOutput
            // 
            this.pnlOutput.Controls.Add(this.pnlOutputGrid);
            this.pnlOutput.Controls.Add(this.pnlOutputControl);
            this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutput.Location = new System.Drawing.Point(0, 0);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(948, 238);
            this.pnlOutput.TabIndex = 3;
            // 
            // pnlOutputGrid
            // 
            this.pnlOutputGrid.Controls.Add(this.webGrid);
            this.pnlOutputGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputGrid.Location = new System.Drawing.Point(0, 42);
            this.pnlOutputGrid.Name = "pnlOutputGrid";
            this.pnlOutputGrid.Size = new System.Drawing.Size(948, 196);
            this.pnlOutputGrid.TabIndex = 1;
            // 
            // webGrid
            // 
            this.webGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webGrid.Location = new System.Drawing.Point(0, 0);
            this.webGrid.MinimumSize = new System.Drawing.Size(20, 20);
            this.webGrid.Name = "webGrid";
            this.webGrid.Size = new System.Drawing.Size(948, 196);
            this.webGrid.TabIndex = 0;
            // 
            // pnlOutputControl
            // 
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
            // btnOutputExportExcel
            // 
            this.btnOutputExportExcel.ImageList = this.imageList;
            this.btnOutputExportExcel.Location = new System.Drawing.Point(586, 7);
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
            this.btnOutputExportHtml.Location = new System.Drawing.Point(523, 6);
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
            this.btnOutputSave.Location = new System.Drawing.Point(366, 6);
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
            this.btnOutputLoad.Location = new System.Drawing.Point(285, 6);
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
            this.lblOutputExport.Location = new System.Drawing.Point(476, 12);
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
            this.cmbOutputSortBy.TabIndex = 1;
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
            this.splitContainer.Panel1.Controls.Add(this.pnlAITactics);
            this.splitContainer.Panel1.Controls.Add(this.pnlHumanTactics);
            this.splitContainer.Panel1MinSize = 315;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlOutput);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(952, 561);
            this.splitContainer.SplitterDistance = 315;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.TabStop = false;
            // 
            // grbBenchmark
            // 
            this.grbBenchmark.Controls.Add(this.chkHideCMWindow);
            this.grbBenchmark.Controls.Add(this.lblBMRunningCount);
            this.grbBenchmark.Controls.Add(this.lblBMDoneCount);
            this.grbBenchmark.Controls.Add(this.lblBMRunningCountCaption);
            this.grbBenchmark.Controls.Add(this.lblBMDoneCountCaption);
            this.grbBenchmark.Controls.Add(this.btnBMSavFilenameDlg);
            this.grbBenchmark.Controls.Add(this.txtBMSavFilename);
            this.grbBenchmark.Controls.Add(this.lblBMSavFilename);
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
            this.grbBenchmark.Size = new System.Drawing.Size(371, 311);
            this.grbBenchmark.TabIndex = 36;
            this.grbBenchmark.TabStop = false;
            this.grbBenchmark.Text = "Benchmarking";
            // 
            // chkHideCMWindow
            // 
            this.chkHideCMWindow.AutoSize = true;
            this.chkHideCMWindow.Location = new System.Drawing.Point(9, 225);
            this.chkHideCMWindow.Name = "chkHideCMWindow";
            this.chkHideCMWindow.Size = new System.Drawing.Size(111, 17);
            this.chkHideCMWindow.TabIndex = 18;
            this.chkHideCMWindow.Text = "Hide CM windows";
            this.chkHideCMWindow.UseVisualStyleBackColor = true;
            // 
            // lblBMRunningCount
            // 
            this.lblBMRunningCount.AutoSize = true;
            this.lblBMRunningCount.Location = new System.Drawing.Point(203, 285);
            this.lblBMRunningCount.Name = "lblBMRunningCount";
            this.lblBMRunningCount.Size = new System.Drawing.Size(13, 13);
            this.lblBMRunningCount.TabIndex = 17;
            this.lblBMRunningCount.Text = "0";
            // 
            // lblBMDoneCount
            // 
            this.lblBMDoneCount.AutoSize = true;
            this.lblBMDoneCount.Location = new System.Drawing.Point(48, 285);
            this.lblBMDoneCount.Name = "lblBMDoneCount";
            this.lblBMDoneCount.Size = new System.Drawing.Size(30, 13);
            this.lblBMDoneCount.TabIndex = 16;
            this.lblBMDoneCount.Text = "0 / 0";
            // 
            // lblBMRunningCountCaption
            // 
            this.lblBMRunningCountCaption.AutoSize = true;
            this.lblBMRunningCountCaption.Location = new System.Drawing.Point(147, 285);
            this.lblBMRunningCountCaption.Name = "lblBMRunningCountCaption";
            this.lblBMRunningCountCaption.Size = new System.Drawing.Size(50, 13);
            this.lblBMRunningCountCaption.TabIndex = 15;
            this.lblBMRunningCountCaption.Text = "Running:";
            // 
            // lblBMDoneCountCaption
            // 
            this.lblBMDoneCountCaption.AutoSize = true;
            this.lblBMDoneCountCaption.Location = new System.Drawing.Point(6, 285);
            this.lblBMDoneCountCaption.Name = "lblBMDoneCountCaption";
            this.lblBMDoneCountCaption.Size = new System.Drawing.Size(36, 13);
            this.lblBMDoneCountCaption.TabIndex = 14;
            this.lblBMDoneCountCaption.Text = "Done:";
            // 
            // btnBMSavFilenameDlg
            // 
            this.btnBMSavFilenameDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBMSavFilenameDlg.AutoSize = true;
            this.btnBMSavFilenameDlg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBMSavFilenameDlg.ImageIndex = 4;
            this.btnBMSavFilenameDlg.ImageList = this.imageList;
            this.btnBMSavFilenameDlg.Location = new System.Drawing.Point(343, 84);
            this.btnBMSavFilenameDlg.Name = "btnBMSavFilenameDlg";
            this.btnBMSavFilenameDlg.Size = new System.Drawing.Size(22, 22);
            this.btnBMSavFilenameDlg.TabIndex = 13;
            this.btnBMSavFilenameDlg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnBMSavFilenameDlg, "Select game save file");
            this.btnBMSavFilenameDlg.UseVisualStyleBackColor = true;
            this.btnBMSavFilenameDlg.Click += new System.EventHandler(this.btnBMSavFilenameDlg_Click);
            // 
            // txtBMSavFilename
            // 
            this.txtBMSavFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBMSavFilename.Location = new System.Drawing.Point(9, 86);
            this.txtBMSavFilename.Name = "txtBMSavFilename";
            this.txtBMSavFilename.ReadOnly = true;
            this.txtBMSavFilename.Size = new System.Drawing.Size(327, 20);
            this.txtBMSavFilename.TabIndex = 11;
            this.txtBMSavFilename.TabStop = false;
            // 
            // lblBMSavFilename
            // 
            this.lblBMSavFilename.AutoSize = true;
            this.lblBMSavFilename.Location = new System.Drawing.Point(6, 70);
            this.lblBMSavFilename.Name = "lblBMSavFilename";
            this.lblBMSavFilename.Size = new System.Drawing.Size(86, 13);
            this.lblBMSavFilename.TabIndex = 12;
            this.lblBMSavFilename.Text = ".sav file location:";
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
            this.prgBenchmark.Location = new System.Drawing.Point(9, 254);
            this.prgBenchmark.Name = "prgBenchmark";
            this.prgBenchmark.Size = new System.Drawing.Size(294, 23);
            this.prgBenchmark.TabIndex = 9;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.ImageIndex = 3;
            this.btnStart.ImageList = this.imageList;
            this.btnStart.Location = new System.Drawing.Point(309, 252);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(59, 25);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnStart, "Start benchmarking");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtThreadTimeout
            // 
            this.txtThreadTimeout.Location = new System.Drawing.Point(139, 195);
            this.txtThreadTimeout.Name = "txtThreadTimeout";
            this.txtThreadTimeout.Size = new System.Drawing.Size(122, 20);
            this.txtThreadTimeout.TabIndex = 7;
            this.txtThreadTimeout.Text = "120";
            // 
            // txtThreadCount
            // 
            this.txtThreadCount.Location = new System.Drawing.Point(159, 170);
            this.txtThreadCount.Name = "txtThreadCount";
            this.txtThreadCount.Size = new System.Drawing.Size(102, 20);
            this.txtThreadCount.TabIndex = 6;
            this.txtThreadCount.Text = "1";
            // 
            // txtTestCount
            // 
            this.txtTestCount.Location = new System.Drawing.Point(165, 145);
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
            this.lblThreadTimeout.Location = new System.Drawing.Point(6, 198);
            this.lblThreadTimeout.Name = "lblThreadTimeout";
            this.lblThreadTimeout.Size = new System.Drawing.Size(127, 13);
            this.lblThreadTimeout.TabIndex = 5;
            this.lblThreadTimeout.Text = "Season run timeout (sec):";
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Location = new System.Drawing.Point(6, 173);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(147, 13);
            this.lblThreadCount.TabIndex = 4;
            this.lblThreadCount.Text = "Max parallel processes to run:";
            // 
            // lblTestCount
            // 
            this.lblTestCount.AutoSize = true;
            this.lblTestCount.Location = new System.Drawing.Point(6, 148);
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
            // pnlAITactics
            // 
            this.pnlAITactics.Controls.Add(this.chkDontReplaceAITactics);
            this.pnlAITactics.Controls.Add(this.btnAIAddSav);
            this.pnlAITactics.Controls.Add(this.btnAIAddFolder);
            this.pnlAITactics.Controls.Add(this.btnAIAddFiles);
            this.pnlAITactics.Controls.Add(this.lblAITacticsTotal);
            this.pnlAITactics.Controls.Add(this.btnAIDelete);
            this.pnlAITactics.Controls.Add(this.btnAIClear);
            this.pnlAITactics.Controls.Add(this.lblAITactics);
            this.pnlAITactics.Controls.Add(this.lstAITactics);
            this.pnlAITactics.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAITactics.Location = new System.Drawing.Point(285, 0);
            this.pnlAITactics.Name = "pnlAITactics";
            this.pnlAITactics.Size = new System.Drawing.Size(292, 311);
            this.pnlAITactics.TabIndex = 38;
            // 
            // chkDontReplaceAITactics
            // 
            this.chkDontReplaceAITactics.AutoSize = true;
            this.chkDontReplaceAITactics.Location = new System.Drawing.Point(147, 5);
            this.chkDontReplaceAITactics.Name = "chkDontReplaceAITactics";
            this.chkDontReplaceAITactics.Size = new System.Drawing.Size(138, 17);
            this.chkDontReplaceAITactics.TabIndex = 49;
            this.chkDontReplaceAITactics.Text = "Do NOT replace tactics";
            this.chkDontReplaceAITactics.UseVisualStyleBackColor = true;
            this.chkDontReplaceAITactics.CheckedChanged += new System.EventHandler(this.chkDontReplaceAITactics_CheckedChanged);
            // 
            // btnAIAddSav
            // 
            this.btnAIAddSav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAIAddSav.ImageIndex = 0;
            this.btnAIAddSav.ImageList = this.imageList;
            this.btnAIAddSav.Location = new System.Drawing.Point(174, 282);
            this.btnAIAddSav.Name = "btnAIAddSav";
            this.btnAIAddSav.Size = new System.Drawing.Size(57, 23);
            this.btnAIAddSav.TabIndex = 48;
            this.btnAIAddSav.Text = ".sav";
            this.btnAIAddSav.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnAIAddSav, "Add from game save file");
            this.btnAIAddSav.UseVisualStyleBackColor = true;
            this.btnAIAddSav.Click += new System.EventHandler(this.btnAIAddSav_Click);
            // 
            // btnAIAddFolder
            // 
            this.btnAIAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAIAddFolder.ImageIndex = 0;
            this.btnAIAddFolder.ImageList = this.imageList;
            this.btnAIAddFolder.Location = new System.Drawing.Point(111, 282);
            this.btnAIAddFolder.Name = "btnAIAddFolder";
            this.btnAIAddFolder.Size = new System.Drawing.Size(57, 23);
            this.btnAIAddFolder.TabIndex = 47;
            this.btnAIAddFolder.Text = "folder";
            this.btnAIAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnAIAddFolder, "Add from folder");
            this.btnAIAddFolder.UseVisualStyleBackColor = true;
            this.btnAIAddFolder.Click += new System.EventHandler(this.btnAIAddFolder_Click);
            // 
            // btnAIAddFiles
            // 
            this.btnAIAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAIAddFiles.ImageIndex = 0;
            this.btnAIAddFiles.ImageList = this.imageList;
            this.btnAIAddFiles.Location = new System.Drawing.Point(58, 283);
            this.btnAIAddFiles.Name = "btnAIAddFiles";
            this.btnAIAddFiles.Size = new System.Drawing.Size(47, 23);
            this.btnAIAddFiles.TabIndex = 46;
            this.btnAIAddFiles.Text = ".tct";
            this.btnAIAddFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnAIAddFiles, "Add from .tct / .pct files");
            this.btnAIAddFiles.UseVisualStyleBackColor = true;
            this.btnAIAddFiles.Click += new System.EventHandler(this.btnAIAddFiles_Click);
            // 
            // lblAITacticsTotal
            // 
            this.lblAITacticsTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAITacticsTotal.AutoSize = true;
            this.lblAITacticsTotal.Location = new System.Drawing.Point(3, 288);
            this.lblAITacticsTotal.Name = "lblAITacticsTotal";
            this.lblAITacticsTotal.Size = new System.Drawing.Size(13, 13);
            this.lblAITacticsTotal.TabIndex = 45;
            this.lblAITacticsTotal.Text = "0";
            this.toolTip.SetToolTip(this.lblAITacticsTotal, "Quantity");
            // 
            // btnAIDelete
            // 
            this.btnAIDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAIDelete.ImageIndex = 2;
            this.btnAIDelete.ImageList = this.imageList;
            this.btnAIDelete.Location = new System.Drawing.Point(237, 283);
            this.btnAIDelete.Name = "btnAIDelete";
            this.btnAIDelete.Size = new System.Drawing.Size(22, 22);
            this.btnAIDelete.TabIndex = 43;
            this.btnAIDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnAIDelete, "Remove selected");
            this.btnAIDelete.UseVisualStyleBackColor = true;
            this.btnAIDelete.Click += new System.EventHandler(this.btnAIDelete_Click);
            // 
            // btnAIClear
            // 
            this.btnAIClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAIClear.ImageIndex = 1;
            this.btnAIClear.ImageList = this.imageList;
            this.btnAIClear.Location = new System.Drawing.Point(265, 283);
            this.btnAIClear.Name = "btnAIClear";
            this.btnAIClear.Size = new System.Drawing.Size(22, 22);
            this.btnAIClear.TabIndex = 44;
            this.toolTip.SetToolTip(this.btnAIClear, "Remove all");
            this.btnAIClear.UseVisualStyleBackColor = true;
            this.btnAIClear.Click += new System.EventHandler(this.btnAIClear_Click);
            // 
            // lblAITactics
            // 
            this.lblAITactics.AutoSize = true;
            this.lblAITactics.Location = new System.Drawing.Point(3, 6);
            this.lblAITactics.Name = "lblAITactics";
            this.lblAITactics.Size = new System.Drawing.Size(86, 13);
            this.lblAITactics.TabIndex = 31;
            this.lblAITactics.Text = "AI tactics to test:";
            // 
            // lstAITactics
            // 
            this.lstAITactics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAITactics.FormattingEnabled = true;
            this.lstAITactics.Location = new System.Drawing.Point(3, 26);
            this.lstAITactics.Name = "lstAITactics";
            this.lstAITactics.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAITactics.Size = new System.Drawing.Size(284, 251);
            this.lstAITactics.TabIndex = 32;
            this.lstAITactics.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstAITactics_KeyUp);
            // 
            // pnlHumanTactics
            // 
            this.pnlHumanTactics.Controls.Add(this.btnHumanAddSav);
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
            this.pnlHumanTactics.Size = new System.Drawing.Size(285, 311);
            this.pnlHumanTactics.TabIndex = 37;
            // 
            // btnHumanAddSav
            // 
            this.btnHumanAddSav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanAddSav.ImageIndex = 0;
            this.btnHumanAddSav.ImageList = this.imageList;
            this.btnHumanAddSav.Location = new System.Drawing.Point(167, 282);
            this.btnHumanAddSav.Name = "btnHumanAddSav";
            this.btnHumanAddSav.Size = new System.Drawing.Size(57, 23);
            this.btnHumanAddSav.TabIndex = 42;
            this.btnHumanAddSav.Text = ".sav";
            this.btnHumanAddSav.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnHumanAddSav, "Add from game save file");
            this.btnHumanAddSav.UseVisualStyleBackColor = true;
            this.btnHumanAddSav.Click += new System.EventHandler(this.btnHumanAddSav_Click);
            // 
            // btnHumanAddFolder
            // 
            this.btnHumanAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHumanAddFolder.ImageIndex = 0;
            this.btnHumanAddFolder.ImageList = this.imageList;
            this.btnHumanAddFolder.Location = new System.Drawing.Point(104, 282);
            this.btnHumanAddFolder.Name = "btnHumanAddFolder";
            this.btnHumanAddFolder.Size = new System.Drawing.Size(57, 23);
            this.btnHumanAddFolder.TabIndex = 41;
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
            this.btnHumanAddFiles.Location = new System.Drawing.Point(51, 283);
            this.btnHumanAddFiles.Name = "btnHumanAddFiles";
            this.btnHumanAddFiles.Size = new System.Drawing.Size(47, 23);
            this.btnHumanAddFiles.TabIndex = 40;
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
            this.lblHumanTacticsTotal.Location = new System.Drawing.Point(12, 288);
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
            this.lstHumanTactics.Size = new System.Drawing.Size(267, 251);
            this.lstHumanTactics.TabIndex = 36;
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
            this.btnHumanDelete.Location = new System.Drawing.Point(230, 283);
            this.btnHumanDelete.Name = "btnHumanDelete";
            this.btnHumanDelete.Size = new System.Drawing.Size(22, 22);
            this.btnHumanDelete.TabIndex = 37;
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
            this.btnHumanClear.Location = new System.Drawing.Point(258, 283);
            this.btnHumanClear.Name = "btnHumanClear";
            this.btnHumanClear.Size = new System.Drawing.Size(22, 22);
            this.btnHumanClear.TabIndex = 38;
            this.toolTip.SetToolTip(this.btnHumanClear, "Remove all");
            this.btnHumanClear.UseVisualStyleBackColor = true;
            this.btnHumanClear.Click += new System.EventHandler(this.btnHumanClear_Click);
            // 
            // dlgTacticsFolder
            // 
            this.dlgTacticsFolder.Description = "Select folder with .pct and .tct files";
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
            // dlgTacticFiles
            // 
            this.dlgTacticFiles.Filter = "CM tactic files|*.tct;*.pct|All files|*.*";
            this.dlgTacticFiles.Multiselect = true;
            this.dlgTacticFiles.Title = "Pick .tct / .pct files";
            // 
            // dlgBmSavFilename
            // 
            this.dlgBmSavFilename.FileName = "a_benchmark.sav";
            this.dlgBmSavFilename.Filter = "CM save files|*.sav|All files|*.*";
            this.dlgBmSavFilename.Title = "Pick .sav file";
            // 
            // dlgCmFolder
            // 
            this.dlgCmFolder.Description = "Select CM folder";
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
            this.pnlAITactics.ResumeLayout(false);
            this.pnlAITactics.PerformLayout();
            this.pnlHumanTactics.ResumeLayout(false);
            this.pnlHumanTactics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog dlgAddSavFilename;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Panel pnlOutputGrid;
        private System.Windows.Forms.WebBrowser webGrid;
        private System.Windows.Forms.FolderBrowserDialog dlgTacticsFolder;
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
        private System.Windows.Forms.Button btnBMSavFilenameDlg;
        private System.Windows.Forms.TextBox txtBMSavFilename;
        private System.Windows.Forms.Label lblBMSavFilename;
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
        private System.Windows.Forms.Panel pnlAITactics;
        private System.Windows.Forms.Button btnAIAddSav;
        private System.Windows.Forms.Button btnAIAddFolder;
        private System.Windows.Forms.Button btnAIAddFiles;
        private System.Windows.Forms.Label lblAITacticsTotal;
        private System.Windows.Forms.Button btnAIDelete;
        private System.Windows.Forms.Button btnAIClear;
        private System.Windows.Forms.Label lblAITactics;
        private System.Windows.Forms.ListBox lstAITactics;
        private System.Windows.Forms.Panel pnlHumanTactics;
        private System.Windows.Forms.Button btnHumanAddSav;
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
        private System.Windows.Forms.OpenFileDialog dlgTacticFiles;
        private System.Windows.Forms.OpenFileDialog dlgBmSavFilename;
        private System.Windows.Forms.CheckBox chkDontReplaceAITactics;
        private System.Windows.Forms.FolderBrowserDialog dlgCmFolder;
    }
}

