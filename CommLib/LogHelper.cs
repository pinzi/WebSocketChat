using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace CommLib
{
    public class LogHelper
    {
        /// <summary>
        /// 日志保存路径
        /// </summary>
        private static string LogPath = string.Empty;
        public LogHelper()
        {
            LogPath = DataDic.LogSavePath;
        }
        public LogHelper(string _logPath)
        {
            if (string.IsNullOrEmpty(_logPath))
            {
                LogPath = DataDic.LogSavePath;
            }
            else
            {
                LogPath = _logPath;
            }
        }

        /// <summary>
        /// 日志信息类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 自定义信息
            /// </summary>
            [Description("自定义信息")]
            Info = 0,
            /// <summary>
            /// 失败信息
            /// </summary>
            [Description("失败信息")]
            Fail = 1,
            /// <summary>
            /// 出错信息
            /// </summary>
            [Description("出错信息")]
            Error = 2,
            /// <summary>
            /// 监控信息
            /// </summary>
            [Description("监控信息")]
            Monitor = 3
        }

        /// <summary>
        /// 自定义文本消息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            WriteLog(LogType.Info, message);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message"></param>
        public static void Fail(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            WriteLog(LogType.Fail, message);
        }

        /// <summary>
        /// 出错信息
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            WriteLog(LogType.Error, message);
        }

        /// <summary>
        /// 向文本中写入内容
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        private static void WriteLog(LogType logType, string message)
        {
            try
            {
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
                var fileName = LogPath + string.Format("{0}_{1}.log", logType.ToString(), DateTime.Now.ToString("yyyyMMdd"));
                message = DateTime.Now.ToString("HH:mm:ss") + "==>>" + message + "\r\n";
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Write, 1024, FileOptions.Asynchronous))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    IAsyncResult writeResult = fs.BeginWrite(buffer, 0, buffer.Length, (asyncResult) =>
                    {
                        var fStream = (FileStream)asyncResult.AsyncState;
                        fStream.EndWrite(asyncResult);
                    }, fs);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch
            { }
        }
    }
}