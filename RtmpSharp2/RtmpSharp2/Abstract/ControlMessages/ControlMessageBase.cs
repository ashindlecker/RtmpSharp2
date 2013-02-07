using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract.ControlMessages
{
    public abstract class ControlMessageBase : Chunk
    {
        public ControlMessageBase(byte type)
        {
            BHeader.ChunkStreamId = 2;
            MHeader.MessageStreamId = 0;
            MHeader.MessageType = type;
        }
    }
}
