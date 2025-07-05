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
            this.dlgLoadSav = new System.Windows.Forms.OpenFileDialog();
            this.dlgLoadFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgSaveSav = new System.Windows.Forms.SaveFileDialog();
            this.lblFolderTactics = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnRefreshFolder = new System.Windows.Forms.Button();
            this.txtLoadFolder = new System.Windows.Forms.TextBox();
            this.btnAutoAssign = new System.Windows.Forms.Button();
            this.lblDragAndDrop = new System.Windows.Forms.Label();
            this.lblSavTactics = new System.Windows.Forms.Label();
            this.btnLoadSav = new System.Windows.Forms.Button();
            this.btnSaveSav = new System.Windows.Forms.Button();
            this.btnSaveAIPack = new System.Windows.Forms.Button();
            this.btnSaveTct = new System.Windows.Forms.Button();
            this.lstSavTactics = new System.Windows.Forms.ListView();
            this.hdrIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrAIPackFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrDefaultName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrCurName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrUpdateTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dlgLoadAIPack = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgSaveAIPack = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgSaveTct = new System.Windows.Forms.FolderBrowserDialog();
            this.lstFolderTactics = new System.Windows.Forms.ListView();
            this.hdrFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkUpdateName = new System.Windows.Forms.CheckBox();
            this.btnEditSavTacticName = new System.Windows.Forms.Button();
            this.btnResetSavTacticNames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dlgLoadSav
            // 
            this.dlgLoadSav.Filter = "CM save files|*.sav|All files|*.*";
            this.dlgLoadSav.Title = "Load .sav file";
            // 
            // dlgLoadFolder
            // 
            this.dlgLoadFolder.Description = "Load .pct and .tct files from folder";
            this.dlgLoadFolder.ShowNewFolderButton = false;
            // 
            // dlgSaveSav
            // 
            this.dlgSaveSav.Filter = "CM save files|*.sav|All files|*.*";
            this.dlgSaveSav.RestoreDirectory = true;
            this.dlgSaveSav.Title = "Save as .sav file";
            // 
            // lblFolderTactics
            // 
            this.lblFolderTactics.AutoSize = true;
            this.lblFolderTactics.Location = new System.Drawing.Point(12, 9);
            this.lblFolderTactics.Name = "lblFolderTactics";
            this.lblFolderTactics.Size = new System.Drawing.Size(97, 13);
            this.lblFolderTactics.TabIndex = 3;
            this.lblFolderTactics.Text = "Tactics from folder:";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenFolder.ImageIndex = 2;
            this.btnOpenFolder.ImageList = this.imageList;
            this.btnOpenFolder.Location = new System.Drawing.Point(142, 4);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(108, 25);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Select folder...";
            this.btnOpenFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Edit.png");
            this.imageList.Images.SetKeyName(1, "OpenFile.png");
            this.imageList.Images.SetKeyName(2, "OpenFolder.png");
            this.imageList.Images.SetKeyName(3, "Refresh.png");
            this.imageList.Images.SetKeyName(4, "Save.png");
            this.imageList.Images.SetKeyName(5, "Undo.png");
            // 
            // btnRefreshFolder
            // 
            this.btnRefreshFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefreshFolder.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefreshFolder.ImageKey = "Refresh.png";
            this.btnRefreshFolder.ImageList = this.imageList;
            this.btnRefreshFolder.Location = new System.Drawing.Point(256, 4);
            this.btnRefreshFolder.Name = "btnRefreshFolder";
            this.btnRefreshFolder.Size = new System.Drawing.Size(89, 25);
            this.btnRefreshFolder.TabIndex = 1;
            this.btnRefreshFolder.Text = "Reload";
            this.btnRefreshFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshFolder.UseVisualStyleBackColor = true;
            this.btnRefreshFolder.Click += new System.EventHandler(this.btnRefreshFolder_Click);
            // 
            // txtLoadFolder
            // 
            this.txtLoadFolder.Location = new System.Drawing.Point(12, 35);
            this.txtLoadFolder.Name = "txtLoadFolder";
            this.txtLoadFolder.ReadOnly = true;
            this.txtLoadFolder.Size = new System.Drawing.Size(333, 20);
            this.txtLoadFolder.TabIndex = 2;
            // 
            // btnAutoAssign
            // 
            this.btnAutoAssign.Location = new System.Drawing.Point(351, 188);
            this.btnAutoAssign.Name = "btnAutoAssign";
            this.btnAutoAssign.Size = new System.Drawing.Size(133, 25);
            this.btnAutoAssign.TabIndex = 4;
            this.btnAutoAssign.Text = "Assign by file name >>";
            this.btnAutoAssign.UseVisualStyleBackColor = true;
            this.btnAutoAssign.Click += new System.EventHandler(this.btnAutoAssign_Click);
            // 
            // lblDragAndDrop
            // 
            this.lblDragAndDrop.AutoSize = true;
            this.lblDragAndDrop.Location = new System.Drawing.Point(377, 234);
            this.lblDragAndDrop.Name = "lblDragAndDrop";
            this.lblDragAndDrop.Size = new System.Drawing.Size(85, 13);
            this.lblDragAndDrop.TabIndex = 13;
            this.lblDragAndDrop.Text = "or drag and drop";
            // 
            // lblSavTactics
            // 
            this.lblSavTactics.AutoSize = true;
            this.lblSavTactics.Location = new System.Drawing.Point(487, 9);
            this.lblSavTactics.Name = "lblSavTactics";
            this.lblSavTactics.Size = new System.Drawing.Size(111, 13);
            this.lblSavTactics.TabIndex = 14;
            this.lblSavTactics.Text = "Tactics in game save:";
            // 
            // btnLoadSav
            // 
            this.btnLoadSav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSav.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLoadSav.ImageIndex = 1;
            this.btnLoadSav.ImageList = this.imageList;
            this.btnLoadSav.Location = new System.Drawing.Point(926, 4);
            this.btnLoadSav.Name = "btnLoadSav";
            this.btnLoadSav.Size = new System.Drawing.Size(103, 25);
            this.btnLoadSav.TabIndex = 6;
            this.btnLoadSav.Text = "Load .sav file";
            this.btnLoadSav.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadSav.UseVisualStyleBackColor = true;
            this.btnLoadSav.Click += new System.EventHandler(this.btnLoadSav_Click);
            // 
            // btnSaveSav
            // 
            this.btnSaveSav.Enabled = false;
            this.btnSaveSav.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveSav.ImageIndex = 4;
            this.btnSaveSav.ImageList = this.imageList;
            this.btnSaveSav.Location = new System.Drawing.Point(490, 857);
            this.btnSaveSav.Name = "btnSaveSav";
            this.btnSaveSav.Size = new System.Drawing.Size(99, 25);
            this.btnSaveSav.TabIndex = 8;
            this.btnSaveSav.Text = "Save to .sav";
            this.btnSaveSav.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveSav.UseVisualStyleBackColor = true;
            this.btnSaveSav.Click += new System.EventHandler(this.btnSaveSav_Click);
            // 
            // btnSaveAIPack
            // 
            this.btnSaveAIPack.Enabled = false;
            this.btnSaveAIPack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveAIPack.ImageIndex = 4;
            this.btnSaveAIPack.ImageList = this.imageList;
            this.btnSaveAIPack.Location = new System.Drawing.Point(595, 857);
            this.btnSaveAIPack.Name = "btnSaveAIPack";
            this.btnSaveAIPack.Size = new System.Drawing.Size(115, 25);
            this.btnSaveAIPack.TabIndex = 9;
            this.btnSaveAIPack.Text = "Save as AI pack";
            this.btnSaveAIPack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveAIPack.UseVisualStyleBackColor = true;
            this.btnSaveAIPack.Click += new System.EventHandler(this.btnSaveAIPack_Click);
            // 
            // btnSaveTct
            // 
            this.btnSaveTct.Enabled = false;
            this.btnSaveTct.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveTct.ImageIndex = 4;
            this.btnSaveTct.ImageList = this.imageList;
            this.btnSaveTct.Location = new System.Drawing.Point(716, 857);
            this.btnSaveTct.Name = "btnSaveTct";
            this.btnSaveTct.Size = new System.Drawing.Size(114, 25);
            this.btnSaveTct.TabIndex = 10;
            this.btnSaveTct.Text = "Save as .tct files";
            this.btnSaveTct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveTct.UseVisualStyleBackColor = true;
            this.btnSaveTct.Click += new System.EventHandler(this.btnSaveTct_Click);
            // 
            // lstSavTactics
            // 
            this.lstSavTactics.AllowDrop = true;
            this.lstSavTactics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSavTactics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrIndex,
            this.hdrAIPackFilename,
            this.hdrDefaultName,
            this.hdrCurName,
            this.hdrUpdateTo});
            this.lstSavTactics.FullRowSelect = true;
            this.lstSavTactics.GridLines = true;
            this.lstSavTactics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstSavTactics.HideSelection = false;
            this.lstSavTactics.Location = new System.Drawing.Point(490, 35);
            this.lstSavTactics.MultiSelect = false;
            this.lstSavTactics.Name = "lstSavTactics";
            this.lstSavTactics.Size = new System.Drawing.Size(539, 816);
            this.lstSavTactics.TabIndex = 7;
            this.lstSavTactics.UseCompatibleStateImageBehavior = false;
            this.lstSavTactics.View = System.Windows.Forms.View.Details;
            this.lstSavTactics.SelectedIndexChanged += new System.EventHandler(this.lstSavTactics_SelectedIndexChanged);
            this.lstSavTactics.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstSavTactics_DragDrop);
            this.lstSavTactics.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstSavTactics_DragEnter);
            // 
            // hdrIndex
            // 
            this.hdrIndex.Text = "#";
            this.hdrIndex.Width = 30;
            // 
            // hdrAIPackFilename
            // 
            this.hdrAIPackFilename.Text = "AI pack file name";
            this.hdrAIPackFilename.Width = 140;
            // 
            // hdrDefaultName
            // 
            this.hdrDefaultName.Text = "Default name";
            this.hdrDefaultName.Width = 100;
            // 
            // hdrCurName
            // 
            this.hdrCurName.Text = "Current name";
            this.hdrCurName.Width = 120;
            // 
            // hdrUpdateTo
            // 
            this.hdrUpdateTo.Text = "Update to";
            this.hdrUpdateTo.Width = 140;
            // 
            // dlgLoadAIPack
            // 
            this.dlgLoadAIPack.Description = "Load from folder with AI pack (set of .pct files)";
            // 
            // dlgSaveAIPack
            // 
            this.dlgSaveAIPack.Description = "Save as AI pack (set .pct files) to folder";
            // 
            // dlgSaveTct
            // 
            this.dlgSaveTct.Description = "Save as .tct files to folder";
            // 
            // lstFolderTactics
            // 
            this.lstFolderTactics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFolderTactics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrFilename});
            this.lstFolderTactics.FullRowSelect = true;
            this.lstFolderTactics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstFolderTactics.HideSelection = false;
            this.lstFolderTactics.Location = new System.Drawing.Point(15, 71);
            this.lstFolderTactics.MultiSelect = false;
            this.lstFolderTactics.Name = "lstFolderTactics";
            this.lstFolderTactics.Size = new System.Drawing.Size(330, 808);
            this.lstFolderTactics.TabIndex = 3;
            this.lstFolderTactics.UseCompatibleStateImageBehavior = false;
            this.lstFolderTactics.View = System.Windows.Forms.View.Details;
            this.lstFolderTactics.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstFolderTactics_ItemDrag);
            // 
            // hdrFilename
            // 
            this.hdrFilename.Text = "File name";
            this.hdrFilename.Width = 320;
            // 
            // chkUpdateName
            // 
            this.chkUpdateName.AutoSize = true;
            this.chkUpdateName.Checked = true;
            this.chkUpdateName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateName.Location = new System.Drawing.Point(359, 270);
            this.chkUpdateName.Name = "chkUpdateName";
            this.chkUpdateName.Size = new System.Drawing.Size(119, 17);
            this.chkUpdateName.TabIndex = 5;
            this.chkUpdateName.Text = "Update tactic name";
            this.chkUpdateName.UseVisualStyleBackColor = true;
            // 
            // btnEditSavTacticName
            // 
            this.btnEditSavTacticName.Enabled = false;
            this.btnEditSavTacticName.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditSavTacticName.ImageIndex = 0;
            this.btnEditSavTacticName.ImageList = this.imageList;
            this.btnEditSavTacticName.Location = new System.Drawing.Point(836, 857);
            this.btnEditSavTacticName.Name = "btnEditSavTacticName";
            this.btnEditSavTacticName.Size = new System.Drawing.Size(84, 25);
            this.btnEditSavTacticName.TabIndex = 11;
            this.btnEditSavTacticName.Text = "Edit name";
            this.btnEditSavTacticName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditSavTacticName.UseVisualStyleBackColor = true;
            this.btnEditSavTacticName.Click += new System.EventHandler(this.btnEditSavTacticName_Click);
            // 
            // btnResetSavTacticNames
            // 
            this.btnResetSavTacticNames.Enabled = false;
            this.btnResetSavTacticNames.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnResetSavTacticNames.ImageIndex = 5;
            this.btnResetSavTacticNames.ImageList = this.imageList;
            this.btnResetSavTacticNames.Location = new System.Drawing.Point(926, 857);
            this.btnResetSavTacticNames.Name = "btnResetSavTacticNames";
            this.btnResetSavTacticNames.Size = new System.Drawing.Size(100, 25);
            this.btnResetSavTacticNames.TabIndex = 12;
            this.btnResetSavTacticNames.Text = "Reset names";
            this.btnResetSavTacticNames.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetSavTacticNames.UseVisualStyleBackColor = true;
            this.btnResetSavTacticNames.Click += new System.EventHandler(this.btnResetSavTacticName_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1038, 891);
            this.Controls.Add(this.btnResetSavTacticNames);
            this.Controls.Add(this.btnEditSavTacticName);
            this.Controls.Add(this.chkUpdateName);
            this.Controls.Add(this.lstFolderTactics);
            this.Controls.Add(this.lstSavTactics);
            this.Controls.Add(this.btnSaveTct);
            this.Controls.Add(this.btnSaveAIPack);
            this.Controls.Add(this.btnSaveSav);
            this.Controls.Add(this.btnLoadSav);
            this.Controls.Add(this.lblSavTactics);
            this.Controls.Add(this.lblDragAndDrop);
            this.Controls.Add(this.btnAutoAssign);
            this.Controls.Add(this.txtLoadFolder);
            this.Controls.Add(this.btnRefreshFolder);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.lblFolderTactics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CM0102 save game tactics editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog dlgLoadSav;
        private System.Windows.Forms.FolderBrowserDialog dlgLoadFolder;
        private System.Windows.Forms.SaveFileDialog dlgSaveSav;
        private System.Windows.Forms.Label lblFolderTactics;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnRefreshFolder;
        private System.Windows.Forms.TextBox txtLoadFolder;
        private System.Windows.Forms.Button btnAutoAssign;
        private System.Windows.Forms.Label lblDragAndDrop;
        private System.Windows.Forms.Label lblSavTactics;
        private System.Windows.Forms.Button btnLoadSav;
        private System.Windows.Forms.Button btnSaveSav;
        private System.Windows.Forms.Button btnSaveAIPack;
        private System.Windows.Forms.Button btnSaveTct;
        private System.Windows.Forms.ListView lstSavTactics;
        private System.Windows.Forms.ColumnHeader hdrIndex;
        private System.Windows.Forms.ColumnHeader hdrDefaultName;
        private System.Windows.Forms.ColumnHeader hdrCurName;
        private System.Windows.Forms.ColumnHeader hdrUpdateTo;
        private System.Windows.Forms.FolderBrowserDialog dlgLoadAIPack;
        private System.Windows.Forms.FolderBrowserDialog dlgSaveAIPack;
        private System.Windows.Forms.FolderBrowserDialog dlgSaveTct;
        private System.Windows.Forms.ColumnHeader hdrAIPackFilename;
        private System.Windows.Forms.ListView lstFolderTactics;
        private System.Windows.Forms.ColumnHeader hdrFilename;
        private System.Windows.Forms.CheckBox chkUpdateName;
        private System.Windows.Forms.Button btnEditSavTacticName;
        private System.Windows.Forms.Button btnResetSavTacticNames;
        private System.Windows.Forms.ImageList imageList;
    }
}

