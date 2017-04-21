using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDetect.Model
{
    public class DetectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<KeyInput> KeyInputs { get; set; }

        public DetectionContext() : base()
        {

        }
    }
}
