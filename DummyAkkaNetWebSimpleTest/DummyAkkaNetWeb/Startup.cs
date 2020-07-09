using Core;
using DummyAkkaNetWeb.Actor;
using DummyAkkaNetWeb.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            // SignalIR
            services.AddSignalR();

            services.AddSingleton<ISysAkkaManager>(p =>
            {
                // Create the ActorSystem and Dependency Resolver                
                var actSystem = ActorSystem.Create(AkaSysName, AkkaConfig(AkaSysPort));
                actSystem.UseServiceProvider(_provider);
                return new SysAkkaManager(actSystem);
            });


            // Register ActorSystem
            services.AddSingleton<ChatHub>();
            services.AddSingleton(provider =>
            {
                var chatHub = provider.GetService<ChatHub>();
                var actManager = new ActorManager(AkkaConfigure.AkaSysName, AkkaConfigure.AkkaConfig(AkkaConfigure.AkaSysPort), chatHub);
                return actManager;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
