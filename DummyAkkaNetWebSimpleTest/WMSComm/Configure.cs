﻿using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using AkkaBase;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using WMSComm;
using WMSComm.Actor;

namespace MMSComm
{
    class Configure
    {
        #region 專案整個系統的Akka Manager 建議寫成單例
        //public static ActorSystem actorSystem;

        // Local Main Sys
        public static readonly string AkaSysName = "WMSAkkaSys";
        public static readonly string AkaSysPort = "8202";

        // Other Akka Sys
        public static readonly string WebAkaSysName = "WebActorSystem";
        public static readonly string WebAkaSysPort = "8201";

        // Outer Sys IP (TCP/IP Protocal)
        public static readonly string RemoteSysIp = "127.0.0.1";
        public static readonly int RemoteSysPort = 7792;

        // Local Sys IP (TCP/IP Protocal)
        public static readonly string LocalSysIp = "127.0.0.1";
        public static readonly int LocalSysPort = 9102;
        #endregion
        // DI
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
            collection.AddSingleton(p => {
                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(LocalSysIp), LocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(RemoteSysIp), RemoteSysPort)
                };
                return ipPoint;
            });
            /**
                註冊Actor，但是不要使用_provider.GetService<MMSMgr>();               
                使用下方步驟
                var akkaManager = _provider.GetService<ISysAkkaManager>();
                akkaManager.CreateActor<MMSMgr>();
            **/
            collection.AddScoped<WMSMgr>();
            collection.AddScoped<WMSRcv>();
            collection.AddScoped<WMSRcvEdit>();
            collection.AddScoped<WMSSnd>();
            collection.AddScoped<WMSSndEdit>();
            // 註冊Server應用場景
            collection.AddScoped(p =>
            {
                var akkaManager = p.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager.CreateActor<WMSMgr>());
            });

            return collection.BuildServiceProvider();
        }


    }
}
