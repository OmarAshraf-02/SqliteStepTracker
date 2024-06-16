using Microsoft.Data.Sqlite;

namespace SqliteStepTracker;
internal static class Menu
{
    internal static void PrintMainMenu()
    {
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Insert Steps");
        Console.WriteLine("2. View Steps");
        Console.WriteLine("3. Update Steps");
        Console.WriteLine("4. Delete Steps");
        Console.Write("Input: ");
    }

    internal static void PrintInvalidInput()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please input a valid option (0-4)");
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static string? MainMenuInputLoop(string? input)
    {
        while (!Input.ValidateMainMenuInput(input))
        {
            Console.Clear();
            PrintInvalidInput();
            PrintMainMenu();
            input = Console.ReadLine();
        }

        return input;
    }

    internal static void InsertMenuInputLoop(ref string? stepsInput)
    {
        while (!Input.ValidateInsertionInput(ref stepsInput))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input a valid number of steps");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Steps: ");
            stepsInput = Console.ReadLine();
        }
    }

    internal static void PrintTableRows(SqliteDataReader reader)
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)}\t{reader.GetInt32(1)}\t{reader.GetDateTime(2):dd-MM-yyyy}");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You haven't logged any steps yet");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    internal static void PrintCyan(string s)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(s);
        Console.ForegroundColor = ConsoleColor.White;
    }

}
