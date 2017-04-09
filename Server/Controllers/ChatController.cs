using BLL;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class ChatController : Controller
    {
        public string Index()
        {
            return new UsersBLL().test();
        }



        // GET: Chat
        //public ActionResult Index()
        //{
        //    WebSocketServer wsServer = new WebSocketServer();



        //    if (!wsServer.Setup("127.0.0.1", 3330))
        //    {
        //        //设置IP 与 端口失败  通常是IP 和端口范围不对引起的 IPV4 IPV6
        //        //MessageBox.Show("设置IP 与 端口失败  通常是IP 和端口范围不对引起的 IPV4 IPV6");
        //        //return;
        //    }
        //    if (!wsServer.Start())
        //    {
        //        //开启服务失败 基本上是端口被占用或者被 某杀毒软件拦截造成的
        //        //MessageBox.Show("开启服务失败 基本上是端口被占用或者被 某杀毒软件拦截造成的");
        //        //return;
        //    }
        //    //有新的连接
        //    wsServer.NewSessionConnected += (session) =>
        //    {


        //    };
        //    //有断开的连接
        //    wsServer.SessionClosed += (session, reason) =>
        //    {

        //    };
        //    //接收到新的文本消息
        //    wsServer.NewMessageReceived += (session, message) =>
        //    {

        //    };


        //    return View();
        //}




    }
}