using AkkaBase;
using AkkaBase.Base;
using AkkaSys.ZigBee.Actor;

namespace AkkaSys.ZigBee
{
    public class ZigBeeMgr :　BaseActor
    {
        public ZigBeeMgr(ISysAkkaManager akkaManager)
        {
            akkaManager.CreateChildActor<ZigBeeRcv>(Context);
        }
    }
}
