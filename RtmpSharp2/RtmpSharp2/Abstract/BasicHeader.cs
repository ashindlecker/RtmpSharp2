using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;
using MiscUtil.IO;

namespace RtmpSharp2.Abstract
{
    public class BasicHeader : ISendable
    {
        public enum HeaderFormats : byte
        {
            F0 = 0x00,
            F1 = 0x01,
            F2 = 0x02,
            F3 = 0x03,
        }

        public HeaderFormats Format
        {
            /*
            get
            {
                if (ChunkStreamId <= 63)
                    return HeaderFormats.F0;
                if (ChunkStreamId <= 319)
                    return HeaderFormats.F1;
                return HeaderFormats.F2;
            }*/
            get; set; 
        }

        public ushort ChunkStreamId;
        public BasicHeader()
        {
            ChunkStreamId = 0;
        }

        public byte[] ToBytes()
        {
            var memory = new MemoryStream();
            var writer = new EndianBinaryWriter(EndianBitConverter.Big, memory);

            var formatData = (byte) Format;
            formatData <<= 6;

            formatData |= (byte)ChunkStreamId;
            writer.Write(formatData);

            /*
            switch (Format)
            {
                case HeaderFormats.F0:
                    {
                        formatData |= (byte)ChunkStreamId;
                        writer.Write(formatData);
                    }
                    break;
                case HeaderFormats.F1:
                    {
                        writer.Write(formatData);
                        writer.Write((byte)ChunkStreamId);
                    }
                    break;
                case HeaderFormats.F2:
                    {
                        writer.Write(formatData);
                        writer.Write(ChunkStreamId);
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            */
            return memory.ToArray();
        }

        public void Load(MemoryStream memory)
        {
            var reader = new EndianBinaryReader(EndianBitConverter.Big, memory);

            var format = reader.ReadByte();
            var copyByte = format;

            format &= 0xC0;
            format >>= 6;
            Format = (HeaderFormats)format;
            copyByte &= 0x3F;
            ChunkStreamId = copyByte;

            /*
            switch ((HeaderFormats) format)
            {
                case HeaderFormats.F0:
                    {
                        copyByte &= 0x3F;
                        ChunkStreamId = copyByte;
                    }
                    break;
                case HeaderFormats.F1:
                    {
                        ChunkStreamId = reader.ReadByte();
                    }
                    break;
                case HeaderFormats.F2:
                    {
                        ChunkStreamId = reader.ReadUInt16();
                    }
                    break;
                case HeaderFormats.F3:
                    break;
                default:
                    break;
            }*/
        }
    }
}
