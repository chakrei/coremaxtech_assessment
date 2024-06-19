using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreMaxTech_Assessment
{
    class MessageHelper
    {
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!! " + message + "\nPress any key to exit");
            Console.ResetColor();
        }

        public static void Info(string message)
        {
            Console.WriteLine(message);
        }

        internal static void WarningMessage(string v)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning!! " + v);
            Console.ResetColor();
        }
    }
}
