using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class Acknowledgement : ControlMessageBase
    {
        public Acknowledgement(int bytesReceived) : base(3)
        {
            Data = EndianBitConverter.Big.GetBytes(bytesReceived);
        }
    }
}
