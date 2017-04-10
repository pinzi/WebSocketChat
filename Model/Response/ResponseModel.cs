namespace Model
{
    /// <summary>
    /// 返回数据模型
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 状态消息文本
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic ResultData { get; set; }
    }
}
