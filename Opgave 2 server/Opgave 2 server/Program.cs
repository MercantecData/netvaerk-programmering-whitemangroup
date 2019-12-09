using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Opgave_2_server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Server

            // connection info
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            // starts to listen for users
            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");

            // accepts client for data stream
            TcpClient client = listener.AcceptTcpClient();

            // gets data from stream
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);
            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            
            Console.WriteLine(message);

            // echos back the message to the client
            stream.Write(buffer, 0, numberOfBytesRead);
        }
    }
}
