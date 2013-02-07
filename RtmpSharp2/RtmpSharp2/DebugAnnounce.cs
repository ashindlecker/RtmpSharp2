using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2
{
    public class DebugAnnounce
    {
        public delegate void AnnounceDel(string str);
        public static event AnnounceDel Announce;

        public static void CallAnnounce(string str)
        {
            Announce(str);
        }
    }
}
