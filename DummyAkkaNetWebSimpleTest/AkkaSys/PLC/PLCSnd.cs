using Akka.Actor;
using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.PLC
{
    /**
    * Author :ICSC 余士鵬
    * Desc : PLC Snd Actor (TCP連線發送)
    **/
    public class PLCSnd : BaseClientActor
    {
        public PLCSnd(AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            Receive<string>(message => ProStr(message));
        }

        private void ProStr(string msg)
        {
            switch (msg)
            {
                case "hello":
                    Console.WriteLine($"[Info] PLCSnd -> switch msg case {msg}");
                    _connection.Tell(Tcp.Write.Create(ByteString.FromString(msg)));
                    break;
                default:
                    Console.WriteLine($"[Info] PLCSnd -> switch msg case default, msg={msg}");
                    break;
            }
        }
    }
}
