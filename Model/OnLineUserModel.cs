using System;
namespace Model
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnLineUserModel
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        public long UID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Int16 Sex { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Motto { get; set; }
        /// <summary>
        /// 上次活跃时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
