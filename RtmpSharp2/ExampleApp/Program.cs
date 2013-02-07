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
            var cl = new Client("199.9.254.78", 1935);
            cl.StartHandshake();

            bool connect = false;
            while (true)
            {
                //Console.WriteLine(cl.CurrentState);
                cl.Update();

                if (cl.CurrentState == ClientBase.ClientStates.Handshake_Done && !connect)
                {
                    connect = true;
                    cl.SendMessage(new RtmpSharp2.Abstract.ControlMessages.SetChunkSize(10000));
                    cl.SendMessage(new RtmpSharp2.Abstract.CommandMessages.Connect());
                }
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
