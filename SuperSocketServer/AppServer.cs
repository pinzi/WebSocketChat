using CommLib;
using SuperSocket.WebSocket;
using System;
using System.Text;

namespace SuperSocketServer
{
    public class AppServer
    {
        private WebSocketServer ws = null;
        private string _Ip;
        private int _Port;
        public AppServer(string Ip, int Port)
        {
            if (ws == null)
            {
                ws = new WebSocketServer();
                _Ip = Ip;
                _Port = Port;
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        internal void Start()
        {
            StringBuilder Msg = new StringBuilder(string.Format("服务器{0}:{1}", _Ip, _Port));
            if (ws != null)
            {
                if (ws.Setup(_Ip, _Port))
                {
                    if (ws.Start())
                    {
                        ShowMsg(null, Msg.Append("已启动").ToString(), DataDic.MsgType.Start);
                        ws.NewSessionConnected += Connected;//注册事件：与客户端建立连接
                        ws.NewMessageReceived += Received;//注册事件：接收客户端消息
                        return;
                    }
                }
            }
            ShowMsg(null, Msg.Append("启动失败").ToString(), DataDic.MsgType.Start);
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        internal void Stop()
        {
            if (ws != null)
                ws.Stop();
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="appSession">客户端对象</param>
        void Connected(WebSocketSession appSession)
        {
            ShowMsg(appSession, null, DataDic.MsgType.Connected);
        }

        /// <summary>
        /// 接收客户端消息
        /// </summary>
        /// <param name="appSession">客户端对象</param>
        /// <param name="MsgText">客户端消息</param>
        void Received(WebSocketSession appSession, string MsgText)
        {
            ShowMsg(appSession, MsgText, DataDic.MsgType.Received);
        }



        /// <summary>
        ///  向控制台输出消息
        /// </summary>
        /// <param name="appSession">客户端对象</param>
        /// <param name="MsgText">消息内容</param>
        /// <param name="Mt">消息类别</param>
        void ShowMsg(WebSocketSession appSession, string MsgText, DataDic.MsgType Mt)
        {
            StringBuilder Msg = new StringBuilder(string.Format("{0}  ", DateTime.Now.ToString("HH:mm:ss")));
            switch (Mt)
            {
                case DataDic.MsgType.Start:
                    Msg.AppendFormat(MsgText);
                    break;
                case DataDic.MsgType.Connected:
                    Msg.AppendFormat("用户[{0}]上线了", appSession.SessionID);
                    break;
                case DataDic.MsgType.Received:
                    Msg.AppendFormat("用户[{0}]：{1}", appSession.SessionID, MsgText);
                    break;
                case DataDic.MsgType.Error:
                    Msg.AppendFormat("系统异常：{0}", MsgText);
                    break;
                case DataDic.MsgType.Closed:
                    Msg.AppendFormat("用户[{0}]下线了", appSession.SessionID);
                    break;
                case DataDic.MsgType.Stop:
                    Msg.AppendFormat(MsgText);
                    break;
            }
            Console.WriteLine(Msg);
        }

        void Send(string MsgText)
        {
        }
    }
}
