using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract
{
    public class Globals
    {
        public const int Handshake_Length = 1536;
        public enum Amf0Types : byte
        {
            Number = 0x00,
            Boolean = 0x01,
            String = 0x02,
            Object = 0x03,
            Null = 0x05,
            Array = 0x08,
            ObjectEnd = 0x09,
        }
    }
}
