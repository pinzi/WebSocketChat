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

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="UID">用户UID</param>
        /// <param name="SessionID">用户会话ID</param>
        /// <returns></returns>
        public int Add(long UID, string Pwd, string SessionID)
        {
            var queryU = db.Users.Find(UID);
            if (queryU == null)
            {
                return -2;//用户uid不存在
            }
            if (!queryU.Pwd.Equals(Func.MD5Encrypt(Pwd, 32, false)))
            {
                return -3;//密码错误
            }
            var query = db.OnLineUser.Where(o => o.UID == UID).SingleOrDefault();
            if (query != null)
            {
                //更新用户会话ID和活跃时间
                query.SessionID = SessionID;
                query.UpdateTime = DateTime.Now;
            }
            else
            {
                //新的用户登录
                db.OnLineUser.Add(new OnLineUser()
                {
                    AccessToken = Guid.NewGuid().ToString(),
                    UID = UID,
                    SessionID = SessionID,
                    UpdateTime = DateTime.Now,
                    AddTime = DateTime.Now
                });
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="UID">用户UID</param>
        /// <returns></returns>
        public int Remove(long UID)
        {
            var query = db.OnLineUser.Find(UID);
            if (query == null)
            {
                return -2;//用户在线记录不存在
            }
            db.OnLineUser.Remove(query);
            return db.SaveChanges();
        }

    }
}
