using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// Entity基类
    /// </summary>
    public class BaseEntity
    {
        private DateTime _addtime;
        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime
        {
            get
            {
                if (_addtime == DateTime.MinValue)
                {
                    return DateTime.Now;
                }
                return _addtime;
            }
            set { _addtime = value; }
        }
    }
}
