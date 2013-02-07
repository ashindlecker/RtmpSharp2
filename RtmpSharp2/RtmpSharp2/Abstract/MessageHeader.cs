using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace RtmpSharp2.Abstract
{
    public class MessageHeader
    {
        public BasicHeader.HeaderFormats Format;

        public int TimeStamp;       //3 Bytes when sent
        public int MessageLength;   //3 Bytes when sent
        public int MessageStreamId; //4 Bytes in little endian order


        public byte MessageType;

        public MessageHeader()
        {
            TimeStamp = 0;
            MessageLength = 0;
            MessageStreamId = 0;
            Format = BasicHeader.HeaderFormats.F1;
            MessageType = 0x00;
        }

        public byte[] ToBytes()
        {
            var memory = new MemoryStream();
            var writer = new EndianBinaryWriter(EndianBitConverter.Big, memory);

            switch (Format)
            {
                case BasicHeader.HeaderFormats.F0: //11 bytes
                    writer.Write(EndianBitConverter.Big.GetBytes(TimeStamp), 1, 3);
                    writer.Write(EndianBitConverter.Big.GetBytes(MessageLength), 1, 3);
                    writer.Write((byte) MessageType);
                    writer.Write(EndianBitConverter.Little.GetBytes(MessageStreamId));
                    break;
                case BasicHeader.HeaderFormats.F1: //7 bytes
                    writer.Write(EndianBitConverter.Big.GetBytes(TimeStamp), 1, 3);
                    writer.Write(EndianBitConverter.Big.GetBytes(MessageLength), 1, 3);
                    writer.Write((byte) MessageType);
                    break;
                case BasicHeader.HeaderFormats.F2: //3 bytes
                    writer.Write(EndianBitConverter.Big.GetBytes(TimeStamp), 1, 3);
                    break;
                case BasicHeader.HeaderFormats.F3: //No bytes
                    break;
            }
            return memory.ToArray();
        }

        public void Load(MemoryStream memory)
        {
            var reader = new EndianBinaryReader(EndianBitConverter.Big, memory);
            switch (Format)
            {
                case BasicHeader.HeaderFormats.F0: //11 bytes
                    {
                        var timeStampByte = new byte[4] {0x00, reader.ReadByte(), reader.ReadByte(), reader.ReadByte()};
                        TimeStamp = EndianBitConverter.Big.ToInt32(timeStampByte, 0);
                        var lengthByte = new byte[4] {0x00, reader.ReadByte(), reader.ReadByte(), reader.ReadByte()};
                        MessageLength = EndianBitConverter.Big.ToInt32(lengthByte, 0);
                        MessageType = reader.ReadByte();
                        var messageStreamBytes = reader.ReadBytes(4);
                        MessageStreamId = EndianBitConverter.Little.ToInt32(messageStreamBytes, 0);
                    }
                    break;
                case BasicHeader.HeaderFormats.F1: //7 bytes
                    {
                        var timeStampByte = new byte[4] {0x00, reader.ReadByte(), reader.ReadByte(), reader.ReadByte()};
                        TimeStamp = EndianBitConverter.Big.ToInt32(timeStampByte, 0);
                        var lengthByte = new byte[4] {0x00, reader.ReadByte(), reader.ReadByte(), reader.ReadByte()};
                        MessageLength = EndianBitConverter.Big.ToInt32(lengthByte, 0);
                        MessageType = reader.ReadByte();
                    }
                    break;
                case BasicHeader.HeaderFormats.F2: //3 bytes
                    {
                        var timeStampByte = new byte[4] {0x00, reader.ReadByte(), reader.ReadByte(), reader.ReadByte()};
                        TimeStamp = EndianBitConverter.Big.ToInt32(timeStampByte, 0);
                    }
                    break;
                case BasicHeader.HeaderFormats.F3: //No bytes
                    break;
            }
        }
    }
}
