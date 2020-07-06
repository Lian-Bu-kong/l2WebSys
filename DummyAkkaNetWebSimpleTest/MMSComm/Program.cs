using Akka.Actor;
using Core;
using Microsoft.Extensions.DependencyInjection;

namespace MMSComm
{
    class Program
    {
        static void Main(string[] args)
        {
            //SysAkkaManager.actSystem = ActorSystem.Create(Configure.AkaSysName, Configure.AkkaConfig(Configure.AkaSysPort));
            //SysAkkaManager.CreateActor<MMSMgr>();
            Configure.DIManagerSetting();
            var actorSys = Configure.Provider.GetService<ISysAkkaManager>();
            actorSys.CreateActor<MMSMgr>();
            Configure.Provider.GetService<MMSMgr>();

            //// Create method takes a name for the new actor system (System Actor Manager Master) - 需下載 Akka Remote
            //var actorSystem = ActorSystem.Create(Configure.AkaSysName, Configure.AkkaConfig(Configure.AkaSysPort));
            //// 註:需安裝Akka DI Extension
            //actorSystem.UseServiceProvider(Configure.provider);
            //actorSystem.ActorOf(actorSystem.DI().Props<MMSMgr>(), nameof(MMSMgr));

        }
    }
}
