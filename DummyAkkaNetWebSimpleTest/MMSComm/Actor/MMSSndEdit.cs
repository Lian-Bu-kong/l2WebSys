using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace MMSComm.Actor
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Snd Edit Actor (解析資料)
    **/
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
                    Console.WriteLine($"[Info] MMSSndEdit -> switch msg case schedule, nsg={msg}");
                    _akkaManager.GetActor(nameof(MMSSnd)).Tell("schedule");
                    break;
                default:
                    Console.WriteLine($"[Info] MMSSndEdit -> switch msg case default, nsg={msg}");
                    break;
            }
        }
    }

  
}
