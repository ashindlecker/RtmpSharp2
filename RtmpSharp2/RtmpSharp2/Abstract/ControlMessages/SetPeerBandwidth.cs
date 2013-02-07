using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public class SetPeerBandwidth : ControlMessageBase
    {
        public enum LimitTypes : byte
        {
            Hard = 0x00,
            Soft = 0x01,
            Dynamic = 0x02,
        }

        public SetPeerBandwidth(int windowSize) : base(6)
        {
            Data = EndianBitConverter.Big.GetBytes(windowSize);
        }
    }
}
