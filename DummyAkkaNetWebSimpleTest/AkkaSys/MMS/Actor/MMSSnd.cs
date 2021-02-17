using Akka.Actor;
using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.MMS
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Snd Actor (TCP連線發送)
    **/
    public class MMSSnd : BaseClientActor
    {
        public MMSSnd(AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            Receive<string>(message => ProStr(message));
        }

        private void ProStr(string msg)
        {
            switch (msg)
            {
                case "schedule":
                    Console.WriteLine($"[Info] MMSSnd -> switch msg case {msg}");
                    _connection.Tell(Tcp.Write.Create(ByteString.FromString(msg)));
                    break;
                case "hello":
                    Console.WriteLine($"[Info] MMSSnd -> switch msg case {msg}");
                    _connection.Tell(Tcp.Write.Create(ByteString.FromString(msg)));
                    break;
                default:
                    Console.WriteLine($"[Info] MMSSnd -> switch msg case default, msg={msg}");
                    break;
            }
        }
    }
}
