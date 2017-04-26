using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PasswordDetect.Controller;

namespace PasswordDetect.View
{
    public abstract class BaseView
    {
        public KeyInputController KeyInputController { get; set; }

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

        public string ReadPasswordWithTime()
        {
            WriteToConsole("");
            string password = null;
            string output = null;
            
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                KeyInputController.AddKeyInput(key.KeyChar, Environment.TickCount);
                password += key.KeyChar;
                output += "*";
            }
            Console.Write(output + "\n");
            return password;
        }

        public string ReadPassword()
        {
            WriteToConsole("");
            string password = null;
            string output = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
                output += "*";
            }
            Console.Write(output + "\n");
            return password;
        }

        public abstract void Start();
    }
}
