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
            Fail = 1,
            /// <summary>
            /// 程序出错
            /// </summary>
            [Description("程序出错")]
            Error = 2,
            /// <summary>
            /// 登陆超时
            /// </summary>
            [Description("登陆超时")]
            LoginTimeOut = -1,
            /// <summary>
            /// 账号已禁用，请联系管理员
            /// </summary>
            [Description("账号已禁用，请联系管理员")]
            DisableAccount = -2,
            /// <summary>
            /// 签名不合法
            /// </summary>
            [Description("签名不合法")]
            UnLegalSign = -3,
            /// <summary>
            /// 账号异地登陆
            /// </summary>
            [Description("账号异地登陆")]
            UnLegalLogin = -4,
            /// <summary>
            /// 没有找到对象
            /// </summary>
            [Description("没有找到对象")]
            NotFoundObject = -5,
            /// <summary>
            /// 参数不合法
            /// </summary>
            [Description("参数不合法")]
            UnLegalParameter = -6,
            /// <summary>
            /// 缺少参数sign
            /// </summary>
            [Description("缺少参数sign")]
            NotFoundSign = -7,
            /// <summary>
            /// 缺少参数version
            /// </summary>
            [Description("缺少参数version")]
            NotFoundParamVersion = -8,
            /// <summary>
            /// 缺少参数token
            /// </summary>
            [Description("缺少参数token")]
            NotFoundParamToken = -9,
            /// <summary>
            /// 手机号码不正确
            /// </summary>
            [Description("手机号码不正确")]
            UnLegalMobile = -10,
            /// <summary>
            /// 没有找到接口地址
            /// </summary>
            [Description("没有找到接口地址")]
            NotFoundPostUrl = -11,
            /// <summary>
            /// 请求接口数据出错
            /// </summary>
            [Description("请求接口数据出错")]
            ErrorPostWebReq = -12,
            /// <summary>
            /// 非法的验证码
            /// </summary>
            [Description("非法的验证码")]
            UnLegalMobileCode = -13,
            /// <summary>
            /// 非法的数据提交
            /// </summary>
            [Description("非法的数据提交")]
            UnPassVerifyModel = -14,
            /// <summary>
            /// 该手机号码已注册
            /// </summary>
            [Description("该手机号码已注册")]
            HasExistUser = -15,
            /// <summary>
            /// 没有找到该用户
            /// </summary>
            [Description("没有找到该用户")]
            NotFoundUser = -16,
            /// <summary>
            /// 主库用户注册失败
            /// </summary>
            [Description("主库用户注册失败")]
            ErrorPostUserReg = -17,
            /// <summary>
            /// 社区库用户注册失败
            /// </summary>
            [Description("社区库用户注册失败")]
            FailBbsReg = -18,
            /// <summary>
            /// 用户名或密码不匹配
            /// </summary>
            [Description("用户名或密码不匹配")]
            UnLegalAccount = -19,
            /// <summary>
            /// 非法的设备
            /// </summary>
            [Description("非法的设备")]
            UnLegalDevice = -20,
            /// <summary>
            /// 缺少参数devicetype
            /// </summary>
            [Description("缺少参数devicetype")]
            NotFoundParamDeviceType = -21,
            /// <summary>
            /// 事务提交失败
            /// </summary>
            [Description("事务提交失败")]
            ErrorCommitTransaction = -22,
            /// <summary>
            /// 该主题不存在或已被删除
            /// </summary>
            [Description("该主题不存在或已被删除")]
            NotFoundTopic = -23,
            /// <summary>
            /// 您没有回复权限
            /// </summary>
            [Description("您没有回复权限")]
            NotHasReplyLegal = -24,
            /// <summary>
            /// 该回复不存在或已被删除
            /// </summary>
            [Description("该回复不存在或已被删除")]
            NotFoundTopicReply = -25,
            /// <summary>
            /// 没有找到文件
            /// </summary>
            [Description("没有找到文件")]
            NotFoundFile = -26,
            /// <summary>
            /// 文件读取出错
            /// </summary>
            [Description("文件读取出错")]
            ErrorReadFile = -27,
            /// <summary>
            /// xml文件属性读取出错
            /// </summary>
            [Description("xml文件属性读取出错")]
            ErrorReadXmlAttr = -28,
            /// <summary>
            /// 您已经关注过该用户
            /// </summary>
            [Description("您已经关注过该用户")]
            HasFollowed = -29,
            /// <summary>
            /// 关注人不存在
            /// </summary>
            [Description("关注人不存在")]
            NotFoundFollowUser = -30,
            /// <summary>
            /// 被关注人不存在
            /// </summary>
            [Description("被关注人不存在")]
            NotFoundFollowedUser = -31,
            /// <summary>
            /// 没有找到数据
            /// </summary>
            [Description("没有找到数据")]
            NotFoundData = -32,
            /// <summary>
            /// 未知的失败原因
            /// </summary>
            [Description("未知的失败原因")]
            UnKnowFailReason = -33,
            /// <summary>
            /// 无效的token
            /// </summary>
            [Description("无效的token")]
            UnLegalToken = -34,
            /// <summary>
            /// 验证码已被使用过
            /// </summary>
            [Description("验证码已被使用过")]
            HasVerified = -35,
            /// <summary>
            /// 验证码已失效
            /// </summary>
            [Description("验证码已失效")]
            MobileCodeHasTimeOut = -36,
            /// <summary>
            /// 主库登陆失败
            /// </summary>
            [Description("主库登陆失败")]
            ErrorMainDBLogin = -37,
            /// <summary>
            /// 用户登陆失败
            /// </summary>
            [Description("用户登陆失败")]
            FailUserLogin = -38,
            /// <summary>
            /// 缺少参数deviceid
            /// </summary>
            [Description("缺少参数deviceid")]
            NotFoundParamDeviceID = -39,
            /// <summary>
            /// 非法请求
            /// </summary>
            [Description("非法请求")]
            UnLegalRequest = -40,
            /// <summary>
            /// 非法调用
            /// </summary>
            [Description("非法调用")]
            Invalid = -41,
            /// <summary>
            /// 请勿重复签到
            /// </summary>
            [Description("请勿重复签到")]
            HasSignInToday = -42,
            /// <summary>
            /// 不能关注自己
            /// </summary>
            [Description("不能关注自己")]
            UnAllowFollowSelf = -43,
            /// <summary>
            /// 云牙公共接口请求失败
            /// </summary>
            [Description("验证码获取失败")]
            FailGetMobileCode = -44,
            /// <summary>
            /// 云牙公共接口返回数据异常
            /// </summary>
            [Description("主库接口返回数据异常")]
            ErrorResponseApiCom = -45,
            /// <summary>
            ///  主库注册返回的用户信息异常
            /// </summary>
            [Description("主库注册返回的用户信息异常")]
            ErrorResponseRegUserInfo = -46,
            /// <summary>
            /// 密码修改失败
            /// </summary>
            [Description("密码修改失败")]
            FailChangePwd = -47,
            /// <summary>
            /// 主库更新失败
            /// </summary>
            [Description("主库更新失败")]
            FailUpdateMainDB = -48,
            /// <summary>
            /// 主库退出失败
            /// </summary>
            [Description("主库退出失败")]
            FailLoginOutMainDB = -49,
            /// <summary>
            /// 退出失败
            /// </summary>
            [Description("退出失败")]
            FailUserLoginOut = -50,
            /// <summary>
            /// 没有社区直播权限
            /// </summary>
            [Description("没有社区直播权限")]
            NotHasOnLiveLegal = -51,
            /// <summary>
            /// 没有找到版块信息
            /// </summary>
            [Description("没有找到版块信息")]
            NotFoundArea = -52,
            /// <summary>
            /// 请勿重复提交
            /// </summary>
            [Description("请勿重复提交")]
            SubmitRepeat = -53
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
