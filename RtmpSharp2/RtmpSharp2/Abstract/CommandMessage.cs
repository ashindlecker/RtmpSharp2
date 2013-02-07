using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;

namespace RtmpSharp2.Abstract
{
    public class CommandMessage : Chunk
    {

        public CommandMessage(byte[] amfData)
        {
            MHeader.MessageStreamId = 0;
            BHeader.ChunkStreamId = 3;
            MHeader.MessageType = 0x14; //AMF0 encoding is only supported as of now
            Data = amfData;
        }

        public CommandMessage()
        {
            MHeader.MessageStreamId = 0;
            BHeader.ChunkStreamId = 3;
            MHeader.MessageType = 0x14; //AMF0 encoding is only supported as of now
        }

        public CommandMessage(AmfWriter amfData)
        {
            MHeader.MessageStreamId = 0;
            BHeader.ChunkStreamId = 3;
            MHeader.MessageType = 0x14; //AMF0 encoding is only supported as of now
            Data = amfData.GetByteArray();
        }

        protected override void ParseChunkData(System.IO.MemoryStream memory)
        {
            var reader = new AmfReader();
            reader.Parse(new EndianBinaryReader(EndianBitConverter.Big, memory), (uint)MHeader.MessageLength);
        }
    }
}
