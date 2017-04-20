using Hangfire;
using Hangfire.MemoryStorage;
using Owin;
using System;

namespace SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化服务器
            AppServer server = new AppServer("127.0.0.1", 2012);
            server.Start();
            //启用HangFire后台任务调度
            GlobalConfiguration.Configuration.UseMemoryStorage();
            ////启用HangfireServer这个中间件（它会自动释放）
            //app.UseHangfireServer();
            ////启用Hangfire的仪表盘（可以看到任务的状态，进度等信息）
            //app.UseHangfireDashboard();
            //GlobalConfiguration.Configuration.UseColouredConsoleLogProvider().UseSqlServerStorage("Server=.;User ID=sa;Password=sa123;database=HangFireDB;Connection Reset=False;");
            Console.ReadKey();
        }
    }
}
