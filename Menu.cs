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

    internal static void PrintTitle()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Welcome to Sqlite Step Tracker");
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static void PrintInvalidInput()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please input a valid option (0-4)");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
