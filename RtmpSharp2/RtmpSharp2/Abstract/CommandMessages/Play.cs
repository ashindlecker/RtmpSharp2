using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract.CommandMessages
{
    public class Play : CommandMessage
    {
        public const int TransactionId = 0;
        public string StreamName;

        public Play(string stream = "none")
        {
            MHeader.MessageStreamId = 1;
            StreamName = stream;
            ApplyValues();
        }

        public void ApplyValues()
        {
            var writer = new AmfWriter();

            writer.WriteString("play");
            writer.WriteNumber(TransactionId);
            writer.WriteNull();
            writer.WriteString(StreamName);
            Data = writer.GetByteArray();
        }
    }
}
