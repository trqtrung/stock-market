using Newtonsoft.Json.Linq;
using System;
using System.Web.Helpers;
using WebSocketSharp;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://socket.vcsc.com.vn:7000/websocket"))
            {
                ws.OnMessage += (sender, e) =>
                {
                    Console.WriteLine(e.Data);
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        dynamic data = JObject.Parse(e.Data);//Json.Decode(e.Data);
                        string service = data.service;
                        if (!string.IsNullOrEmpty(service) && service =="stockinfo")
                        {
                            var content = data.content;
                            Console.WriteLine(service);
                        }
                    }
                };

                ws.OnError += (sender, e) =>
                    Console.WriteLine("Error: " + e.Message);

                ws.Connect();
                Console.ReadKey(true);
            }
        }
    }
}
