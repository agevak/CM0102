using System;
using System.Windows.Forms;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace CM.Scout
{
    public static class TrueCMScout
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
