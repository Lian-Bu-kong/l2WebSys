using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMSComm
{
    public class AkkaServerEngine
    {
        private readonly IActorRef _mmsMgrActor;

        // 所有Acotor使用的場景，要新增就修改建構子
        public AkkaServerEngine(IActorRef mmsMgrActor)
        {
            _mmsMgrActor = mmsMgrActor;
        }

        // 所有Acotor使用的商業邏輯，寫在下方
        public void Start()
        {
            _mmsMgrActor.Tell("Hello");
        }
    }
}
