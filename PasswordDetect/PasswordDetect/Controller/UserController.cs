﻿using System;
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

            if (!UsernameExists())
            {
                if (ValidUser())
                {
                    if (ValidPassword())
                    {
                        DataAccess.AddUser(User);
                        DataAccess.SaveChanges();
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

        public bool UserExists(string username, string password)
        {
            User = new User() { Username = username, Password = password };
            User = UserAuthentificate();

            if (User != null)
            {
                return true;
            }
            ErrorHandler.WriteErrorToConsole("User does not exist.");
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

        private bool UsernameExists()
        {
            return DataAccess.UsernameExists(User.Username);
        }

        public User UserAuthentificate()
        {
            return DataAccess.UserAuthentificate(User.Username,User.Password);
        }
    }
}
