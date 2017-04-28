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

        public TrainingView(UserController userController, TrainingController trainingController, KeyInputController keyInputController) : base(keyInputController)
        {
            Kontext = "Train";
            UserController = userController;
            TrainingController = trainingController;
        }

        public override void Start()
        {
            do
            {
                WriteLineToConsole("Train a User.");
                TrainingController.Reset();
                KeyInputController.Reset();
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
