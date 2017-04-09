using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 群组
    /// </summary>
    public class ChatGroup : BaseEntity
    {
        /// <summary>
        /// 组ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 组名
        /// </summary>
        public string Gname { get; set; }
        [StringLength(200, ErrorMessage = "群介绍文字长度不超过200")]
        public string Motto { get; set; }
        /// <summary>
        /// 群主用户UID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 群友数
        /// </summary>
        public int UserTotals { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public string UpdateTime { get; set; }
    }
}
