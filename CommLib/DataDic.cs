using System;
using System.ComponentModel;

namespace CommLib
{
    /// <summary>
    /// 系统公共字典
    /// </summary>
    public class DataDic
    {
        /// <summary>
        /// 结果状态码
        /// </summary>
        public enum ResultCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success = 0,
            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Fail = -1,
            /// <summary>
            /// 程序出错
            /// </summary>
            [Description("程序出错")]
            Error = -2,
            /// <summary>
            /// 非法的数据提交
            /// </summary>
            [Description("非法的数据提交")]
            UnPassVerifyModel = -3,
            /// <summary>
            /// 非法请求
            /// </summary>
            [Description("非法请求")]
            UnLegalRequest = -4,
            /// <summary>
            /// 签名不合法
            /// </summary>
            [Description("签名不合法")]
            UnLegalSign = -5,
            /// <summary>
            /// 无效的token
            /// </summary>
            [Description("无效的token")]
            UnLegalToken = -6,
            /// <summary>
            /// 参数不合法
            /// </summary>
            [Description("参数不合法")]
            UnLegalMethod = -7,
            /// <summary>
            /// 参数不合法
            /// </summary>
            [Description("参数不合法")]
            UnLegalParameter = -8,
            /// <summary>
            /// 登陆超时
            /// </summary>
            [Description("登陆超时")]
            LoginTimeOut = -9,
            /// <summary>
            /// 没有找到对象
            /// </summary>
            [Description("没有找到对象")]
            NotFoundObject = -10,
            /// <summary>
            /// 没有找到数据
            /// </summary>
            [Description("没有找到数据")]
            NotFoundData = -11,
            /// <summary>
            /// 事务提交失败
            /// </summary>
            [Description("事务提交失败")]
            ErrorDB = -97,
            /// <summary>
            /// 事务提交失败
            /// </summary>
            [Description("事务提交失败")]
            ErrorCommitTransaction = -98,
            /// <summary>
            /// 未知的失败原因
            /// </summary>
            [Description("未知的失败原因")]
            UnKnowFailReason = -99,
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 未知
            /// </summary>
            [Description("未知")]
            Other = 0,
            /// <summary>
            /// 男
            /// </summary>
            [Description("男")]
            WoMan = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Description("女")]
            Man = 2
        }

        /// <summary>
        /// 群友身份
        /// </summary>
        public enum GroupUserRole
        {
            /// <summary>
            /// 普通群友
            /// </summary>
            [Description("普通群友")]
            CommonPerson = 0,
            /// <summary>
            /// 群主
            /// </summary>
            [Description("群主")]
            Owner = 1,
            /// <summary>
            /// 管理员
            /// </summary>
            [Description("管理员")]
            Manager = 2
        }

        /// <summary>
        /// 是否处于调试模式
        /// </summary>
        public static bool IsDebug = Convert.ToBoolean(Func.ReadConfig("IsDebug").ToLower());
        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        public static string LogSavePath = Func.ReadConfig("LogSavePath", null);
    }
}
