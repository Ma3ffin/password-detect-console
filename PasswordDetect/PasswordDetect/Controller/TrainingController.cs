using PasswordDetect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Controller
{
    public class TrainingController : BaseController
    {

        public Training Training { get; set; }

        public Dictionary<char,int> Ticks { get; set; }

        public List<KeyInput> KeyInputs { get; set; }

        public TrainingController()
        {
            Training = new Training();
            Ticks = new Dictionary<char, int>();
            KeyInputs = new List<KeyInput>();
        }

        public void TrackInput(char key, int tick)
        {
            Ticks.Add(key, tick);
        }

        public bool AddTraining(User user)
        {
            if (Ticks.Count != 0)
            {
                AddKeyInputsWithDeltaTime();
                Training.KeyInputs = KeyInputs;
                Training.User = user;
                Training.Time = GetTainingtime();
                DetectionContext.Trainings.Add(Training);
                DetectionContext.SaveChanges();
                return true;
            }
            return false;

        }

        private long GetTainingtime()
        {
            long ret = 0;
            foreach (var input in KeyInputs)
            {
                ret += input.Time;
            }
            return ret;
        }

        public void AddKeyInputsWithDeltaTime()
        {
            int prevTicks = 0;
            int index = 0;
            KeyInput input;
            foreach (var tick in Ticks)
            {
                if (prevTicks == 0) {
                    prevTicks = tick.Value;
                    ;
                }
                input = new KeyInput() {
                    Position = index ,
                    Value = tick.Key,
                    Time = (tick.Value - prevTicks),
                    Training = Training
                };
                index++;
                KeyInputs.Add(input);
                prevTicks = tick.Value;
            }

        }

        private void saveTraining()
        {
            DetectionContext.Trainings.Add(Training);
            DetectionContext.SaveChanges();
        }


    }
}
