using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using AkkaBase;
using AkkaSys.Event;
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
using Sharp7;
using System;

namespace DummyAkkaNetWeb
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
     
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // 使用EntityFrameworkNpgsql和啟用ToDoContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<ICoilRepo, CoilRepo>();
            services.AddScoped<ICoilRepo, CoilDummyRepo>();

            // SignalIR
            services.AddSignalR();
            // Register ActorSystem
            services.AddSingleton<ChatHub>();
            services.AddSingleton<TrackingHub>();

            // Regiseter EventPush
            services.AddSingleton<ITrackingEventPusher, TrackingEventPusher>();

            // Regiseter Snap7
            services.AddSingleton<S7Client>();

            // 注入自定義 HtmlHelper (Html.Action)
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<ISysAkkaManager>(provider =>
            {
                var akkaSys = _configuration["AkkaConfigure:AkaSysName"];
                var akkaPort = _configuration["AkkaConfigure:AkaSysPort"];
  
                var actSystem = ActorSystem.Create(akkaSys, AkkaPara.Config(akkaPort));
                actSystem.UseServiceProvider(provider);
                return new SysAkkaManager(actSystem);
            });


            // Register the Swagger services          
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "L2 API";
                    document.Info.Description = "API Document";
                    //document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "ICSC",
                        Email = "service@icsc.com.tw",
                        Url = "https://www.icsc.com.tw/"
                    };                   
                };
            });

            new AkkaSysDIService(services, _environment, _configuration).Inject();

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

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<TrackingHub>("/trackinghub");
            });

            // 啟用Akka
            var serverEngine = serviceProvider.GetService<AkkaServerEngine>();
        }

    }
}
