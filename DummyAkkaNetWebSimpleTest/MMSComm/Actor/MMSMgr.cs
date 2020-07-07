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
        public TestDI TestDI { get; }

        //public MMSMgr(TestDI testDI)
        //{
        //    // 透過依賴注入的方式取得 TestDI
        //    TestDI = testDI;
        //}

        public MMSMgr(ISysAkkaManager akkaManager)
        {
            akkaManager.CreateActor<MMSRcv>();
            akkaManager.CreateActor<MMSRcvEdit>();
        }
    }

    public class TestDI
    {

    }
}
