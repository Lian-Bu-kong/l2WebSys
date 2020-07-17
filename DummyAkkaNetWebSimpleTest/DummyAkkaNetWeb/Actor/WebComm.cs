using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using DummyAkkaNetWeb.Hubs;
using System;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Actor
{
    public class WebComm : BaseActor
    {
        private readonly ISysAkkaManager _akkaManager;
        private readonly ChatHub _hub;

        public WebComm(ISysAkkaManager akkaManager, ChatHub hub)
        {
            _akkaManager = akkaManager;
            _hub = hub;

            //await _hub.SendMessage("123", "123");
            Receive<string>(message => GetStrMsg(message));
            ReceiveAny(message => RcvAny(message));
        }

        private void GetStrMsg(string msg)
        {
            switch (msg)
            {
                case "schedule":
                    _akkaManager.GetActorSelection("akka.tcp://MMSAkkaSys@127.0.0.1:8201/user/MMSMgr/MMSSndEdit").Tell("schedule");
                    break;
                case "broadcast":
                    broadcast();
                    break;
                default:
                    Console.WriteLine($"[Info] WebComm -> switch msg case default, nsg={msg}");
                    break;
            }
        }

        private void RcvAny(object message)
        {
            Console.WriteLine("[Error] WebComm -> Rcv Any " + message);
        }

        private async void broadcast()
        {
            await _hub.SendMessage("msg", "msg");
        }
    }
}
