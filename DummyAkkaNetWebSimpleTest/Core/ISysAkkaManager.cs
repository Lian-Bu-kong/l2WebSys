using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ISysAkkaManager
    {
        ActorSystem ActorSystem { get; }

        [Obsolete("不建議使用此方式建議使用，CreateActor<T>()取代")]
        IActorRef CreateActor<T>(string ip, int port) where T : ActorBase;

        IActorRef CreateActor<T>() where T : ActorBase;

        IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase;
    }
}
