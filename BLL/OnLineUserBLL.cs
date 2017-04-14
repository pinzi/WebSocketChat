using CommLib;
using DAL;
using Model;
using System;

namespace BLL
{
    public class OnLineUserBLL
    {
        OnLineUserDAL dal = new OnLineUserDAL();

        /// <summary>
        /// 检查用户身份令牌合法性
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public ApiResult CheckAccessToken(string accessToken)
        {
            ApiResult result = new ApiResult();
            int Ret = dal.CheckAccessToken(accessToken);
            switch (Ret)
            {
                case -3:
                    result.ResultMsg = "令牌过期";
                    result.ResultCode = (int)DataDic.ResultCode.UnLegalToken;
                    break;
                case -2:
                    result.ResultMsg = "令牌不存在";
                    result.ResultCode = (int)DataDic.ResultCode.NotFoundData;
                    break;
                case -1:
                    result.ResultMsg = "数据库操作出错";
                    result.ResultCode = (int)DataDic.ResultCode.ErrorDB;
                    break;
                default:
                    result.ResultMsg = "成功";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="AccessToken">身份令牌</param>
        /// <returns></returns>
        public OnLineUserModel QueryOnLineUser(string AccessToken)
        {
            return dal.QueryOnLineUser(AccessToken);
        }
    }
}
