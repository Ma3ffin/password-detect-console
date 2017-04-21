using PasswordDetect.Handler;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class BaseController
    {
        public DetectionContext DetectionContext { get; set; }

        public ErrorHandler ErrorHandler { get; set; }

        public BaseController()
        {
            DetectionContext = new DetectionContext();
            ErrorHandler = new ErrorHandler();
        }

    }
}