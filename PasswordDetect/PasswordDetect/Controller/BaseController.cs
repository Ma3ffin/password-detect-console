using PasswordDetect.Handler;
using PasswordDetect.Model;
using PasswordDetect.Data;

namespace PasswordDetect.Controller
{
    public class BaseController
    {
        public DataAccess DataAccess { get; set; }

        public ErrorHandler ErrorHandler { get; set; }

        public BaseController()
        {
            DataAccess = new DataAccess();
            ErrorHandler = new ErrorHandler();
        }

    }
}