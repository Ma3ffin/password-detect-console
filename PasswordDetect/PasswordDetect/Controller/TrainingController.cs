using System.Collections.Generic;
using PasswordDetect.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Data;
using PasswordDetect.Handler;

namespace PasswordDetect.Controller
{
    public class TrainingController : BaseController
    {

        public Training Training { get; set; }

        public TrainingController(DataAccess db, IErrorHandler errorHandler) : base(db, errorHandler)
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
                Training.User = DataAccess.GetUserByUserId(user.UserId);
                Training.Time = GetTainingtime(Training.KeyInputs);
                DataAccess.AddTraining(Training);
                DataAccess.SaveChanges();
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
