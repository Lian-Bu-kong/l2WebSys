using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Configuration;
using Core;
using System;
using System.Collections.Generic;

namespace MMSComm
{
    public class SysAkkaManager : ISysAkkaManager
    {
        // 系統Akka
        private readonly ActorSystem _actSystem;

        private readonly Dictionary<string, IActorRef> _actorDics = new Dictionary<string, IActorRef>();

        public SysAkkaManager(ActorSystem actSystem)
        {
            _actSystem = actSystem;
        }

        public IActorRef CreateActor<T>(string ip, int port) where T : ActorBase
        { 
            return CreateActor<T>(typeof(T).Name, () => Activator.CreateInstance(typeof(T), ip, port));
        }
        
        public IActorRef CreateActor<T>() where T : ActorBase
        {
            return CreateActor<T>(typeof(T).Name, () => Activator.CreateInstance(typeof(T)));
        }

        private IActorRef CreateActor<T>(string actName, Func<object> func) where T : ActorBase
        {
            if (_actorDics.ContainsKey(actName)) return _actorDics[actName];
            var actor = _actSystem.ActorOf(Props.Create(() => (T)func()));
            _actorDics.Add(actName, actor);
            return actor;
        }
    }
}
