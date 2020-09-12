using AkkaBase;
using AkkaSys.MMS;
using AkkaSys.PLC;
using AkkaSys.WMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Actor
{
    public class ActorSysService
    {

        private readonly IServiceCollection _service;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ActorSysService(IServiceCollection service,IWebHostEnvironment environment,IConfiguration configuration)
        {
            _service = service;
            _environment = environment;
            _configuration = configuration;
        }

        public void Inject()
        {
            // MMS
            _service.AddScoped<MMSMgr>();
            _service.AddScoped(p => {

                var akkaManager = p.GetService<ISysAkkaManager>();

                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.MMSLocalSysIp), AkkaConfigure.MMSLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.MMSRemoteSysIp), AkkaConfigure.MMSRemoteSysPort)
                };

                return new MMSRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<MMSRcvEdit>();
            _service.AddScoped(p => {
                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.MMSLocalSysIp), AkkaConfigure.MMSLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.MMSRemoteSysIp), AkkaConfigure.MMSRemoteSysPort)
                };
                return new MMSSnd(ipPoint);
            });
            _service.AddScoped<MMSSndEdit>();

            //WMS
            _service.AddScoped<WMSMgr>();
            _service.AddScoped(p => {

                var akkaManager = p.GetService<ISysAkkaManager>();

                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.WMSLocalSysIp), AkkaConfigure.WMSLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.WMSRemoteSysIp), AkkaConfigure.WMSRemoteSysPort)
                };

                return new WMSRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<WMSRcvEdit>();
            _service.AddScoped(p => {
                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.WMSLocalSysIp), AkkaConfigure.WMSLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.WMSRemoteSysIp), AkkaConfigure.WMSRemoteSysPort)
                };
                return new WMSSnd(ipPoint);
            });
            _service.AddScoped<WMSSndEdit>();
            // PLC
            _service.AddScoped<PLCMgr>();
            _service.AddScoped(p => {

                var akkaManager = p.GetService<ISysAkkaManager>();

                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.PLCLocalSysIp), AkkaConfigure.PLCLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.PLCRemoteSysIp), AkkaConfigure.PLCRemoteSysPort)
                };

                return new PLCRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<PLCRcvEdit>();
            _service.AddScoped(p => {
                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.PLCLocalSysIp), AkkaConfigure.PLCLocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.PLCRemoteSysIp), AkkaConfigure.PLCRemoteSysPort)
                };
                return new PLCSnd(ipPoint);
            });
            _service.AddScoped<PLCSndEdit>();


            _service.AddScoped<Sharp7>();
            // 註冊Server應用場景
            _service.AddScoped(provider =>
            {
                var akkaManager = provider.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager);
            });
        }

    }
}
