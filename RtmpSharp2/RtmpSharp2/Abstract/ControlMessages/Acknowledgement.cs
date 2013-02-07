using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class Acknowledgement : ControlMessageBase
    {
        public int BytesReceived;
        public Acknowledgement(int bytesReceived) : base(3)
        {
            BytesReceived = bytesReceived;
            Data = EndianBitConverter.Big.GetBytes(BytesReceived);
        }


        protected override void ParseChunkData(System.IO.MemoryStream memory)
        {
            base.ParseChunkData(memory);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, memory);
            BytesReceived = reader.ReadInt32();
        }
    }
}
