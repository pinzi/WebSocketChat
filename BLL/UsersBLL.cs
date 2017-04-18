using CommLib;
using DAL;
using Model;
using System;

namespace BLL
{
    public class UsersBLL : BaseBLL
    {
        public UsersBLL()
        { }
        public UsersBLL(OnLineUserModel model) : base(model)
        {
        }

        UsersDAL dal = new UsersDAL();

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ApiResult Reg(RequestUserRegModel model)
        {
            ApiResult result = new ApiResult();
            long UID = dal.Reg(model);
            if (UID < 1)
            {
                result.SetFailResult();
            }
            //注册成功，返回token
            string AccessToken = new OnLineUserDAL().GenerateAccessToken(UID);
            result.ResultData = new string[] { AccessToken };
            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult Login(RequestUserLoginModel model)
        {
            ApiResult result = new ApiResult();
            model.Pwd = Func.MD5Encrypt(model.Pwd, 32, false);
            string AccessToken = new UsersDAL().Login(model);
            if (string.IsNullOrEmpty(AccessToken))
            {
                //登录失败
                result.SetFailResult("用户名密码不匹配");
            }
            else if (AccessToken.Length == 32)
            {
                //登录成功
                result.ResultData = new string[] { AccessToken };
                result.SetSuccessResult("登录成功");
            }
            else
            {
                //accesstoken异常
                result.SetResult(DataDic.ResultCode.UnLegalToken, "登录失败，accesstoken异常");
            }
            return result;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        public ApiResult LoginOut(string AccessToken)
        {
            ApiResult result = new ApiResult();
            int Ret = dal.LoginOut(AccessToken);
            switch (Ret)
            {
                case -2:
                    result.SetResult(DataDic.ResultCode.UnLegalToken, "用户身份令牌无效");
                    break;
                case -1:
                    result.SetResult(DataDic.ResultCode.ErrorDB);
                    break;
                case 0:
                    result.SetResult(DataDic.ResultCode.NonBeAffected);
                    break;
                default:
                    result.SetSuccessResult("操作成功");
                    break;
            }
            return result;
        }
    }
}
