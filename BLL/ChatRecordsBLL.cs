using DAL;
using Entity;
using Model;

namespace BLL
{
    public class ChatRecordsBLL : BaseBLL
    {
        public ChatRecordsBLL()
        { }
        public ChatRecordsBLL(OnLineUserModel model) : base(model)
        {
        }

        ChatRecordsDAL dal = new ChatRecordsDAL();

        /// <summary>
        /// 添加聊天记录
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="SendUID"></param>
        /// <param name="MsgText"></param>
        /// <returns></returns>
        public int AddRecords(string UID, string SendUID, string MsgText)
        {
            if (string.IsNullOrEmpty(UID))
            {
                return -2;//接收人UID不能为空
            }
            if (string.IsNullOrEmpty(SendUID))
            {
                return -3;//发送人UID不能为空
            }
            if (string.IsNullOrEmpty(MsgText))
            {
                return -4;//消息文本不能为空
            }
            UsersDAL uDAL = new UsersDAL();
            if (uDAL.Query(UID) == null)
            {
                return -5;//接收人UID不存在
            }
            if (uDAL.Query(SendUID) == null)
            {
                return -6;//发送人UID不存在
            }
            ChatRecords entity = new ChatRecords();
            return dal.AddRecords(entity);
        }
    }
}
