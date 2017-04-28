using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Controller;

namespace PasswordDetect.View
{
    public class RegisterView : BaseView
    {
        public UserController UserController { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public RegisterView(UserController userController): base(null)
        {
            Kontext = "Add";
            UserController = userController;
        }

        public override void Start()
        {

            do
            {
                WriteLineToConsole("Register a User.");
                InputUser();
            } while (RepeateOperation("Add another User?"));
        }

        private void InputUser()
        {
            WriteLineToConsole("Enter a Username:");
            Username = ReadUsername();
            WriteLineToConsole("Enter a Password:");
            Password = ReadPassword();

            if (UserController.AddUser(Username, Password))
            {
                WriteLineToConsole("User " + Username + " was registered.");
            }
            


        }
    }
}
