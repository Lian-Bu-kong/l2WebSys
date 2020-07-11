using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class AppDBInitializer
    {
        // 將資料庫初始化
        public static void Init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // 刪除資料庫
                context.Database.EnsureDeleted();

                // 創建料庫
                context.Database.EnsureCreated();

                // 將資料遷移
                context.Database.Migrate();
            }
        }
    }
}
