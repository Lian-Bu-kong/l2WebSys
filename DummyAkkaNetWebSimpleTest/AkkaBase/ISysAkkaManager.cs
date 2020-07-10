using Akka.Actor;
using Akka.Configuration;

namespace AkkaBase
{
    /**
    * Author :ICSC 余士鵬
    * Desc : System Actor Manager 
    **/
    public interface ISysAkkaManager
    {
        ActorSystem ActorSystem { get; }

        IActorRef CreateActor<T>() where T : ActorBase;

        IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase;

        IActorRef GetActor(string actName);
    }
}
