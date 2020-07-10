using Akka.Actor;

namespace DummyAkkaNetWeb.Actor
{
    public class AkkaServerEngine
    {
        private readonly IActorRef _mgrActor;

        // 所有Acotor使用的場景，要新增就修改建構子
        public AkkaServerEngine(IActorRef mgrActor)
        {
            _mgrActor = mgrActor;
        }

        // 所有Acotor使用的商業邏輯，寫在下方
        public void Start()
        {
            _mgrActor.Tell("Hello");
        }
    }
}
