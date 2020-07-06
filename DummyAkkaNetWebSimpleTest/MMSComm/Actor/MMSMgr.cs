using Akka.Actor;
using Akka.Configuration;
using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMSComm
{
    public class MMSMgr : ReceiveActor
    {
        public MMSMgr()
        {

        }

        //public MMSMgr(ISysAkkaManager actorManager)
        //{
        //    // 啟用此系統相關Actor
        //    actorManager.CreateActor<MMSRcv>(Configure.LocalSysIp, Configure.LocalSysPort);
        //    actorManager.CreateActor<MMSRcvEdit>();
        //}
        //public MMSMgr()
        //{
        //    var d = "";
        //    var e = "";
        //}

    }
}
