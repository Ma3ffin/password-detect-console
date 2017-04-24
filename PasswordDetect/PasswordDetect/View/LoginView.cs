using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Controller;

namespace PasswordDetect.View
{
    public class LoginView : BaseView
    {
        public UserController UserController { get; set; }

        public TrainingController TrainingController { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginView()
        {
            Kontext = "Login";
            UserController = new UserController();
        }

        public override void Start()
        {
            do
            {
                TrainingController = new TrainingController();
                WriteLineToConsole("Login a User.");
                InputUser();
            } while (RepeateOperation("Login another User?"));
        }

        private void InputUser()
        {
            WriteLineToConsole("Enter a Username:");
            Username = ReadUsername();
            WriteLineToConsole("Enter a Password:");
            Password = ReadPassword();

            if (UserController.UserExists(Username, Password))
            {
                WriteLineToConsole("User " + Username + " was Loged in.");
            }

        }

        private string ReadPassword()
        {
            WriteToConsole("");
            string password = null;
            string output = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                TrainingController.TrackInput(key.KeyChar, Environment.TickCount);
                password += key.KeyChar;
                output += "*";
            }
            Console.Write(output + "\n");
            return password;
        }
    }
}
