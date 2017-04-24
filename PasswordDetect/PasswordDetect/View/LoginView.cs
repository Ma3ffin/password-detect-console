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

        public LoginController LoginController { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginView()
        {
            Kontext = "Login";
            UserController = new UserController();
            //Normalverteilung Test
            Distribution = NormalDistribution.Estimate(new double[4] { 172, 170, 174, 172 });
        }

        public override void Start()
        {
            do
            {
                KeyInputController = new KeyInputController();
                WriteLineToConsole("Login a User.");
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
                WriteLineToConsole("User " + Username + " was Loged in.");
            }

        }
    }
}
