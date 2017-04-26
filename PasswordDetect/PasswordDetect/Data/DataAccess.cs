using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Model;

namespace PasswordDetect.Data
{
    public class DataAccess
    {
        private static DetectionContext db;

        public DataAccess(DetectionContext db = null)
        {
            if (null != db)
            {
                DataAccess.db = db;
            }

            if (null == DataAccess.db)
            {
                DataAccess.db = new DetectionContext();

            }
        }

        public void AddUser(User user)
        {
            db.Users.Add(user);
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Count(u => u.Username == username) > 0;
        }

        public User UserAuthentificate(string username, string password)
        {
            return db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
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
            List<Training> trainings = new List<Training>();

            IQueryable<Training> trainingQuery =
                from t in db.Trainings
                join u in db.Users
                on t.User.UserId equals u.UserId
                where t.User.UserId == user.UserId
                select t;

            foreach (Training item in trainingQuery)
            {
                trainings.Add(item);
            }

            foreach (Training item in trainings)
            {
                IQueryable<KeyInput> keyInputQuery =
                    from k in db.KeyInputs
                    join t in db.Trainings
                    on k.Training.TrainingId equals t.TrainingId
                    where k.Training.TrainingId == item.TrainingId
                    select k;


                foreach (KeyInput input in keyInputQuery) { }
            }

            return trainings;
        }
    }
}
