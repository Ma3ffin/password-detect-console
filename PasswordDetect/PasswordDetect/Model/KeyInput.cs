using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Model
{
    public class KeyInput
    {
        public int KeyInputId { get; set; }

        public char Value { get; set; }

        public long Time { get; set; }

        public int Position { get; set; }

        public Training Training { get; set; }
    }
}
