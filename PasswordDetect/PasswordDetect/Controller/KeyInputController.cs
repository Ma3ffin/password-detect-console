using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class KeyInputController : BaseController
    {
        public List<KeyInput> KeyInputs { get; set; }

        public void AddKeyInput(char key, int tick)
        {
            KeyInputs.Add(new KeyInput() { Time = tick, Value = key });
        }

        public KeyInputController()
        {
            KeyInputs = new List<KeyInput>();
        }

        public List<KeyInput> GetKeyInputsWithDeltaTime()
        {
            List<KeyInput> retList = new List<KeyInput>();

            long prevTicks = 0;
            int index = 0;
            KeyInput input;
            foreach (var tick in KeyInputs)
            {
                if (prevTicks == 0)
                {
                    prevTicks = tick.Time;
                    ;
                }
                input = new KeyInput()
                {
                    Position = index,
                    Value = tick.Value,
                    Time = (tick.Time - prevTicks)
                };
                index++;
                retList.Add(input);
                prevTicks = tick.Time;
            }

            return retList;

        }
    }
}
