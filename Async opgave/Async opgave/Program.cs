using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Async_opgave
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup();
            Console.ReadLine();
        }

        static void Startup()
        {
            Console.WriteLine("1. Client\n2. Server");
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
                Startup();
            }
        }


        static void Client ()
        {
            TcpClient client = new TcpClient();

            // Connection
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            // Gets data from stream
            NetworkStream stream = client.GetStream();

            // Loop keeps program running. Else it would stop after first message send
            bool isConnected = true;
            while (isConnected == true)
            {
                // Gets data from stream
                stream = client.GetStream();

                ReceiveMessage(stream);


                // Message encoding
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                
                // Message Write on stream = shows on server side
                stream.Write(buffer, 0, buffer.Length);
            }

            client.Close();
        }

        static async void Server()
        {
            // Connection
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            // Starts to listen for users
            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");

            // "Await" command creates an off-branch code execution
            // 1. branch continues the code
            // 2. branch waits for a client
            TcpClient client = await listener.AcceptTcpClientAsync();

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            // Loop keeps program running. Else it would stop after first message received
            bool isConnected = true;
            while (isConnected == true)
            {
                // Message encoding
                Console.Write("Write your message here: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);

                // Message Write on stream = shows on client side
                stream.Write(buffer, 0, buffer.Length);

                Console.ReadKey();
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

// Note :
// Jeg kunne ikke få den udleverede kode til at fungere
// Det eneste fix jeg kunne finde var at lave de forskellige metoder til "Static"
// (Opgavens kode skriver kun "public")