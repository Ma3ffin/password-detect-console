using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordDetect.View
{
    public abstract class BaseView
    {
        public string Kontext { get; set; }

        public void WriteLineToConsole(string message)
        {
            Console.WriteLine(this.Kontext + "\t>\t" + message);
        }

        public void WriteToConsole(string message)
        {
            Console.Write(this.Kontext + "\t>\t" + message);
        }

        public string ReadUsername()
        {
            Console.Write(this.Kontext + "\t>\t");
            string username = null;
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    break;
                username += key.KeyChar;
            }
            WriteLineToConsole(username);
            return username;
        }

        public bool RepeateOperation(string question)
        {
            WriteLineToConsole(question + "[Y/N]");
            while (true)
            {
                WriteToConsole("");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("\n");
                    return true;
                }
                if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("\n");
                    return false;
                }
                Console.WriteLine("\n");
            }
        }

        public abstract void Start();
    }
}
