using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.View
{
    public interface MenuItem
    {
        string Kontext { get; set; }

        void Start();
    }
}
