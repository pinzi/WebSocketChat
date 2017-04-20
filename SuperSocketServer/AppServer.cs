﻿using BLL;
using CommLib;
using Hangfire;
using SuperSocket.SocketBase;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
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
                        ws.SessionClosed += DisConnected;//注册事件：与客户端断开连接
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
        /// 与客户端建立连接
        /// </summary>
        /// <param name="appSession">客户端对象</param>
        void Connected(WebSocketSession appSession)
        {
            ShowMsg(appSession, string.Format("  当前在线人数:{0}", ws.SessionCount), DataDic.MsgType.Connected);
            StringBuilder MsgText = new StringBuilder();
            MsgText.AppendFormat("用户[{0}]上线了，当前在线人数:{1}", appSession.SessionID, ws.SessionCount);
            SendSysMsgToClient(appSession, MsgText.ToString());
            OnLineUserBLL bll = new OnLineUserBLL();
            BackgroundJob.Enqueue(() => bll.UserOnOffLine((long)1, appSession.SessionID, DataDic.UserAction.OnLine));
        }

        /// <summary>
        /// 接收客户端消息
        /// </summary>
        /// <param name="appSession">客户端对象</param>
        /// <param name="MsgText">客户端消息</param>
        void Received(WebSocketSession appSession, string MsgText)
        {
            ShowMsg(appSession, MsgText, DataDic.MsgType.Received);
            SendSysMsgToClient(appSession, string.Format("用户[{0}]说:{1}", appSession.SessionID, MsgText));
        }

        /// <summary>
        /// 与客户端断开连接
        /// </summary>
        /// <param name="appSession"></param>
        void DisConnected(WebSocketSession appSession, CloseReason Reason)
        {
            string ReasonText = string.Empty;
            switch (Reason)
            {
                case CloseReason.Unknown:
                    ReasonText = "未知原因";
                    break;
                case CloseReason.ServerShutdown:
                    ReasonText = "服务器关机";
                    break;
                case CloseReason.ClientClosing:
                    ReasonText = "客户端关闭";
                    break;
                case CloseReason.ServerClosing:
                    ReasonText = "服务器关闭";
                    break;
                case CloseReason.ApplicationError:
                    ReasonText = "程序错误";
                    break;
                case CloseReason.SocketError:
                    ReasonText = "Socket故障";
                    break;
                case CloseReason.TimeOut:
                    ReasonText = "连接超时";
                    break;
                case CloseReason.ProtocolError:
                    ReasonText = "协议故障";
                    break;
                case CloseReason.InternalError:
                    ReasonText = "网络故障";
                    break;
                default:
                    break;
            }
            ShowMsg(appSession, string.Format("  下线原因：{0}  当前在线人数:{1}", ReasonText, ws.SessionCount), DataDic.MsgType.DisConnected);
            StringBuilder MsgText = new StringBuilder();
            MsgText.AppendFormat("用户[{0}]下线了，当前在线人数:{1}", appSession.SessionID, ws.SessionCount);
            SendSysMsgToClient(appSession, MsgText.ToString());
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
                    Msg.AppendFormat("用户[{0}]上线了{1}", appSession.SessionID, MsgText);
                    break;
                case DataDic.MsgType.Received:
                    Msg.AppendFormat("用户[{0}]：{1}", appSession.SessionID, MsgText);
                    break;
                case DataDic.MsgType.Error:
                    Msg.AppendFormat("系统异常：{0}", MsgText);
                    break;
                case DataDic.MsgType.DisConnected:
                    Msg.AppendFormat("用户[{0}]下线了{1}", appSession.SessionID, MsgText);
                    break;
                case DataDic.MsgType.Stop:
                    Msg.AppendFormat(MsgText);
                    break;
            }
            Console.WriteLine(Msg);
        }

        /// <summary>
        /// 向所有在线的客户端发送广播消息
        /// </summary>
        /// <param name="appSession">当前客户端对象</param>
        /// <param name="MsgText">消息内容</param>
        void SendSysMsgToClient(WebSocketSession appSession, string MsgText)
        {
            //获取所有在线的客户端
            IEnumerable<WebSocketSession> wsSessionList = ws.GetAllSessions();
            //排除当前用户
            if (appSession == null)
                wsSessionList.IEnumerableRemove(appSession);
            //遍历所有在线客户端对象
            foreach (var wsSession in wsSessionList)
            {
                //加入后台计划任务序列
                BackgroundJob.Enqueue(() => wsSession.Send(MsgText));
            }
        }
    }
}
