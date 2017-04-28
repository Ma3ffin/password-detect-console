using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordDetect.Controller;

namespace PasswordDetect.View
{
    public class ImportExportView : BaseView
    {
        public IImportExportController ImportExportController { get; set; }

        public ImportExportView(IImportExportController importExportController) : base(null)
        {
            Kontext = "Import/Export";
            ImportExportController = importExportController;
        }

        public override void Start()
        {
            WriteLineToConsole("What do you want to do?");
            WriteLineToConsole("i) Import database from json");
            WriteLineToConsole("e) Export database as json");

            KeySelect();
        }

        public bool KeySelect()
        {

            while (true)
            {
                WriteToConsole("");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.I)
                {
                    Console.WriteLine("\n");
                    return Import();
                }else if (key.Key == ConsoleKey.E)
                {
                    Console.WriteLine("\n");
                    return Export();
                }
                else
                {
                    Console.WriteLine("\n");
                    WriteToConsole("Invalid input!");
                }
                Console.WriteLine("\n");
            }
        }

        private bool Import()
        {
            bool success = ImportExportController.Import();
            if (success)
            {
                WriteLineToConsole("Import was successful.");
            }
            return success;
        }

        private bool Export()
        {
            bool success = ImportExportController.Export();
            if (success)
            {
                WriteLineToConsole("Export was successful.");
            }
            return success;
        }
    }
}
