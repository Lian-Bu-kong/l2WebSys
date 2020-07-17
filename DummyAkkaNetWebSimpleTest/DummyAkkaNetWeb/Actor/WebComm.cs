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
        private readonly string _actorPath = "akka.tcp://MMSAkkaSys@127.0.0.1:8201/user/MMSMgr/MMSSndEdit";

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
                    Console.WriteLine($"[Info] WebComm -> switch msg case {msg}");
                    _akkaManager.GetActorSelection($"{_actorPath}").Tell(msg);
                    break;
                case "hello":
                    Console.WriteLine($"[Info] WebComm -> switch msg case {msg}");
                    _akkaManager.GetActorSelection($"{_actorPath}").Tell(msg);
                    break;
                case "broadcast":
                    Broadcast(msg);
                    break;
                default:
                    Console.WriteLine($"[Info] WebComm -> switch msg case default, msg={msg}");
                    break;
            }
        }

        private void RcvAny(object message)
        {
            Console.WriteLine("[Error] WebComm -> Rcv Any " + message);
        }

        private async void Broadcast(string msg)
        {
            Console.WriteLine($"[Info] WebComm -> broadcast all client, msg={msg}");

            await _hub.SendMessage("msg", msg);
        }
    }
}
