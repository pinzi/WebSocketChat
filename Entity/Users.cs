using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Users : BaseEntity
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        [Key, Required]
        public Int64 UID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required, StringLength(50, MinimumLength = 1, ErrorMessage = "昵称长度不能小于1位，不能超过50位")]
        public string NickName { get; set; }
        /// <summary>
        /// 性别:0女1男，默认为0
        /// </summary>
        [Required, Range(0, 1)]
        public Int16 Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Motto { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(32, MinimumLength = 32, ErrorMessage = "密码密文长度必须为32位")]
        public string Pwd { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
