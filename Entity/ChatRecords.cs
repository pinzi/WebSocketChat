using CommLib;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 好友聊天记录
    /// </summary>
    public class ChatRecords : BaseEntity
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        /// <summary>
        /// 接收者用户UID
        /// </summary>
        [Required]
        public Int64 UID { get; set; }
        /// <summary>
        /// 发送者用户UID
        /// </summary>
        [Required]
        public Int64 SendUID { get; set; }
        /// <summary>
        /// 消息文本
        /// </summary>
        [Required]
        public string MsgText { get; set; }
    }
}
