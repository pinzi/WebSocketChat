using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class RequestUserRegModel : RequestBaseModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [Required, StringLength(20, MinimumLength = 5, ErrorMessage = "昵称长度不能小于5，大于20")]
        public string NickName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Required, MinLength(12, ErrorMessage = "年龄不能小于12岁")]
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required, Range(0, 1, ErrorMessage = "性别只能为男或女")]
        public Int16 Sex { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(20, MinimumLength = 8, ErrorMessage = "密码长度不能小于8位，大于20位")]
        public string Pwd { get; set; }
    }
}
