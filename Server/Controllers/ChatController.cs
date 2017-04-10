using CommLib;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Server.Controllers
{
    public class ChatController : BaseApiController
    {
        public async Task<ApiResult> Post(JObject formData)
        {
            ResponseModel _model = new ResponseModel();
            ApiResult result = new ApiResult();
            if (formData == null)
            {
                _model.ResultCode = (int)DataDic.ResultCode.UnLegalRequest;
                result.SetResult(_model);
                return result;
            }



            return result;
        }
    }
}
