using PasswordDetect.Controller;
using PasswordDetect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.View
{
    public class TrainingView : BaseView
    {
        public UserController UserController { get; set; }

        public TrainingController TrainingController { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public TrainingView()
        {
            Kontext = "Train";
            UserController = new UserController();
        }

        public override void Start()
        {
            do
            {
                TrainingController = new TrainingController();
                KeyInputController = new KeyInputController();
                WriteLineToConsole("Train a User.");
                InputUser();
            } while (RepeateOperation("Train another User?"));
        }

        private void InputUser()
        {
            WriteLineToConsole("Enter a Username:");
            Username = ReadUsername();
            WriteLineToConsole("Enter a Password:");
            Password = ReadPasswordWithTime();

            if (UserController.UserExists(Username, Password))
            {
                TrainingController.AddTraining(UserController.User, KeyInputController.GetKeyInputsWithDeltaTime());
                WriteLineToConsole("User " + Username + " was trained.");
            }

        }
    }
}
