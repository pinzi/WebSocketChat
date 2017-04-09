using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 好友组
    /// </summary>
    public class Group : BaseEntity
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
        /// <summary>
        /// 分组所属人UID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 好友数
        /// </summary>
        public int FriendTotals { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public string UpdateTime { get; set; }
    }
}
