using Akka.Actor;
using AkkaBase;
using AkkaSys.MMS;
using AkkaSys.PLC;
using AkkaSys.Sharp7;
using AkkaSys.WMS;

namespace DummyAkkaNetWeb.Actor
{
    public class AkkaServerEngine
    {
        // 所有Acotor使用的場景，要新增就修改建構子
        public AkkaServerEngine(ISysAkkaManager akkaManager)
        {

            //akkaManager.CreateActor<MMSMgr>();
            //akkaManager.CreateActor<WMSMgr>();
            //akkaManager.CreateActor<PlcMgr>();
            akkaManager.CreateActor<Sharp7Mgr>();
            //_mgrActor = mgrActor;
        }


        //所有Acotor使用的商業邏輯，寫在下方
        //public void Start()
        //{
        //}
    }
}
