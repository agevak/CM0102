using System.Drawing;
using System.Windows.Forms;

namespace CM.Model
{
    public class UIState
    {
        public string AddTacticFiles { get; set; }
        public string AddTacticsFolder { get; set; }
        public string AddGameSaveFiles { get; set; }
        public string AddGameSavesFolder { get; set; }
        public string CMFolder {  get; set; }
        public int TestCount { get; set; } = 1;
        public int ThreadCount { get; set; } = 1;
        public int ThreadTimeoutSec { get; set; } = 300;
        public bool HideCMWindow { get; set; }
        public Point MainFormLocation { get; set; } = new Point(100, 100);
        public Size MainFormSize { get; set; } = new Size(968, 600);
        public int SplitterDistance { get; set; } = 315;
        public FormWindowState WindowState { get; set; }
        public bool HumanTacticsInRows { get; set; } = true;
        public bool MakePlayersAllPositioners { get; set; } = true;
        public string SaveResultsPath { get; set; }
        public string LoadResultsPath { get; set; }
        public string ExportResultsPath { get; set; }
    }
}
