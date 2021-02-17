using AkkaBase;
using AkkaSys.MMS;
using AkkaSys.PLC;
using AkkaSys.Sharp7;
using AkkaSys.WMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace DummyAkkaNetWeb.Actor
{

    public class AkkaSysDIService
    {

        private readonly IServiceCollection _service;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public AkkaSysDIService(IServiceCollection service,IWebHostEnvironment environment,IConfiguration configuration)
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
                var ipPoint = NewIPPoint("MMS");
                return new MMSRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<MMSRcvEdit>();
            _service.AddScoped(p => {

                var ipPoint = NewIPPoint("MMS");
                return new MMSSnd(ipPoint);
            });
            _service.AddScoped<MMSSndEdit>();
            
            //WMS
            _service.AddScoped<WMSMgr>();
            _service.AddScoped(p => {

                var akkaManager = p.GetService<ISysAkkaManager>();
                var ipPoint = NewIPPoint("WMS");
                return new WMSRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<WMSRcvEdit>();
            _service.AddScoped(p => {
                var ipPoint = NewIPPoint("WMS");              
                return new WMSSnd(ipPoint);
            });
            _service.AddScoped<WMSSndEdit>();

            // PLC
            _service.AddScoped<PlcMgr>();
            _service.AddScoped(p => {
                var akkaManager = p.GetService<ISysAkkaManager>();
                var ipPoint = NewIPPoint("PLC");            
                return new PLCRcv(akkaManager, ipPoint);
            });
            _service.AddScoped<PLCRcvEdit>();
            _service.AddScoped(p => {
                var ipPoint = NewIPPoint("PLC");             
                return new PLCSnd(ipPoint);
            });
            _service.AddScoped<PLCSndEdit>();

            //Sharp7 Service
            _service.AddScoped<Sharp7Mgr>();
            _service.AddScoped<Sharp7Service>();

            // 註冊Server應用場景
            _service.AddScoped(provider =>
            {
                var akkaManager = provider.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager);
            });
        }

        private AkkaSysIP NewIPPoint(string sysName)
        {
            var localIp = _configuration[$"AkkaConfigure:{sysName}:LocalIp"];
            var localPort = _configuration[$"AkkaConfigure:{sysName}:LocalPort"];
            var remoteIp = _configuration[$"AkkaConfigure:{sysName}:RemoteIp"];
            var remotePort = _configuration[$"AkkaConfigure:{sysName}:RemotePort"];

            var ipPoint = new AkkaSysIP
            {
                LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(localIp), Int32.Parse(localPort)),
                RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), Int32.Parse(remotePort))
            };

            return ipPoint;
        }
    }
}
