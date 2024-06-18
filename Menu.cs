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

    internal static string? MainMenuInputLoop(string? input)
    {
        while (!Input.IsValidMainMenuInput(input))
        {
            Console.Clear();
            PrintError("Please input a valid option (0-4)");
            PrintMainMenu();
            input = Console.ReadLine();
        }
        return input;
    }

    internal static void StepInputLoop(ref string? stepsInput)
    {
        while (!Input.IsValidStepsInput(stepsInput))
        {
            PrintError("Input a valid number of steps >= 0");
            Console.Write("Steps: ");
            stepsInput = Console.ReadLine();
        }
    }

    internal static void IdInputLoop(ref string? idInput, SqliteConnection connection)
    {
        while (!Input.IsValidIdInput(idInput) || !Db.FindId(int.Parse(idInput), connection))
        {
            PrintError("Input an existing integer ID > 0");
            Console.Write("ID: ");
            idInput = Console.ReadLine();
        }
    }

    internal static Table CreateTable(SqliteDataReader reader)
    {
        var table = new Table();
        table.AddColumn("[bold cyan3]ID[/]");
        table.AddColumn("[bold cyan3]Steps[/]");
        table.AddColumn("[bold cyan3]Date[/]");

        while (reader.Read())
        {
            table.AddRow($"{reader.GetInt32(0)}", $"{reader.GetInt32(1)}", $"{reader.GetDateTime(2):dd-MM-yyyy}");
        }

        return table;
    }

    internal static void PrintTable(SqliteConnection connection)
    {
        using SqliteCommand getTable = new(Queries.getTable, connection);
        using SqliteDataReader reader = getTable.ExecuteReader();
        if (reader.HasRows)
        {
            var table = CreateTable(reader);
            CreateTable(reader);
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
