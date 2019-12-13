using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MultiClient_Server
{
    class Class_Server
    {
        // Handles multiple clients once connected
        public List<TcpClient> clients = new List<TcpClient>();
        public List<string> usernames = new List<string>();
        public void MyServer()
        {
            Console.WriteLine("Server");
            // Connection info
            IPAddress ip = IPAddress.Any; //Parse("127.0.0.1");
            int port = 13356;
            TcpListener listener = new TcpListener(ip, port);

            // Starts to listen on the given socket (ip & port)
            listener.Start();

            // Starts a loop to accept incomming users from the listening socket
            // The loop is required here for multiple user connections
            AcceptClients(listener);

            // Loop to continuously send messages without ending the program
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Write message: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);

                foreach (TcpClient client in clients)
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
        }

        // Method handles accepting clients
        // Loop ensures multiple users are able to be accepted and added to List
        // Async branches off for every client accepted
        public async void AcceptClients(TcpListener listener)
        {
            bool isRunning = true;
            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessages(stream);
            }
        }

        // Method handles incomming data from all users on stream
        // Loop ensures program doesn't end
        public async void ReceiveMessages(NetworkStream stream)
        {
            // int counter = 0;

            byte[] buffer = new byte[256];
            bool isRunning = true;
            while (isRunning)
            {

                // Jeg prøvede at lave et ID baseret på 2 listers værdier
                // Men jeg nåede det ikke før dagen var omme.
                /*
                if (counter == 0)
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        usernames[i] = $"{clients} + {usernames}";
                    }
                }
                */



                int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine("client writes: " + text);
                
                // This loop works as psudo-broadcast
                // It writes received messages to all clients in the List
                foreach (TcpClient client in clients)
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
