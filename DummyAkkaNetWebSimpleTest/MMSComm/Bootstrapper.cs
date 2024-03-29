﻿using Microsoft.Extensions.DependencyInjection;

namespace MMSComm
{
    /// <summary>
    /// 程式導引使用(DI注入寫在這就好，讓Program class乾淨一點)
    /// </summary>
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
