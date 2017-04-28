using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Controller
{
    public interface IImportExportController
    {
        bool Import();
        bool Export();
    }
}
