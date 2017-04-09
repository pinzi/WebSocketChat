using CommLib;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// 群友
    /// </summary>
    public class ChatGroupUser : BaseEntity
    {
        /// <summary>
        /// 群ID
        /// </summary>
        [Key, Required]
        [Column(Order = 0)]
        public int GID { get; set; }
        /// <summary>
        /// 群友用户UID
        /// </summary>
        [Key, Required]
        [Column(Order = 1)]
        public Int64 UID { get; set; }
        /// <summary>
        /// 群友身份：0普通群友1群主2管理员
        /// </summary>
        public DataDic.GroupUserRole Role
        {
            get { return (DataDic.GroupUserRole)_Role; }
            set { _Role = (Int16)value; }
        }
        public Int16 _Role { get; set; }
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
