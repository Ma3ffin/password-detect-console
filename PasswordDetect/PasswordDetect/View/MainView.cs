using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Model;

namespace PasswordDetect.View
{
    public class MainView : BaseView
    {
        public List<BaseView> Views { get; set; }

        public BaseView SelectedView { get; set; }

        public RegisterView RegisterView { get; set; }

        public TrainingView TrainingView { get; set; }

        public LoginView LoginView { get; set; }

        public MainView()
        {
            Views = new List<BaseView>();
            Views.Add(new RegisterView());
            Views.Add(new TrainingView());
            Views.Add(new LoginView());
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
            if (input == Views.Count)
            {
                return false;
            }

            try
            {
                SelectedView = Views.ElementAt(input);
                SelectedView.Start();
            }
            catch (Exception e)
            {
                return true;
            }
            
            return true;
        }
    }
}
