using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.MMS
{
    public class MMSSndEdit : BaseActor
    {
        private readonly ISysAkkaManager _akkaManager;

        public MMSSndEdit(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;

            Receive<String>(message => ProStr(message));
        }

        private void ProStr(String msg)
        {
            Console.WriteLine("[Info] MMSSndEdit Rcv Data " + msg);

            switch (msg)
            {
                case "schedule":
                    Console.WriteLine($"[Info] MMSSndEdit -> switch msg case {msg}");
                    _akkaManager.GetActor(nameof(MMSSnd)).Tell(msg);
                    break;
                case "hello":
                    Console.WriteLine($"[Info] MMSSndEdit -> switch msg case {msg}");
                    _akkaManager.GetActor(nameof(MMSSnd)).Tell(msg);
                    break;
                default:
                    Console.WriteLine($"[Info] MMSSndEdit -> switch msg case default, msg={msg}");
                    break;
            }
        }
    }
}
