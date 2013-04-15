using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChromeKiller
{
    class HistoryClearer
    {
        public static void ClearHistory()
        {
            foreach (string file in Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default")))
            {
                if (file.Contains("History") || file.Contains("Current Session"))
                    File.Delete(file);
            }
        }

        public static void CloseChrome()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.Equals("chrome"))
                    p.Kill();
            }
        }

        public static void Kill()
        {
            HistoryClearer.CloseChrome();
            Thread.Sleep(100);
            HistoryClearer.ClearHistory();
        }
    }
}
