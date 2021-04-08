using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;

namespace AkkaSys.ZigBee.Actor
{
    public　class ZigBeeRcv : BaseServerActor
    {
        private readonly ISysAkkaManager _akkaManager;

        public ZigBeeRcv(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            _akkaManager = akkaManager;
     
        }
    }
}
