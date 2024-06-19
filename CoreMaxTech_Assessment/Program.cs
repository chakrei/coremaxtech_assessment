using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CoreMaxTech_Assessment
{
    class Program
    {
        static List<string> allowedExtension = ConfigurationManager.AppSettings["AllowedFileExtensions"].Split('|').ToList();

        static void InitialValidations(string filePath)
        {
            if (!allowedExtension.Contains(Path.GetExtension(filePath)))
            {
                MessageHelper.ErrorMessage("Only text files are allowed. Please enter a valid path and try again.");
                Environment.Exit(0);
            }

            if (!File.Exists(filePath))
            {
                MessageHelper.ErrorMessage($"Error - file at {filePath} not found. Please restart the program and give right path to the file.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        static void Main(string[] args)
        {
            double timeToWait = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["LoopTimerInSeconds"])).TotalMilliseconds;

            if (timeToWait < 1)
                MessageHelper.WarningMessage("Loop wait timer is ZERO, this may cause excessive CPU load. Please recheck your configuration");

            MessageHelper.Info("Welcome!! This program will keep track of the file you need to keep an eye on." + Environment.NewLine);

            Console.Write("Enter path to file : ");
            var filePath = Console.ReadLine();

            InitialValidations(filePath);

            MessageHelper.Info($"Found file at path {filePath}.\nStarting to watch the file for every 15 seconds");

            FileTracker.WatchFileChanges(filePath, timeToWait);
        }
    }
}
