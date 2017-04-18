using CommLib;

namespace Model
{
    /// <summary>
    /// 返回数据模型
    /// </summary>
    public class ApiResult
    {
        #region 构造函数
        public ApiResult()
        {
            ResultCode = (int)DataDic.ResultCode.Success;
            ResultMsg = "成功";
        }
        public ApiResult(DataDic.ResultCode _ResultCode)
        {
            ResultCode = (int)_ResultCode;
        }
        public ApiResult(DataDic.ResultCode _ResultCode, string _ResultMsg)
        {
            ResultCode = (int)_ResultCode;
            ResultMsg = _ResultMsg;
        }
        #endregion

        #region 属性成员
        /// <summary>
        /// 状态码
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 状态消息文本
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int ResultCount { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic ResultData { get; set; }
        #endregion

        /// <summary>
        /// 设置返回结果
        /// </summary>
        public void SetResult(DataDic.ResultCode _ResultCode, string _ResultMsg, dynamic _ResultData = null, int _ResultCount = 0)
        {
            ResultCode = (int)_ResultCode;
            ResultMsg = string.IsNullOrEmpty(_ResultMsg) ? Func.GetEnumDescription(_ResultCode) : _ResultMsg;
            ResultData = _ResultData;
            ResultCount = _ResultCount;
        }

        /// <summary>
        /// 设置返回结果
        /// </summary>
        public void SetResult(DataDic.ResultCode _ResultCode)
        {
            ResultCode = (int)_ResultCode;
            ResultMsg = Func.GetEnumDescription(_ResultCode);
            ResultCount = 0;
        }

        /// <summary>
        ///  设置返回成功
        /// </summary>
        /// <param name="ResultMsg"></param>
        public void SetSuccessResult(string _ResultMsg = null, dynamic _ResultData = null, int _ResultCount = 0)
        {
            if (string.IsNullOrEmpty(_ResultMsg))
            {
                _ResultMsg = "成功";
            }
            ResultMsg = _ResultMsg;
            ResultData = _ResultData;
            ResultCount = _ResultCount;
        }

        /// <summary>
        ///  设置返回失败
        /// </summary>
        /// <param name="ResultMsg"></param>
        public void SetFailResult(string _ResultMsg = null)
        {
            if (string.IsNullOrEmpty(_ResultMsg))
            {
                _ResultMsg = "失败";
            }
            ResultCode = (int)DataDic.ResultCode.Fail;
            ResultMsg = _ResultMsg;
            ResultCount = 0;
        }
    }
}
