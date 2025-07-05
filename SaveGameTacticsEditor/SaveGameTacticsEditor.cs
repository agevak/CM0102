using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace CM
{
    static class SaveGameTacticsEditor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
