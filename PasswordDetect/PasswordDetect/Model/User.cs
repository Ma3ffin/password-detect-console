﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual IList<Training> Trainings { get; set; }

        public User()
        {

        }
    }
}
