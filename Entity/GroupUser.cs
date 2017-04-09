using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 好友分组
    /// </summary>
    public class GroupUser : BaseEntity
    {
        /// <summary>
        /// 好友所属组ID
        /// </summary>
        [Key, Required]
        [Column(Order = 0)]
        public int GID { get; set; }
        /// <summary>
        /// 用户UID
        /// </summary>
        [Required]
        [Column(Order = 1)]
        public Int64 UID { get; set; }
    }
}
