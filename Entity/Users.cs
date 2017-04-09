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
        [Required, StringLength(200, MinimumLength = 1, ErrorMessage = "昵称长度不能小于1，不能超过200")]
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
        /// 上次更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
