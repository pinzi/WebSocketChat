using Entity;

namespace DAL
{
    public class ChatRecordsDAL
    {
        EntityDB db = new EntityDB();

        /// <summary>
        /// 添加聊天记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddRecords(ChatRecords entity)
        {
            db.ChatRecords.Add(entity);
            return db.SaveChanges();
        }
    }
}
