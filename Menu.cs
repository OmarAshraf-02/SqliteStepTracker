using Microsoft.Data.Sqlite;
using Spectre.Console;

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
            var table = new Table();
            table.AddColumn("[bold cyan3]ID[/]");
            table.AddColumn("[bold cyan3]Steps[/]");
            table.AddColumn("[bold cyan3]Date[/]");
            while (reader.Read())
            {
                table.AddRow($"{reader.GetInt32(0)}", $"{reader.GetInt32(1)}", $"{reader.GetDateTime(2):dd-MM-yyyy}");
            }
            AnsiConsole.Write(table);
        }
        else
        {
            PrintError("You haven't logged any steps yet");
        }
    }

    internal static void PrintCyan(string s)
    {
        AnsiConsole.Markup($"[bold cyan3]{s}[/]\n");
    }

    internal static void PrintError(string s)
    {
        AnsiConsole.Markup($"[bold maroon]{s}[/]\n");
    }

    internal static void EnterToContinue()
    {
        PrintCyan("Press ENTER to continue");
        Console.ReadLine();
        Console.Clear();
    }

}
