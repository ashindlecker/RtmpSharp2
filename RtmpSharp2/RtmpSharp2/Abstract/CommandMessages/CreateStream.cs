using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract.CommandMessages
{
    public class CreateStream : CommandMessage
    {
        public const int TransactionId = 2;

        public CreateStream()
        {
            ApplyValues();
        }

        public void ApplyValues()
        {
            var writer = new AmfWriter();

            writer.WriteString("createStream");
            writer.WriteNumber(TransactionId);
            writer.WriteNull();
            Data = writer.GetByteArray();
        }
    }
}
