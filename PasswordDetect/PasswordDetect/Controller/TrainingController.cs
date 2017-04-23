using System.Collections.Generic;
using PasswordDetect.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Controller
{
    public class TrainingController : BaseController
    {

        public Training Training { get; set; }

        public List<KeyInput> KeyInputs { get; set; }

        public TrainingController()
        {
            Training = new Training();
            KeyInputs = new List<KeyInput>();
        }

        public void TrackInput(char key, int tick)
        {
            KeyInputs.Add(new KeyInput() {Time = tick, Value = key});
        }

        public bool AddTraining(User user)
        {
            if (KeyInputs.Count != 0)
            {
                Training.KeyInputs = AddKeyInputsWithDeltaTime();
                Training.User = DetectionContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
                Training.Time = GetTainingtime(Training.KeyInputs);
                DetectionContext.Trainings.Add(Training);
                DetectionContext.SaveChanges();
                return true;
            }
            return false;

        }

        private long GetTainingtime(IList<KeyInput> inputs)
        {
            long ret = 0;
            foreach (var input in inputs)
            {
                ret += input.Time;
            }
            return ret;
        }

        public List<KeyInput> AddKeyInputsWithDeltaTime()
        {
            List<KeyInput> retList = new List<KeyInput>();

            long prevTicks = 0;
            int index = 0;
            KeyInput input;
            foreach (var tick in KeyInputs)
            {
                if (prevTicks == 0) {
                    prevTicks = tick.Time;
                    ;
                }
                input = new KeyInput() {
                    Position = index ,
                    Value = tick.Value,
                    Time = (tick.Time - prevTicks),
                    Training = Training
                };
                index++;
                retList.Add(input);
                prevTicks = tick.Time;
            }

            return retList;

        }

    }
}
