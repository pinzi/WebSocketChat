using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 群聊天记录
    /// </summary>
    public class GroupChatRecords : BaseEntity
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        /// <summary>
        /// 群ID
        /// </summary>
        [Required]
        public Int64 GID { get; set; }
        /// <summary>
        /// 群友用户UID
        /// </summary>
        [Required]
        public Int64 UID { get; set; }
        /// <summary>
        /// 消息文本
        /// </summary>
        [Required]
        public string MsgText { get; set; }
    }
}
