using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract
{
    public class Chunk : ISendable
    {
        public BasicHeader BHeader;
        public MessageHeader MHeader;
        public byte[] Data;

        public Chunk()
        {
            BHeader = new BasicHeader();
            MHeader = new MessageHeader();
            Data = null;
        }

        public byte[] ToBytes()
        {
            MHeader.Format = BHeader.Format;
            MHeader.MessageLength = 0;
            if (Data != null)
            {
                MHeader.MessageLength = Data.Length;
            }

            var memory = new MemoryStream();
            var writer = new EndianBinaryWriter(EndianBitConverter.Big, memory);
            writer.Write(BHeader.ToBytes());
            writer.Write(MHeader.ToBytes());
            if (Data != null)
            {
                writer.Write(Data);
            }
            return memory.ToArray();
        }

        public void Load(MemoryStream memory)
        {
            BHeader = new BasicHeader();
            BHeader.Load(memory);

            MHeader = new MessageHeader();
            MHeader.Format = BHeader.Format;
            MHeader.Load(memory);
            Data = new byte[MHeader.MessageLength];
            memory.Read(Data, 0, MHeader.MessageLength);
            var dataMemory = new MemoryStream(Data);
            ParseChunkData(dataMemory);
        }


        public void ParseChunkData(Chunk chunk)
        {
            var dataMemory = new MemoryStream(chunk.Data);
            ParseChunkData(dataMemory);
        }

        protected virtual void ParseChunkData(MemoryStream memory)
        {
            //This is overridden in inherrited classes, but is NOT NEEDED
        }
    }
}
