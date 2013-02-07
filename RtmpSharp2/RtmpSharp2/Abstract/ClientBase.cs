using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;
using System.IO;
using RtmpSharp2.Abstract.ControlMessages;

namespace RtmpSharp2.Abstract
{
    public abstract class ClientBase
    {
        public enum ClientStates
        {
            Handshake_WaitForS0,
            Handshake_WaitForS1,
            Handshake_WaitForS2,
            Handshake_Done,
        }

        public ClientStates CurrentState { get; private set; }

        public ClientBase()
        {
        }

        public void StartHandshake()
        {
            SendData(Handshake.GenerateC0());
            SendData(Handshake.GenerateC1());

            CurrentState = ClientStates.Handshake_WaitForS0;
        }

        public virtual void Update()
        {
            if (CanReceiveData())
            {
                var data = ReceivedData();
                var memory = new MemoryStream(data);
                var reader = new EndianBinaryReader(EndianBitConverter.Big, memory);

                while (memory.Position < memory.Length)
                {
                    switch (CurrentState)
                    {
                        case ClientStates.Handshake_WaitForS0:
                            {
                                reader.ReadByte();
                                CurrentState = ClientStates.Handshake_WaitForS1;
                            }
                            break;
                            case ClientStates.Handshake_WaitForS1:
                            {
                                var s1Chunk = reader.ReadBytes(Globals.Handshake_Length);
                                SendData(Handshake.GenerateC2(s1Chunk));
                                CurrentState = ClientStates.Handshake_WaitForS2;
                            }
                            break;
                            case ClientStates.Handshake_WaitForS2:
                            {
                                var s2Chunk = reader.ReadBytes(Globals.Handshake_Length);
                                CurrentState = ClientStates.Handshake_Done;
                            }
                            break;
                            case ClientStates.Handshake_Done:
                            {
                                var chunk = new Chunk();
                                chunk.Load(memory);
                                ParseChunk(chunk);
                            }
                            break;
                    }
                }
            }
        }

        public void SendMessage(Chunk chunk)
        {
            SendData(chunk.ToBytes());
        }

        protected virtual void ParseChunk(Chunk chunk)
        {
            //TODO: Add automatic responses to window acknowledgements

            if (chunk.MHeader.MessageType == 5 || chunk.MHeader.MessageType == 3) //This are acknowledgements
            {
                var ack = new WindowAcknowledgement(-1);
                ack.ParseChunkData(chunk);
                SendMessage(new WindowAcknowledgement(ack.WindowSize));
            }
        }

        protected virtual void Debug(string str)
        {

        }

        protected abstract void SendData(byte[] array);
        protected abstract bool CanReceiveData();
        protected abstract byte[] ReceivedData();
    }
}
