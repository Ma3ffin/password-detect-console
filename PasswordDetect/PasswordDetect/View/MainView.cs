using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Exceptions;
using PasswordDetect.Handler;
using PasswordDetect.Model;

namespace PasswordDetect.View
{
    public class MainView : BaseView
    {
        public List<MenuItem> Views { get; set; }

        public MenuItem SelectedView { get; set; }

        public RegisterView RegisterView { get; set; }

        public TrainingView TrainingView { get; set; }

        public LoginView LoginView { get; set; }

        public ImportExportView ImportExportView { get; set; }

        public MainView(IEnumerable<MenuItem> views):base(null)
        {
            Views = views.ToList();
            Kontext = "Main";
        }

        public override void Start()
        {
            while (Navigate()) { }
        }

        private bool Navigate()
        {
            WriteLineToConsole("What do you want to do?");
            foreach (var view in Views)
            {
                WriteLineToConsole(Views.IndexOf(view) + ") " + view.Kontext);
            }
            WriteLineToConsole(Views.Count() + ") Quit");
            WriteToConsole("");
            var key = Console.ReadKey();
            Console.WriteLine("\n");
            int input = (int)Char.GetNumericValue(key.KeyChar);
            if (input == Views.Count())
            {
                return false;
            }

            try
            {
                SelectedView = Views.ElementAt(input);
                SelectedView.Start();
            }
            catch (ArgumentOutOfRangeException)
            {
                WriteToConsole("Invalid Input!\n");
                return true;
            }
            catch (PasswordDetectException ex)
            {
                Console.WriteLine("Programm Error occoured! Please contact the Software Manufacturerer Error: " +
                                  ex.Message);
                return true;
            }
            

            return true;
        }
    }
}
