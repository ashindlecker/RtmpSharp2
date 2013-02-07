using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtmpSharp2;
using System.Net;
using System.Net.Sockets;

namespace ExampleApp
{
    class Client : RtmpSharp2.Abstract.ClientBase
    {
        private TcpClient client;

        public Client(string ip, int port)
        {
            client = new TcpClient(ip, port);
            client.Client.Blocking = false;
            client.NoDelay = true;
            client.SendBufferSize = 10000;
            client.ReceiveBufferSize = 10000;
        }

        protected override void SendData(byte[] array)
        {
            client.Client.Send(array);
        }

        protected override bool CanReceiveData()
        {
            return client.Available > 0;
        }

        protected override byte[] ReceivedData()
        {
            var buffer = new byte[client.Available];
            client.Client.Receive(buffer);
            return buffer;
        }

        protected override void Debug(string str)
        {
            base.Debug(str);
            Console.WriteLine(str);
        }
    }
}
