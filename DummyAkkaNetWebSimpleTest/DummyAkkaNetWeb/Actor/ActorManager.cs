using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Actor
{
    public class ActorManager
    {
        private ActorSystem _actorManager;

        public IActorRef CommActor { get; }

        public ActorManager(string actorSysName, Config config)
        {
            _actorManager = ActorSystem.Create(actorSysName, config);

            // 啟用此系統相關Actor
            CommActor = _actorManager.ActorOf(Props.Create(typeof(CommActor)), nameof(Actor.CommActor));
        }

     
    }
}
