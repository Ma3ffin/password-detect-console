using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using PasswordDetect.Controller;

namespace PasswordDetect.View
{
    public class LoginView : BaseView
    {
        public NormalDistribution Distribution { get; set; }

        public UserController UserController { get; set; }

        public TrainingController TrainingController { get; set; }

        public LoginController LoginController { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginView(LoginController loginController, UserController userController, KeyInputController keyInputController, TrainingController trainingController): base(keyInputController)
        {
            Kontext = "Login";
            UserController = userController;
            TrainingController = trainingController;
            LoginController = loginController;
        }

        public override void Start()
        {
            do
            {
                WriteLineToConsole("Login a User.");
                KeyInputController.Reset();
                InputUser();
            } while (RepeateOperation("Login another User?"));
        }

        private void InputUser()
        {
            WriteLineToConsole("Enter a Username:");
            Username = ReadUsername();
            WriteLineToConsole("Enter a Password:");
            Password = ReadPasswordWithTime();

            if (UserController.UserExists(Username, Password))
            {
                if (LoginController.CheckInputPattern(UserController.User, KeyInputController.GetKeyInputsWithDeltaTime()))
                {
                    TrainingController.AddTraining(UserController.User,
                        KeyInputController.GetKeyInputsWithDeltaTime());
                    WriteLineToConsole("User " + Username + " was Loged in.");
                }
            }
            

        }
    }
}
