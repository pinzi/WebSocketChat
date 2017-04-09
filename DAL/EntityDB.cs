using Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace DAL
{
    public class EntityDB : DbContext
    {
        public EntityDB()
            : base("name=DBchat")
        {
            Ret = string.Empty;
            this.Database.Initialize(false);
        }

        /// <summary>
        /// SaveChanges异常信息
        /// </summary>
        public string Ret = string.Empty;

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// 重写数据提交
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            Ret = string.Empty;
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Ret = string.Format("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName, validationError.PropertyName, validationError.ErrorMessage);
                        //LogHelper.Error(Ret);
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// 重写模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除EF映射默认给表名添加“s“或者“es”
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        #region 数据库关系映射
        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<Users> Users { get; set; }
        /// <summary>
        /// 好友组
        /// </summary>
        public virtual DbSet<Group> Group { get; set; }
        /// <summary>
        /// 好友分组
        /// </summary>
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        /// <summary>
        /// 好友聊天记录
        /// </summary>
        public virtual DbSet<ChatRecords> ChatRecords { get; set; }
        /// <summary>
        /// 群组
        /// </summary>
        public virtual DbSet<ChatGroup> ChatGroup { get; set; }
        /// <summary>
        /// 群友
        /// </summary>
        public virtual DbSet<ChatGroupUser> ChatGroupUser { get; set; }
        /// <summary>
        /// 群聊天记录
        /// </summary>
        public virtual DbSet<GroupChatRecords> GroupChatRecords { get; set; }
        /// <summary>
        /// 在线用户
        /// </summary>
        public virtual DbSet<OnLineUser> OnLineUser { get; set; }
        #endregion
    }
}
