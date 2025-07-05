namespace CM.Scout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbcTabs = new System.Windows.Forms.TabControl();
            this.tabAddTab = new System.Windows.Forms.TabPage();
            this.tbcTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcTabs
            // 
            this.tbcTabs.Controls.Add(this.tabAddTab);
            this.tbcTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcTabs.Location = new System.Drawing.Point(0, 0);
            this.tbcTabs.Name = "tbcTabs";
            this.tbcTabs.SelectedIndex = 0;
            this.tbcTabs.Size = new System.Drawing.Size(1340, 875);
            this.tbcTabs.TabIndex = 0;
            this.tbcTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcTabs_Selecting);
            this.tbcTabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbcTabs_MouseClick);
            this.tbcTabs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbcTabs_MouseDoubleClick);
            // 
            // tabAddTab
            // 
            this.tabAddTab.Location = new System.Drawing.Point(4, 22);
            this.tabAddTab.Name = "tabAddTab";
            this.tabAddTab.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddTab.Size = new System.Drawing.Size(1332, 849);
            this.tabAddTab.TabIndex = 0;
            this.tabAddTab.Text = "+";
            this.tabAddTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 875);
            this.Controls.Add(this.tbcTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "True CM Scout";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tbcTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcTabs;
        private System.Windows.Forms.TabPage tabAddTab;
    }
}

