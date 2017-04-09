using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 好友
    /// </summary>
    public class Friend : BaseEntity
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        [Key, Required]
        [Column(Order = 0)]
        public Int16 UID { get; set; }
        /// <summary>
        /// 好友UID
        /// </summary>
        [Key, Required]
        [Column(Order = 1)]
        public Int64 FriendUID { get; set; }
        /// <summary>
        /// 好友所属组ID
        /// </summary>
        [Required]
        public int GroupID { get; set; }
    }
}
