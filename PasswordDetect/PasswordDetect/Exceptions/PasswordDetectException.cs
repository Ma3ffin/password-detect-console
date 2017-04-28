using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Exceptions
{
    class PasswordDetectException:Exception
    {
        public PasswordDetectException()
        {
        }

        public PasswordDetectException(string message)
            : base(message)
        {
        }

        public PasswordDetectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
