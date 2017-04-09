using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// Entity基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }
    }
}
