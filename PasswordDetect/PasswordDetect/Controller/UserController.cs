using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class UserController : BaseController
    {
        public User User { get; set; }

        public bool AddUser(string username, string password)
        {
            User = new User() {Username = username, Password = password};

            if (!UserExists())
            {
                if (ValidUser())
                {
                    if (ValidPassword())
                    {
                        DetectionContext.Users.Add(User);
                        DetectionContext.SaveChanges();
                        return true;
                    }
                    ErrorHandler.WriteErrorToConsole("Password invalid.");
                    return false;
                }
                ErrorHandler.WriteErrorToConsole("Username invalid.");
                return false;
            }
            ErrorHandler.WriteErrorToConsole("Username exists allredy.");
            return false;

        }

        private bool ValidPassword()
        {
            if (User.Password != null)
            {
                if (User.Password.Length < 5)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool ValidUser()
        {
            if (User.Username != null)
            {
                if (User.Username.Length < 3)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool UserExists()
        {
            return DetectionContext.Users.Count(u => u.Username == User.Username) > 0;

        }
    }
}
