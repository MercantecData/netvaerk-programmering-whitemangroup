using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Opgave_2___Call_response
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup();
        }

        static void Startup ()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("1: Client");
            Console.WriteLine("2: Server");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Client();
            }
            else if (input == "2")
            {
                Server();
            }
            else
            {
                Console.WriteLine("Wrong input");
                Console.ReadKey();
                Console.Clear();
                Startup();
            }
        }

        static void Client ()
        {
            TcpClient client = new TcpClient();

            // connection info
            int port = EnterPort();     // default = 13356
            IPAddress ip = IPAddress.Parse(EnterIp());
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);

            // gets data from stream
            NetworkStream stream = client.GetStream();

            // creates the message from user input
            string text = EnterMsg();
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            // sends message
            stream.Write(buffer, 0, buffer.Length);

            // reads echo from server
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine(Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();

            client.Close();
        }

        private static int EnterPort ()
        {
            Console.WriteLine("Enter port number");
            int port = int.Parse(Console.ReadLine());
            return port;
        }

        private static string EnterIp ()
        {
            Console.WriteLine("Enter IP of receiver");
            string ip = Console.ReadLine();
            return ip;
        }

        private static string EnterMsg ()
        {
            Console.WriteLine("Enter your message");
            string msg = Console.ReadLine();
            return msg;
        }



        static void Server ()
        {
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
