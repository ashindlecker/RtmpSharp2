using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtmpSharp2.Abstract;

namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RtmpSharp2.DebugAnnounce.Announce += delegate(string str) { Console.WriteLine(str); };

            var twitchUsher = TwitchUsher.FromUser("day9tv");
            if (twitchUsher.Streams.Count == 0)
            {
                Console.WriteLine("This channel doesn't exist or is not live");
                return;
            }
            var cl = new Client(twitchUsher.Streams[0].Server, 1935, twitchUsher);
            cl.StartHandshake();

            bool connect = false;
            while (true)
            {
                //Console.WriteLine(cl.CurrentState);
                cl.Update();
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
