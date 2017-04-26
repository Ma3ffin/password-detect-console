using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class LoginController : BaseController
    {
        public User User { get; set; }

        public List<Training> Trainings { get; set; }

        public Dictionary<int,List<long>> InputPositions { get; set; }

        public Dictionary<int, NormalDistribution> InputDistributions { get; set; }

        private int Majority { get; set; }

        public LoginController() : base()
        {
            Trainings = new List<Training>();
            InputPositions = new Dictionary<int, List<long>>();
            InputDistributions = new Dictionary<int, NormalDistribution>();
        }

        public bool CheckInputPattern(User user, List<KeyInput> keyInputs)
        {
            GetTraining(user);
            GetInputTimes();
            GetInputDistributions();
            SetMajority();

            if (SimilarInput(keyInputs))
            {
                return true;
            }

            ErrorHandler.WriteErrorToConsole("Wrong User was typing. User was not logged in.");
            return false;
        }

        private void SetMajority()
        {
            
            Majority = (InputPositions.Count() / 2)+1; ;
        }

        private bool SimilarInput(List<KeyInput> keyInputs)
        {
            bool ret = true;

           List<bool> similarityBools = new List<bool>();

            foreach (var item in keyInputs)
            {
                if (InputDistributions[item.Position].Quartiles.IsInside(Convert.ToDouble(item.Time)))
                {
                    similarityBools.Add(true);
                }
                else
                {
                    similarityBools.Add(false);
                }
            }

            int error = 0;
            ErrorHandler.WriteErrorToConsole("\n\n");
            foreach (var item in similarityBools)
            {
                ErrorHandler.WriteErrorToConsole(item.ToString());
                if (!item)
                {
                    error++;
                }

                if (error == Majority)
                {
                    ret = false;
                }
                
            }
            ErrorHandler.WriteErrorToConsole("\n\n");

            return ret;
        }

        private void GetTraining(User user)
        {
            Trainings = DataAccess.GetTrainingsForUser(user);
        }

        public void GetInputTimes()
        {
            foreach (var item in Trainings.First().KeyInputs)
            {
                InputPositions.Add(item.Position,new List<long>());
            }

            foreach (var item in Trainings)
            {
                foreach (var input in item.KeyInputs)
                {
                    InputPositions[input.Position].Add(input.Time);
                }
            }
        }

        private void GetInputDistributions()
        {
            try
            {
                foreach (var item in InputPositions)
                {
                    InputDistributions.Add(item.Key, NormalDistribution.Estimate(item.Value.ToArray().ToDouble()));
                }

            }
            catch (Exception e)
            {
                if (e.Message.Equals("Variance is zero. Try specifying a regularization constant in the fitting options."))
                {
                    ErrorHandler.WriteErrorToConsole("This User is not trained enougth. Please train him more.\n");
                }
                
                throw;
            }
            
        }

    }
}
