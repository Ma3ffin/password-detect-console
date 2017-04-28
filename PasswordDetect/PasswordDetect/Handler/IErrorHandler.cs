using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Handler
{
    public interface IErrorHandler
    {
        void Error(string error);
    }
}
