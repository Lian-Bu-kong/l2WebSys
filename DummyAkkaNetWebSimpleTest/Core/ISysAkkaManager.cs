using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ISysAkkaManager
    {
        IActorRef CreateActor<T>(string ip, int port) where T : ActorBase;

        IActorRef CreateActor<T>() where T : ActorBase;
    }
}
