using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Linq;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AppServer server = new AppServer("127.0.0.1", 2012);
            server.Start();
            Console.ReadKey();
        }
    }
}
