using System;
using System.Collections.Generic;

namespace MultiClient_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Class_Server server = new Class_Server();
            server.MyServer();
        }
    }
}
