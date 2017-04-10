using Model;

namespace CommLib
{
    public class ApiResult
    {
        ResponseModel _model = new ResponseModel();

        #region 构造函数
        public ApiResult()
        {
            _model.ResultCode = (int)DataDic.ResultCode.Success;
            _model.ResultMsg = "成功";
        }
        public ApiResult(ResponseModel Model)
        {
            _model = Model;
        }
        #endregion

        /// <summary>
        /// 设置返回结果
        /// </summary>
        public void SetResult(ResponseModel Model)
        {
            _model = Model;
        }

        /// <summary>
        ///  设置返回成功
        /// </summary>
        /// <param name="ResultMsg"></param>
        public void SetSuccessResult(string ResultMsg)
        {
            _model.ResultMsg = ResultMsg;
        }

        /// <summary>
        ///  设置返回失败
        /// </summary>
        /// <param name="ResultMsg"></param>
        public void SetFailResult(string ResultMsg)
        {
            _model.ResultCode = (int)DataDic.ResultCode.Fail;
            _model.ResultMsg = ResultMsg;
        }
    }
}
