using System.Drawing;
using System.Windows.Forms;

namespace CM.Model
{
    public class UIState
    {
        public bool DontReplaceTactics {  get; set; }
        public string AddFiles { get; set; }
        public string AddFolder { get; set; }
        public string AddSav { get; set; }
        public string CMFolder {  get; set; }
        public string BMSavFilename { get; set; }
        public int TestCount { get; set; } = 1;
        public int ThreadCount { get; set; } = 1;
        public int ThreadTimeoutSec { get; set; } = 120;
        public bool HideCMWindow { get; set; }
        public Point MainFormLocation { get; set; } = new Point(100, 100);
        public Size MainFormSize { get; set; } = new Size(968, 600);
        public int SplitterDistance { get; set; } = 315;
        public FormWindowState WindowState { get; set; }
    }
}
