using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
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

            // Create and build your container
            //var builder = new ServiceCollection();
            //builder.AddTransient<MMSMgr>();
            //builder.AddTransient<TestDI>();
            //var serviceProvider = builder.BuildServiceProvider();


            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
             
            System.Console.ReadLine();


            //Configure.Provider.GetService<MMSMgr>();

            //// Create method takes a name for the new actor system (System Actor Manager Master) - 需下載 Akka Remote
            //var actorSystem = ActorSystem.Create(Configure.AkaSysName, Configure.AkkaConfig(Configure.AkaSysPort));
            //// 註:需安裝Akka DI Extension
            //actorSystem.UseServiceProvider(Configure.provider);
            //actorSystem.ActorOf(actorSystem.DI().Props<MMSMgr>(), nameof(MMSMgr));

        }
    }
}
