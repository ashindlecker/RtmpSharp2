using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract
{
    public interface ISendable
    {
        byte[] ToBytes();
        void Load(MemoryStream bytes);
    }
}
