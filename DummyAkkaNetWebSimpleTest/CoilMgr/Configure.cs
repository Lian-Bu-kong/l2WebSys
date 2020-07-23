using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using AkkaBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoilMgr
{
    class Configure
    {
        // Akka Sys

        // Local Main Akka Sys
        public static readonly string AkaSysName = "CoilAkkaSys";
        public static readonly string AkaSysPort = "8300";

        private static ServiceProvider _provider;

        public static ServiceProvider GetProvider()
        {
            if (_provider is null) _provider = RegisterSetting();
            return _provider;
        }

        private static ServiceProvider RegisterSetting()
        {
            /**
             注入使用方式參考 https://github.com/iron9light/Akka.Extensions/blob/f94697f7ef181b1b89ee1e72042f269e018b200c/README.md
             */
            // Create and build your container
            var collection = new ServiceCollection();
            // SysActor Manager注入使用方式
            collection.AddSingleton<ISysAkkaManager>(p =>
            {
                // Create the ActorSystem and Dependency Resolver                
                var actSystem = ActorSystem.Create(AkaSysName, AkkaPara.Config(AkaSysPort));
                actSystem.UseServiceProvider(_provider);
                return new SysAkkaManager(actSystem);
            });
           
            /**
                註冊Actor，但是不要使用_provider.GetService<MMSMgr>();               
                使用下方步驟
                var akkaManager = _provider.GetService<ISysAkkaManager>();
                akkaManager.CreateActor<MMSMgr>();
            **/
            collection.AddScoped<CoilMgr>();
            // 註冊Server應用場景
            collection.AddScoped(p =>
            {
                var akkaManager = p.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager.CreateActor<CoilMgr>());
            });

            return collection.BuildServiceProvider();
        }
    }
}
