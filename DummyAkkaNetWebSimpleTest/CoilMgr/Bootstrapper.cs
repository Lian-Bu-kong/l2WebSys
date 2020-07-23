using Microsoft.Extensions.DependencyInjection;

namespace CoilMgr
{
    public class Bootstrapper
    {
        public void Run()
        {
            var serviceProvider = Configure.GetProvider();
            var serverEngine = serviceProvider.GetService<AkkaServerEngine>();
            serverEngine.Start();
        }
    }
}
