using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;

namespace CoilMgr
{
    public class CoilMgr :BaseActor
    {
        private readonly ActorSelection _mmsSndEdit;


        public CoilMgr(ISysAkkaManager akkaManager)
        {
            _mmsSndEdit = Context.ActorSelection("akka.tcp://MMSAkkaSys@127.0.0.1:8201/user/MMSMgr/MMSSndEdit");

            _mmsSndEdit.Tell("123");

        }
    }
}
