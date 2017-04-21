using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnLineUser : BaseEntity
    {
        /// <summary>
        /// 用户身份令牌
        /// </summary>
        [Key, Required]
        public string AccessToken { get; set; }
        /// <summary>
        /// 用户UID
        /// </summary>
        [Required]
        public Int64 UID { get; set; }
        /// <summary>
        /// 会话ID
        /// </summary>
        [Required]
        public string SessionID { get; set; }
        /// <summary>
        /// 最后一次更新的时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
