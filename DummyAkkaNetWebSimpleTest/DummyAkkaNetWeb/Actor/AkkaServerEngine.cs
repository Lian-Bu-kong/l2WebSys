using Akka.Actor;
using AkkaBase;
using AkkaSys.MMS;
using AkkaSys.PLC;
using AkkaSys.WMS;

namespace DummyAkkaNetWeb.Actor
{
    public class AkkaServerEngine
    {
        //private readonly IActorRef _mgrActor;

        //// 所有Acotor使用的場景，要新增就修改建構子
        //public AkkaServerEngine(IActorRef mgrActor)
        //{
        //    _mgrActor = mgrActor;
        //}
        public AkkaServerEngine(ISysAkkaManager akkaManager)
        {

            akkaManager.CreateActor<MMSMgr>();
            akkaManager.CreateActor<WMSMgr>();
            akkaManager.CreateActor<PLCMgr>();
            //_mgrActor = mgrActor;
        }


        //// 所有Acotor使用的商業邏輯，寫在下方
        //public void Start()
        //{
        //    _mgrActor.Tell("Hello");
        //}
    }
}
