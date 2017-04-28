using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Model;

namespace PasswordDetect.Data
{
    public class DataAccess
    {
        private readonly DetectionContext db;

        public DataAccess(DetectionContext db)
        {
            this.db = db;
        }

        public void AddUser(User user)
        {
            user.Password =Hashing.HashPassword(user.Password);
            db.Users.Add(user);
        }

        public void AddUsers(List<User> users)
        {
            db.Users.AddRange(users);
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Count(u => u.Username == username) > 0;
        }

        public User UserAuthentificate(string username, string password)
        {
            User ret = db.Users.FirstOrDefault(u => u.Username == username);

            if (ret != null)
            {
                if (!Hashing.ValidatePassword(password, ret.Password))
                {
                    ret = null;
                }
            }

            return ret;
        }

        public User GetUserByUserId(int userId)
        {
            return db.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void AddTraining(Training training)
        {
            db.Trainings.Add(training);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public List<Training> GetTrainingsForUser(User user)
        {
            return user.Trainings.ToList();
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            users = db.Users
                .Include("Trainings")
                .Include("Trainings.KeyInputs")
                .ToList();


            return users;
        }

        public void RemoveAllEntries()
        {
            db.KeyInputs.RemoveRange(db.KeyInputs.ToList());
            db.Trainings.RemoveRange(db.Trainings.ToList());
            db.Users.RemoveRange(db.Users.ToList());
        }
    }
}
