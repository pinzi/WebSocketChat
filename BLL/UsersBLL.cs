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
            return result;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        public ApiResult LoginOut()
        {
            throw new NotImplementedException();
        }
    }
}
