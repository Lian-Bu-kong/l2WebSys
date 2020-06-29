using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Actor
{
    public static class AkkaConfigure
    {
        public static readonly string AkaSysName = "IcscAkkaSystem";
        public static readonly string AkaSysPort = "8202";

        // Outer Sys IP (TCP/IP Protocal)
        public static readonly string OutSysIp = "127.0.0.1";
        public static readonly int OutSysPort = 7788;

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
