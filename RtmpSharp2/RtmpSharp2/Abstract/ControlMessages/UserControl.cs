using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class UserControl : ControlMessageBase
    {
        public enum EventTypes : ushort
        {
            StreamBegin = 0x00,
            StreamEOF = 0x01,
            StreamDry = 0x02,
            SetBufferLength = 0x03,
            StreamIsRecorded = 0x04,
            PingRequest = 0x06,
            PingResponse = 0x07,
        }

        public UserControl(EventTypes eventType, int streamId = 1, int bufferlength = 1000, int pingTime = 0) : base(4)
        {
            var memory = new MemoryStream();
            var writer = new EndianBinaryWriter(EndianBitConverter.Big, memory);
            writer.Write((ushort) eventType);

            switch (eventType)
            {
                case EventTypes.StreamBegin:
                    writer.Write(streamId);
                    break;
                case EventTypes.StreamEOF:
                    writer.Write(streamId);
                    break;
                case EventTypes.StreamDry:
                    writer.Write(streamId);
                    break;
                case EventTypes.SetBufferLength:
                    writer.Write(streamId);
                    writer.Write(bufferlength);
                    break;
                case EventTypes.StreamIsRecorded:
                    writer.Write(streamId);
                    break;
                case EventTypes.PingRequest:
                    writer.Write(pingTime);
                    break;
                case EventTypes.PingResponse:
                    writer.Write(pingTime);
                    break;
                default:
                    break;
            }
            Data = memory.ToArray();
        }
    }
}
