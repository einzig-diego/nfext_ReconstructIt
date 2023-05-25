using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace reconstructIt
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo("..\\..\\..\\nFinance.exe");
            string version = versionInfo.FileVersion;
            if (version != "1.0.0.63") { MessageBox.Show("This version is not compatible with your installation."); return; }
         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
