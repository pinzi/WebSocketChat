using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 请求数据模型基类
    /// </summary>
    public class RequestBaseModel
    {
        /// <summary>
        /// 用户身份令牌
        /// </summary>
        [Required, StringLength(32, MinimumLength = 32, ErrorMessage = "AccessToken长度必须为32")]
        public string AccessToken { get; set; }
        /// <summary>
        /// API方法名
        /// </summary>
        [Required]
        public string Method { get; set; }
    }
}
