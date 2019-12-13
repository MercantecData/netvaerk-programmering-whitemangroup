using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Async_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client");
            Client();
        }

        public static void Client ()
        {
            TcpClient client = new TcpClient();

            // Connection
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            // Loop keeps program running. Else it would stop after first message send
            while (true)
            {
                // Gets data from stream
                NetworkStream stream = client.GetStream();
                ReceiveMessage(stream);

                // Message encoding
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);

                // Message Write on stream = shows on server side
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];

            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

            Console.WriteLine("\n" + receivedMessage);
        }
    }
}
