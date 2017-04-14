using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class RequestUserLoginModel : RequestBaseModel
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        [Required]
        public long UID { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required, StringLength(6, MinimumLength = 20, ErrorMessage = "密码长度至少6位，不超过20位")]
        public string Pwd { get; set; }
    }
}
