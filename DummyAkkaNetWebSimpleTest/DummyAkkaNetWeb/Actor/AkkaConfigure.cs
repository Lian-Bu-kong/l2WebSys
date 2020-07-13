using Akka.Configuration;

namespace DummyAkkaNetWeb.Actor
{
    public static class AkkaConfigure
    {
        public static readonly string AkaSysName = "WebActorSystem";
        public static readonly string AkaSysPort = "8200";

        // Outer Sys IP (TCP/IP Protocal)-Test Use
        public static readonly string RemoteSysIp = "127.0.0.1";
        public static readonly int RemoteSysPort = 7788;

        // Local Sys IP (TCP/IP Protocal)-Test Use
        public static readonly string LocalSysIp = "127.0.0.1";
        public static readonly int LocalSysPort = 9101;
    }
}
