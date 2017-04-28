using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordDetect.Model
{
    public class Training
    {
        public int TrainingId { get; set; }

        public long Time { get; set; }

        public User User { get; set; }

        public virtual List<KeyInput> KeyInputs { get; set; }

        public Training()
        {

        }

    }
}
