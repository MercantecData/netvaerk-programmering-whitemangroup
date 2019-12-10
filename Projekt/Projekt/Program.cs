using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            StartMenu();
        }
        static void StartMenu()
        {
            Console.WriteLine("Choose:");
            Console.WriteLine("1. Client");
            Console.WriteLine("2. Server");

            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Client();
            }
            else if (userInput == "2")
            {
                Server();
            }
            else
            {
                StartMenu();
            }
        }
        static void Server() 
        {
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);
            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(message);

            stream.Write(buffer, 0, numberOfBytesRead);

        }
        static void Client()
        {
            TcpClient client = new TcpClient();
            Console.WriteLine("Write the port number:");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine("Write the IP address:");
            IPAddress ip = IPAddress.Parse(Console.ReadLine());
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            Console.WriteLine("Write your message:");
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
    
}
