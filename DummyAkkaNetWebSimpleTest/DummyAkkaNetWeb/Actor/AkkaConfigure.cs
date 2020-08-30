using Akka.Configuration;

namespace DummyAkkaNetWeb.Actor
{
    public static class AkkaConfigure
    {
        public static readonly string AkaSysName = "ActorSystem";
        public static readonly string AkaSysPort = "8200";


        // MMS
        public static readonly string MMSRemoteSysIp = "127.0.0.1";
        public static readonly int MMSRemoteSysPort = 7791;
        public static readonly string MMSLocalSysIp = "127.0.0.1";
        public static readonly int MMSLocalSysPort = 9101;

        // WMS
        public static readonly string WMSRemoteSysIp = "127.0.0.1";
        public static readonly int WMSRemoteSysPort = 7789;
        public static readonly string WMSLocalSysIp = "127.0.0.1";
        public static readonly int WMSLocalSysPort = 9102;

        // PLC
        public static readonly string PLCRemoteSysIp = "127.0.0.1";
        public static readonly int PLCRemoteSysPort = 7788;
        public static readonly string PLCLocalSysIp = "127.0.0.1";
        public static readonly int PLCLocalSysPort = 9105;

    }
}
