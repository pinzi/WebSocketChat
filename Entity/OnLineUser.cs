using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnLineUser : BaseEntity
    {
        private string _token;
        /// <summary>
        /// 用户身份令牌
        /// </summary>
        [Key, Required]
        public string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_token))
                {
                    return Guid.NewGuid().ToString();
                }
                return _token;
            }
            set
            {
                _token = value;
            }
        }
        /// <summary>
        /// 用户UID
        /// </summary>
        [Required]
        public Int64 UID { get; set; }
        /// <summary>
        /// 最后一次更新的时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
