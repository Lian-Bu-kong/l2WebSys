using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using AkkaBase;
using DataAccess;
using DataAccess.Repository;
using DummyAkkaNetWeb.Actor;
using DummyAkkaNetWeb.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace DummyAkkaNetWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<ICoilRepo, CoilRepo>();
            // 使用EntityFrameworkNpgsql和啟用ToDoContext

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // SignalIR
            services.AddSignalR();
            // Register ActorSystem
            services.AddSingleton<ChatHub>();



            // 注入自定義 HtmlHelper (Html.Action)
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSingleton(p => {
                var ipPoint = new AkkaSysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.LocalSysIp), AkkaConfigure.LocalSysPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(AkkaConfigure.RemoteSysIp), AkkaConfigure.RemoteSysPort)
                };
                return ipPoint;
            });

            services.AddSingleton<ISysAkkaManager>(provider =>
            {
                // Create the ActorSystem and Dependency Resolver                
                var actSystem = ActorSystem.Create(AkkaConfigure.AkaSysName, AkkaPara.Config(AkkaConfigure.AkaSysPort));
                actSystem.UseServiceProvider(provider);
                return new SysAkkaManager(actSystem);
            });
            services.AddScoped<WebComm>();
            // 註冊Server應用場景
            services.AddScoped(provider =>
            {
                var akkaManager = provider.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager.CreateActor<WebComm>());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
            });

            // 啟用Akka
            var serverEngine = serviceProvider.GetService<AkkaServerEngine>();
        }

    }
}
