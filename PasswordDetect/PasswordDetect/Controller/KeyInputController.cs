using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Data;
using PasswordDetect.Handler;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class KeyInputController : BaseController
    {
        public List<KeyInput> KeyInputs { get; set; }

        public KeyInputController(DataAccess db, IErrorHandler errorHandler) : base(db, errorHandler)
        {
            Reset();
        }

        public void Reset()
        {
            KeyInputs = new List<KeyInput>();
        }

        public void AddKeyInput(char key, int tick)
        {
            KeyInputs.Add(new KeyInput() { Time = tick });
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
                }

                long delta = tick.Time - prevTicks;

                if (delta != 0)
                {
                    input = new KeyInput()
                    {
                        Position = index,
                        Time = (delta)
                    };
                    index++;
                    retList.Add(input);
                }
                prevTicks = tick.Time;
            }
            KeyInputs = new List<KeyInput>();
            return retList;

        }

 
    }
}
