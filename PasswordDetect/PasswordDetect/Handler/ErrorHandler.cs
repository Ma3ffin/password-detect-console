using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Handler
{
    public class ErrorHandler
    {
        public void WriteErrorToConsole(string error)
        {
            Console.WriteLine("Error\t>\t" + error);
        }
    }
}
