using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CM.Helpers;
using CM.UI.Model;

namespace CM.Scout.Model
{
    public class CMScoutUIState
    {
        public static CMScoutUIState FromString(string s) => Helper.Deserialize<CMScoutUIState>(s);

        public Point MainFormLocation { get; set; } = new Point(0, 0);
        public Size MainFormSize { get; set; } = new Size(1360, 1399);
        public FormWindowState WindowState { get; set; }
        public IList<PlayerSearchControlUIState> Tabs { get; set; } = new List<PlayerSearchControlUIState>();
        public int SelectedTabIndex { get; set; }

        public override string ToString() => Helper.Serialize(this);
    }
}
