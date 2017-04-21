using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Model
{
    public class Training
    {
        public int TrainingId { get; set; }

        public long Time { get; set; }

        public User User { get; set; }

        public IList<KeyInput> KeyInputs { get; set; }

        public Training()
        {

        }
    }
}
