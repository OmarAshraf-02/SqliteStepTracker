using SqliteStepTracker;
using Microsoft.Data.Sqlite;

Console.Clear();

bool exit = false;
string path = $"Data Source={Environment.CurrentDirectory}/Steps.db";

using SqliteConnection connection = new(path);
connection.Open();

SqliteCommand createTable = new(Queries.createTable, connection);
createTable.ExecuteNonQuery();

while (true)
{
    Menu.PrintCyan("Sqlite Step Tracker");
    Menu.PrintMainMenu();

    string? input = Console.ReadLine();

    input = Menu.MainMenuInputLoop(input);

    bool _ = int.TryParse(input, out int parsedInput);

    switch (parsedInput)
    {
        case 0:
            exit = true;
            break;
        case 1:
            Console.Write("Steps: ");
            string? stepsInput = Console.ReadLine();

            Menu.InsertMenuInputLoop(ref stepsInput);

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            string date = currentDate.ToString("yyyy-MM-dd");

            bool parse = int.TryParse(stepsInput, out int steps);
            string sql = $"INSERT INTO steps(steps,date) VALUES({steps},'{date}')";

            SqliteCommand insert = new(sql, connection);
            insert.ExecuteNonQuery();

            Menu.PrintCyan($"Logged steps for {currentDate:dd-MM-yyyy}!");
            break;
        case 2:
            Menu.PrintCyan("ID\tSteps\tDate");

            SqliteCommand getTable = new(Queries.getTable, connection);
            SqliteDataReader reader = getTable.ExecuteReader();
            Menu.PrintTableRows(reader);
            break;
        case 3:
            Console.WriteLine("Choose which ID to update");

            break;
        case 4:

            break;
    }

    if (exit) break;
}
