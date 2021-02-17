using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.PLC
{
    public class PLCSndEdit : BaseActor
    {
        private readonly ISysAkkaManager _akkaManager;

        public PLCSndEdit(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;

            Receive<String>(message => ProStr(message));
        }

        private void ProStr(String msg)
        {
            Console.WriteLine("[Info] PLCSndEdit Rcv Data " + msg);

            switch (msg)
            {
                case "tracking":
                    Console.WriteLine($"[Info] PLCSndEdit -> switch msg case {msg}");
                    _akkaManager.GetActor(nameof(PLCSnd)).Tell(msg);
                    break;
                case "hello":
                    Console.WriteLine($"[Info] PLCSndEdit -> switch msg case {msg}");
                    _akkaManager.GetActor(nameof(PLCSnd)).Tell(msg);
                    break;
                default:
                    Console.WriteLine($"[Info] PLCSndEdit -> switch msg case default, msg={msg}");
                    break;
            }
        }
    }
}
