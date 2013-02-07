using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract.CommandMessages
{
    public class Connect : CommandMessage
    {
        public string App = "app";
        public string FlashVersion = "FMSc/1.0";
        public string SwfUrl = "NONE";
        public string ServerUrl = "NONE";
        public bool Proxy = false;
        public int AudioCodecs = 0x0FFF;
        public int VideoCodecs = 0x00FF;
        public int VideoFunction = 1;
        public string PageUrl = "NONE";
        public int ObjectEncoding = 0x00; //AMF0 is only supported atm


        private const int TransactionId = 1;

        public Connect()
        {
            ApplyValues();
        }

        public void ApplyValues()
        {
            var writer = new AmfWriter();

            writer.WriteString("connect");
            writer.WriteNumber(TransactionId);

            var obj = new AmfObject();
            obj.Strings.Add("app", App);
            obj.Strings.Add("flashver", FlashVersion);
            obj.Strings.Add("swfUrl", SwfUrl);
            obj.Strings.Add("tcUrl", ServerUrl);
            obj.Booleans.Add("fpad", Proxy);
            obj.Numbers.Add("audioCodecs", AudioCodecs);
            obj.Numbers.Add("videoCodecs", VideoCodecs);
            obj.Numbers.Add("videoFunction", VideoFunction);
            obj.Strings.Add("pageUrl", PageUrl);
            obj.Numbers.Add("objectEncoding", ObjectEncoding);

            writer.WriteObject(obj);
            Data = writer.GetByteArray();
        }
    }
}
