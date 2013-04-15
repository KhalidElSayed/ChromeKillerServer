using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChromeKiller
{

    public class UDPServer
    {
        private const int listenPort = 18250;
        private bool running = true;

        public UDPServer(int port)
        {
            Thread t = new Thread(new ThreadStart(run));
            t.Start();
        }

        public void run()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;
            try
            {
                while (running)
                {
                    receive_byte_array = listener.Receive(ref groupEP);
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    //Kill chrome
                    HistoryClearer.Kill();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            listener.Close();
        }

        public void Close()
        {
            running = false;
        }
    }
}