using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtmpSharp2.Abstract
{
    public class Handshake
    {
        private static Random _random = new Random();

        public static byte[] GenerateC0(byte version = 0x03)
        {
            return new byte[1] {version};
        }

        public static byte[] GenerateC1()
        {
            var data = new byte[Globals.Handshake_Length];
            for (var i = 0; i < Globals.Handshake_Length; i++)
            {
                data[i] = (byte)_random.Next(0, 255);
            }

            return data;
        }

        public static byte[] GenerateC2(byte[] s1)
        {
            //Atm, C2 is allowed to be a complete copy of s1
            return s1;
        }
    }
}
