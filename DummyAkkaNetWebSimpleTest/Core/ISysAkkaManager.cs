using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface ISysAkkaManager
    {
        void CreateActor<T>(string ip, int port) where T : ActorBase;

        void CreateActor<T>() where T : ActorBase;

    }
}
