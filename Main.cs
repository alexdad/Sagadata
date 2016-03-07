using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    static class Toplevel
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string lockFile = Path.Combine(Path.GetTempPath(), "RecordKeeper.lock");
            if (!File.Exists(lockFile))
                File.WriteAllText(lockFile, "LOCK");

            using (FileStream fs = File.Open(lockFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormGlob());
            }
        }
    }
}
