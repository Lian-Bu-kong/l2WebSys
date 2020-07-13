using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using DummyAkkaNetWeb.Hubs;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Actor
{
    public class WebComm : BaseActor
    {
        private readonly ChatHub _hub;

        public WebComm(ChatHub hub)
        {
            _hub = hub;
            //await _hub.SendMessage("123", "123");
            Receive<string>(message => GetStrMsg(message));
        }

        private async void GetStrMsg(string msg)
        {
            await _hub.SendMessage("msg", "msg");
        }
    }
}
