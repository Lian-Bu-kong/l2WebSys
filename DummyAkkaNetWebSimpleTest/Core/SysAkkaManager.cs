using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Configuration;
using Akka.DI.Core;
using Core;
using System;
using System.Collections.Generic;

namespace MMSComm
{
    public class SysAkkaManager : ISysAkkaManager
    {
        // 系統Akka
        public ActorSystem ActorSystem { get; }

        private readonly Dictionary<string, IActorRef> _actorDics = new Dictionary<string, IActorRef>();

        public SysAkkaManager(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        /// <summary>
        /// 不建議使用此方式，建議使用
        /// <see cref="CreateActor{T}"/>
        /// </summary>
        public IActorRef CreateActor<T>(string ip, int port) where T : ActorBase
        {
            var actName = typeof(T).Name;
            if (_actorDics.ContainsKey(actName)) return _actorDics[actName];
            var actor = ActorSystem.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), ip, port)));
            return RegisterActor(actName, actor);
        }

        public IActorRef CreateActor<T>() where T : ActorBase
        {
            return CreateActor<T>(() => ActorSystem);
        }

        private IActorRef RegisterActor(string actName, IActorRef actor)
        {
            if (_actorDics.ContainsKey(actName)) throw new ArgumentException($"You have been register Action {actName}");
            _actorDics.Add(actName, actor);
            return actor;
        }

        public IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase
        {
            return CreateActor<T>(() => context);
        }

        private IActorRef CreateActor<T>(Func<IActorRefFactory> func) where T : ActorBase
        {
            var actName = typeof(T).Name;
            if (_actorDics.ContainsKey(actName)) return _actorDics[actName];
            return RegisterActor(actName, func().ActorOf(ActorSystem.DI().Props<T>(), typeof(T).Name));
        }
    }
}
