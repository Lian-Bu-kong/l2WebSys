using Akka.Actor;
using Akka.Configuration;
using Akka.IO;
using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMSComm
{
    public class MMSMgr : ReceiveActor
    {
        private readonly IActorRef _mmsRcv;
        private readonly IActorRef _mmsRcvEdit;

        public MMSMgr(ISysAkkaManager akkaManager)
        {
            _mmsRcv = akkaManager.CreateChildActor<MMSRcv>(Context);
            _mmsRcvEdit = akkaManager.CreateChildActor<MMSRcvEdit>(Context);
        }
    }

    public class TestDI
    {

    }
}
