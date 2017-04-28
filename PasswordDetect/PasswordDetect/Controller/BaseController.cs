using PasswordDetect.Handler;
using PasswordDetect.Model;
using PasswordDetect.Data;

namespace PasswordDetect.Controller
{
    public class BaseController
    {
        public DataAccess DataAccess { get; set; }

        public IErrorHandler ErrorHandler { get; set; }

        public BaseController(DataAccess db, IErrorHandler errorHandler)
        {
            ErrorHandler = errorHandler;
            DataAccess = db;
        }

    }
}