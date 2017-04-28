using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordDetect.Model
{

    public class KeyInput
    {
        public int KeyInputId { get; set; }

        public long Time { get; set; }

        public int Position { get; set; }

        public Training Training { get; set; }
    }
}
