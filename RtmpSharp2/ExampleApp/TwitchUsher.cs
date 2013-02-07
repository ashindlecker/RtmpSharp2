using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace ExampleApp
{
    public class TwitchUsher
    {
        public class StreamData
        {
            public string PlayStream;
            public string VideoHeight;
            public string Token;
            public string Server;
        }

        public List<StreamData> Streams; 

        public TwitchUsher()
        {
            Streams = new List<StreamData>();
        }

        public static TwitchUsher FromUser(string user)
        {
            HttpWebRequest req =
                (HttpWebRequest)WebRequest.Create("http://usher.twitch.tv/find/" + user + ".xml?type=live");

            req.Method = "GET";

            var resp = req.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            var str = reader.ReadToEnd();

            var doc = new HtmlDocument();
            doc.LoadHtml(str);

            var ret = new TwitchUsher();

            try
            {
                foreach (var node in doc.DocumentNode.SelectNodes("//live"))
                {
                    var strData = new StreamData();

                    if (node.SelectSingleNode("play") != null)
                        strData.PlayStream = node.SelectSingleNode("play").InnerText;
                    if (node.SelectSingleNode("video_height") != null)
                        strData.VideoHeight = node.SelectSingleNode("video_height").InnerText;
                    if (node.SelectSingleNode("token") != null)
                        strData.Token = node.SelectSingleNode("token").InnerText;
                    if (node.SelectSingleNode("connect") != null)
                        strData.Server = node.SelectSingleNode("connect").InnerText;

                    if (strData.Server.Contains("rtmp://"))
                        strData.Server = strData.Server.Substring(("rtmp://").Length);

                    if (strData.Server.Contains("/app"))
                        strData.Server = strData.Server.Remove(strData.Server.IndexOf("/app"));


                    ret.Streams.Add(strData);
                }
            }
            catch(Exception ex){}
            return ret;
        }
    }
}
