using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasswordDetect.Model;

namespace PasswordDetect.Controller
{
    public class ImportExportController : BaseController
    {
        public bool Import()
        {

            try
            {
                string json = ReadFromFile();

                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                DataAccess.RemoveAllEntries();
                DataAccess.SaveChanges();
                DataAccess.AddUsers(users);
                DataAccess.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                ErrorHandler.WriteErrorToConsole("Import failed. Check if the File ..bin/data/import/data.json exists and has the right format");
                return false;
            }
           
        }

        public bool Export()
        {
            List<User> users = DataAccess.GetAllUsers();
            string json = JsonConvert.SerializeObject(users, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            try
            {
                WriteToFile(json);
            }
            catch (Exception e)
            {
                ErrorHandler.WriteErrorToConsole("Export failed. Check if the Folder ..bin/data/export/ exists.");
                return false;
            }

            return true;
        }

        private bool WriteToFile(string json)
        {
            FileStream fileStream = File.Open("../data/export/data.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            var writer = new StreamWriter(fileStream);
            writer.Write(json);
            writer.Flush();
            writer.Close();

            return true;
        }

        private string ReadFromFile()
        {
            FileStream fileStream = File.Open("../data/import/data.json", FileMode.Open, FileAccess.Read);

            var reader = new StreamReader(fileStream);

            string json = reader.ReadToEnd();

            return json;
        }
    }
}
