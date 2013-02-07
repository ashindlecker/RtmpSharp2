using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class WindowAcknowledgement : ControlMessageBase
    {
        public WindowAcknowledgement(int windowSize) : base(5)
        {
            Data = EndianBitConverter.Big.GetBytes(windowSize);
        }
    }
}
