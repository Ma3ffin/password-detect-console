using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using PasswordDetect.Data;
using PasswordDetect.Exceptions;
using PasswordDetect.Handler;
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

        public LoginController(DataAccess db, IErrorHandler errorHandler) : base(db, errorHandler)
        {
            ResetController();
        }

        public void ResetController()
        {
            Trainings = new List<Training>();
            InputPositions = new Dictionary<int, List<long>>();
            InputDistributions = new Dictionary<int, NormalDistribution>();
        }

        public bool CheckInputPattern(User user, List<KeyInput> keyInputs)
        {
            if (GetTraining(user))
            {
                try
                {
                    GetInputTimes();
                    GetInputDistributions();
                    SetMajority();

                    if (SimilarInput(keyInputs))
                    {
                        ResetController();
                        return true;
                    }

                    ErrorHandler.Error("Wrong User was typing. User was not logged in.");
                    ResetController();
                }
                catch (UserNotTrainedEnoughtException e)
                {
                    ErrorHandler.Error(e.Message);
                    return false;
                }
            }
            else
            {
                ErrorHandler.Error("User is not trained.");
                ResetController();
            }
            return false;
        }

        private void SetMajority()
        {
            
            Majority = (InputPositions.Count() / 2)+1; ;
        }

        private bool SimilarInput(List<KeyInput> keyInputs, bool output = false)
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
            foreach (var item in similarityBools)
            {
                if (!item)
                {
                    error++;
                }

                if (error == Majority)
                {
                    ret = false;
                }
                
            }

            if (output)
            {
                OutputResult(similarityBools);
            }

            return ret;
        }

        private void OutputResult(List<bool> similarityBools)
        {
            ErrorHandler.Error("\n");
            foreach (var item in similarityBools)
            {
                ErrorHandler.Error(item.ToString());
            }
            ErrorHandler.Error("\n");
        }

        private bool GetTraining(User user)
        {
            Trainings = DataAccess.GetTrainingsForUser(user);
            if (Trainings.Count < 1)
            {
                return false;
            }

            return true;
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
            
            catch (ArgumentException e)
            {

                throw new UserNotTrainedEnoughtException("This User is not trained enougth. Please train more.\n", e);
            }
            
        }


    }
}
