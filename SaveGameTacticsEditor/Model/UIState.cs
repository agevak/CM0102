using System.Drawing;

namespace CM.Model
{
    public class UIState
    {
        public string LoadFolder { get; set; }
        public string LoadSav { get; set; }
        public string LoadAIPack { get; set; }
        public string SaveSav {  get; set; }
        public string SaveAIPack { get; set; }
        public string SaveTct { get; set; }
        public bool UpdateTitle { get; set; } = true;
        public Point MainFormLocation { get; set; } = new Point(100, 100);
        public Size MainFormSize { get; set; } = new Size(1054, 930);
    }
}
