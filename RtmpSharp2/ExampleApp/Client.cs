using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtmpSharp2;
using System.Net;
using System.Net.Sockets;
using RtmpSharp2.Abstract;
using RtmpSharp2.Abstract.CommandMessages;

namespace ExampleApp
{
    class Client : RtmpSharp2.Abstract.ClientBase
    {
        private TcpClient client;
        private bool _connect = true;
        private bool _sendToken = true;
        private TwitchUsher _usher;

        public Client(string ip, int port, TwitchUsher usher)
        {
            client = new TcpClient(ip, port);
            client.Client.Blocking = false;
            client.NoDelay = true;
            client.SendBufferSize = 10000;
            client.ReceiveBufferSize = 10000;
            _usher = usher;
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

        public override void Update()
        {
            base.Update();
            if (CurrentState == ClientStates.Handshake_Done)
            {
                if (_connect)
                {
                    _connect = false;

                    SendMessage(new RtmpSharp2.Abstract.ControlMessages.SetChunkSize(10000));
                    var connect = new Connect();
                    connect.SwfUrl = "http://www-cdn.jtvnw.net/widgets/live_facebook_embed_player.swf";
                    connect.ApplyValues();
                    SendMessage(connect);
                }
            }
        }

        protected override void ParseChunk(Chunk chunk)
        {
            base.ParseChunk(chunk);
            if (_sendToken)
            {
                _sendToken = false;
                sendToken();
            }

        }

        private void sendToken()
        {
            if (_usher.Streams.Count == 0)
                return;

            AmfWriter writer = new AmfWriter();

            writer.WriteString("NetStream.Authenticate.UsherToken");
            writer.WriteNumber(0);
            writer.WriteNull();
            writer.WriteString(_usher.Streams[0].Token);

            RtmpSharp2.Abstract.CommandMessage message = new CommandMessage(writer);

            SendMessage(message);
            SendMessage(new CreateStream());
            SendMessage(new Play(_usher.Streams[0].PlayStream));
        }
    }
}
