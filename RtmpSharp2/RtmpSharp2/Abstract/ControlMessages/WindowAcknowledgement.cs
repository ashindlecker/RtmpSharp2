using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class WindowAcknowledgement : ControlMessageBase
    {
        public int WindowSize;

        public WindowAcknowledgement(int windowSize) : base(5)
        {
            WindowSize = windowSize;
            Data = EndianBitConverter.Big.GetBytes(WindowSize);
        }

        protected override void ParseChunkData(System.IO.MemoryStream memory)
        {
            base.ParseChunkData(memory);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, memory);
            WindowSize = reader.ReadInt32();
        }
    }
}
