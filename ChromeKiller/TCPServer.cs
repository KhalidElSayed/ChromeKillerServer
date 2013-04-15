using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO.Ports;

namespace ChromeKiller
{
    public class TCPServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private TcpClient client;
        private int port;
        private bool running = true;

        // Creates a new threaded server
        public TCPServer(int port)
        {
            this.port = port;
            //Specifies IP and Port of the server
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            //Listen for clients on a seperate thread
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            //Start the process
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            try
            {
                this.tcpListener.Start();
            }
            catch
            {
                MessageBox.Show("Failed to start server.");
            }
            while (running)
            {
                try
                {
                    //Blocks until a client has connected to the server
                    client = this.tcpListener.AcceptTcpClient();
                    //Kill client
                    client.Close();
                    //Kill chrome
                    HistoryClearer.Kill();
                }
                catch { break; }
            }
        }

        public void Close()
        {
            this.running = false;
            tcpListener.Stop();
        }
    }
}
