using Akka.Actor;
using Akka.Configuration;
using Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MMSComm
{
    class Configure
    {
        #region 專案整個系統的Akka Manager 建議寫成單例
        //public static ActorSystem actorSystem;

        // Local Main Sys
        public static readonly string AkaSysName = "MMSAkkaSys";
        public static readonly string AkaSysPort = "8201";

        // Outer Sys IP (TCP/IP Protocal)
        public static readonly string OutSysIp = "127.0.0.1";
        public static readonly int OutSysPort = 7791;

        // Local Sys IP (TCP/IP Protocal)
        public static readonly string LocalSysIp = "127.0.0.1";
        public static readonly int LocalSysPort = 9101;
        #endregion

        // DI
        public static ServiceProvider Provider;
        public static void DIManagerSetting()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton(provider =>
            {
                ISysAkkaManager actManager = new SysAkkaManager(AkaSysName, AkkaConfig(AkaSysPort), new Dictionary<string, IActorRef>());
                return actManager;
            });
            collection.AddScoped(provider =>
            {
                var actorManager = provider.GetService<ISysAkkaManager>();
                var mmsMgr = new MMSMgr(actorManager);
                //var mmsMgr = new MMSMgr();
                return mmsMgr;
            });
            //collection.AddScoped(provider =>
            //{
            //    var actorManager = provider.GetService<SysAkkaManager>();
            //    var mmsRcv = new MMSRcv(actorManager);
            //    return mmsRcv;
            //});
            //collection.AddScoped(provider =>
            //{
            //    var actorManager = provider.GetService<SysAkkaManager>();
            //    var mmsRcvEdit = new MMSRcvEdit(actorManager);
            //    return mmsRcvEdit;
            //});

            Provider = collection.BuildServiceProvider();
        }

        /// <summary>
        ///     建立 config
        /// </summary>
        /// <param name="port"> 本地端接口埠號 </param>
        public static Config AkkaConfig(string port)
        {
            var strConfig = @"
                akka
                {
                    #loglevel = DEBUG
                    #loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
                    actor
                    {
                        provider = remote
                        debug
                        {
                            receive = on      # log any received message
                            autoreceive = on  # log automatically received messages, e.g. PoisonPill
                            lifecycle = on    # log actor lifecycle changes
                            event-stream = on # log subscription changes for Akka.NET event stream
                            unhandled = on    # log unhandled messages sent to actors
                        }
                    }
                    remote 
                    {
                        dot-netty.tcp 
                        {
                            port = {port}
                            hostname = 0.0.0.0
                            public-hostname = 127.0.0.1
                        }
                    }
                }";
            strConfig = strConfig.Replace("{port}", port);

            return ConfigurationFactory.ParseString(strConfig);
        }

    }
}
