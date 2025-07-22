using System;
using System.Windows.Forms;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace CM
{
    static class TacticsBenchmarkerVsAI
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
