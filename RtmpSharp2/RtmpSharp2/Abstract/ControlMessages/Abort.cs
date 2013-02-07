using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class Abort : ControlMessageBase
    {
        public Abort() : base(2)
        {
            Data = EndianBitConverter.Big.GetBytes((int) BHeader.ChunkStreamId);
        }
    }
}
