using System.Linq;
using DAL;

namespace BLL
{
    public class UsersBLL
    {
        EntityDB db = new EntityDB();

        public string test()
        {
            return db.Database.Connection.Database;
        }
    }
}
