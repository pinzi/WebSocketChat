using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Admin.Startup))]
namespace Admin
{
    /// <summary>
    /// Hangfire的配置
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //指定Hangfire使用内存存储后台任务信息
            GlobalConfiguration.Configuration.UseMemoryStorage();
            //启用HangfireServer这个中间件（它会自动释放）
            app.UseHangfireServer();
            //启用Hangfire的仪表盘（可以看到任务的状态，进度等信息）
            app.UseHangfireDashboard();
        }
    }
}
