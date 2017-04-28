using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Exceptions
{
    class UserNotTrainedEnoughtException: PasswordDetectException
    {
        public UserNotTrainedEnoughtException()
        {
        }

        public UserNotTrainedEnoughtException(string message)
            : base(message)
        {
        }

        public UserNotTrainedEnoughtException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
