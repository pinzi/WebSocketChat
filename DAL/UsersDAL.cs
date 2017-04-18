using CommLib;
using Entity;
using Model;
using System.Linq;

namespace DAL
{
    public class UsersDAL
    {
        EntityDB db = new EntityDB();

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Reg(RequestUserRegModel model)
        {
            Users entity = new Users();
            long UID = db.Users.Max(u => u.UID);
            if (UID > 0)
            {
                UID++;
            }
            else
            {
                UID = 1000;
            }
            entity.UID = UID;
            entity.NickName = model.NickName;
            entity.Pwd = Func.MD5Encrypt(model.Pwd, 32, false);
            entity.Age = model.Age;
            entity.Sex = model.Sex;
            db.Users.Add(entity);
            db.SaveChanges();
            return UID;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Login(RequestUserLoginModel model)
        {
            var query = db.Users.Where(u => u.UID == model.UID && u.Pwd == model.Pwd).ToList();
            if (query.Count.Equals(1))
            {
                //身份验证通过，返回accesstoken
                return new OnLineUserDAL().GenerateAccessToken(model.UID);
            }
            return null;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="accessToken">用户身份令牌</param>
        /// <returns></returns>
        public int LoginOut(string AccessToken)
        {
            var query = db.OnLineUser.Find(AccessToken);
            if (query == null)
            {
                return -2;
            }
            db.OnLineUser.Remove(query);
            return db.SaveChanges();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户UID</param>
        /// <returns></returns>
        public Users Query(string UID)
        {
            return db.Users.Find(UID);
        }
    }
}
