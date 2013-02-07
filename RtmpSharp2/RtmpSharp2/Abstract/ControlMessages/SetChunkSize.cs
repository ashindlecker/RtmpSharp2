using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class SetChunkSize : ControlMessageBase
    {
        public SetChunkSize(int size) : base(1)
        {
            Data = EndianBitConverter.Big.GetBytes(size & 0x7FFFFFFF);
        }
    }
}
