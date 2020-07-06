using Akka.Actor;
using Akka.Configuration;
using Core;
using System;
using System.Collections.Generic;

namespace MMSComm
{
    public class SysAkkaManager : ISysAkkaManager
    {
        // 系統Akka
        public ActorSystem actSystem { get; }

        private Dictionary<string, IActorRef> _actorDics { get; }

        public SysAkkaManager(string actorSysName, Config config, Dictionary<string, IActorRef> dics)
        {
            _actorDics = dics;
            actSystem = ActorSystem.Create(actorSysName, config);        
        }

        public void CreateActor<T>(string ip, int port) where T : ActorBase
        {
            var actName = typeof(T).Name;
            var actor = actSystem.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), ip, port)));
            _actorDics.Add(actName, actor);
        }
        
        public void CreateActor<T>() where T : ActorBase
        {
            var actName = typeof(T).Name;
            var actor = actSystem.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T))));
            _actorDics.Add(actName, actor);           
        }
  
    }
}
