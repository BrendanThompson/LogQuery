using System;
using System.IO;

namespace LogQuery.Lib
{
    class CheckRoutines
    {
        public static void CheckDestinationExists(string dst)
        {
            string input;

            if (!Directory.Exists(dst))
            {
                Console.WriteLine();
                ConsoleOptions.ErrorMessage("Directory doesn't exist!");
                Console.WriteLine();
                Console.Write("Would you like to create it (Y/N): ");
                input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "y":
                    case "Y":
                    case "yes":
                    case "Yes":
                    case "YES":
                        CheckRoutineActions.CreateDestination(dst);
                        break;
                    default:
                        ConsoleOptions.WarningMessage("The destination directory has not been created!");
                        Environment.Exit(2);
                        break;
                }
            }
        }
    }

    class CheckRoutineActions
    {
        public static void CreateDestination(string dst)
        {
            try
            {
                Directory.CreateDirectory(dst);

                if (Directory.Exists(dst))
                {
                    ConsoleOptions.InformationalMessage(String.Format("Directory {0} has been created!", dst));
                    Console.WriteLine();
                }
                else
                {
                    ConsoleOptions.ErrorMessage("Directory has not been created. Not really sure what happened.");
                }
            }
            catch (Exception ex)
            {
                ConsoleOptions.ErrorMessage("Directory has not been created! An exception has been caught!");
            }
        }
    }
}
