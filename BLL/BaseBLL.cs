using Model;

namespace BLL
{
    public class BaseBLL
    {
        private static OnLineUserModel _model;
        public BaseBLL()
        { }
        public BaseBLL(OnLineUserModel model)
        {
            _model = model;
        }
    }
}
