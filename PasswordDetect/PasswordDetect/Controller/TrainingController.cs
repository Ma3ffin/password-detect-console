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

        public TrainingController()
        {
            Training = new Training();
        }

        public bool AddTraining(User user, List<KeyInput> keyInputs)
        {
            if (keyInputs.Count != 0)
            {
                Training.KeyInputs = keyInputs;
                foreach (var input in Training.KeyInputs)
                {
                    input.Training = Training;
                }
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

    }
}
