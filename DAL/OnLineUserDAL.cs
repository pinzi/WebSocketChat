using CommLib;
using Entity;
using Model;
using System;
using System.Linq;

namespace DAL
{
    public class OnLineUserDAL
    {
        EntityDB db = new EntityDB();

        /// <summary>
        /// 生成用户身份令牌
        /// </summary>
        /// <param name="UID">用户UID</param>
        /// <returns></returns>
        public string GenerateAccessToken(long UID)
        {
            var query = db.OnLineUser.Where(o => o.UID == UID).SingleOrDefault();
            if (query == null)
            {
                query = new OnLineUser();
                query.UID = UID;
                query.UpdateTime = DateTime.Now;
                db.OnLineUser.Add(query);
            }
            //不为空，则刷新令牌
            int Days = 0;
            int.TryParse(Func.ReadConfig("AccessTokenDays"), out Days);
            if (query.UpdateTime.AddDays(Days) < DateTime.Now)
                query.AccessToken = Guid.NewGuid().ToString();
            query.UpdateTime = DateTime.Now;
            db.SaveChanges();
            return query.AccessToken;
        }

        /// <summary>
        /// 检查用户身份令牌合法性
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public int CheckAccessToken(string accessToken)
        {
            var query = db.OnLineUser.Find(accessToken);
            if (query == null)
            {
                return -2;//令牌不存在
            }
            int Days = 0;
            int.TryParse(Func.ReadConfig("AccessTokenDays"), out Days);
            if (query.UpdateTime.AddDays(Days) < DateTime.Now)
            {
                return -3;//令牌过期
            }
            return 0;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="AccessToken">身份令牌</param>
        /// <returns></returns>
        public OnLineUserModel QueryOnLineUser(string AccessToken)
        {
            var queryO = db.OnLineUser.Find(AccessToken);
            if (queryO != null)
            {
                var queryU = db.Users.Find(queryO.UID);
                if (queryU != null)
                {
                    OnLineUserModel model = new OnLineUserModel();
                    model.UID = queryU.UID;
                    model.NickName = queryU.NickName;
                    model.Age = queryU.Age;
                    model.Sex = queryU.Sex;
                    model.Motto = queryU.Motto;
                    model.UpdateTime = queryU.UpdateTime;
                    model.AddTime = queryU.AddTime;
                    return model;
                }
            }
            return null;
        }
    }
}
