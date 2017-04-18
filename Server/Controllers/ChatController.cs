using BLL;
using CommLib;
using Model;
using Model.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Server.Controllers
{
    public class ChatController : BaseApiController
    {
        /// <summary>
        /// 在线用户
        /// </summary>
        private static OnLineUserModel OnLineUserModel = null;

        public async Task<ApiResult> Post(JObject formData)
        {
            ApiResult result = new ApiResult();
            if (formData == null)
            {
                result.SetResult(DataDic.ResultCode.UnLegalRequest);
                return result;
            }
            string jsonstring = formData.ToString();
            if (string.IsNullOrEmpty(jsonstring))
            {
                result.SetResult(DataDic.ResultCode.UnLegalRequest);
                return result;
            }
            //公共参数为空校验
            RequestBaseModel basemodel = new RequestBaseModel();
            basemodel.AccessToken = Request.Headers.GetValues("AccessToken").FirstOrDefault();
            basemodel.Method = Request.Headers.GetValues("Method").FirstOrDefault();
            basemodel.Sign = Request.Headers.GetValues("Sign").FirstOrDefault();
            if (!ModelState.IsValid)
            {
                result.SetResult(DataDic.ResultCode.UnPassVerifyModel);
                return result;
            }
            if (string.IsNullOrEmpty(basemodel.AccessToken))
            {
                result.SetResult(DataDic.ResultCode.UnLegalToken);
                return result;
            }
            if (string.IsNullOrEmpty(basemodel.Method))
            {
                result.SetResult(DataDic.ResultCode.UnLegalMethod);
                return result;
            }
            if (string.IsNullOrEmpty(basemodel.Sign))
            {
                result.SetResult(DataDic.ResultCode.UnLegalSign);
                return result;
            }

            #region 不验证登录
            switch (basemodel.Method)
            {
                case "commit_usersreg"://用户注册
                    {
                        var model = JsonConvert.DeserializeObject<RequestUserRegModel>(jsonstring);
                        result = await Task.Run(() => new UsersBLL().Reg(model));
                        return result;
                    }
                case "commit_userslogin"://用户登录
                    {
                        var model = JsonConvert.DeserializeObject<RequestUserLoginModel>(jsonstring);
                        result = await Task.Run(() => new UsersBLL().Login(model));
                        return result;
                    }
                case "commit_usersloginout"://用户退出
                    {
                        result = await Task.Run(() => new UsersBLL(OnLineUserModel).LoginOut(basemodel.AccessToken));
                        return result;
                    }
                default:
                    {
                        break;
                    }
            }
            #endregion

            #region 校验accesstoken
            OnLineUserModel = new OnLineUserBLL().QueryOnLineUser(basemodel.AccessToken);
            if (OnLineUserModel == null)
            {
                result.SetResult(DataDic.ResultCode.LoginTimeOut);
                return result;
            }
            #endregion

            #region 验证登录
            //switch (basemodel.Method)
            //{
            //    case "commit_usersreg"://用户注册
            //        var model = JsonConvert.DeserializeObject<RequestUserRegModel>(jsonstring);
            //        result = await Task.Run(() => new UsersBLL().Reg(model));
            //        break;
            //    default:
            //        result.SetResult(DataDic.ResultCode.UnLegalMethod);
            //        break;
            //}
            #endregion

            return result;
        }
    }
}
