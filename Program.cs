using SqliteStepTracker;
using Microsoft.Data.Sqlite;

Console.Clear();
bool exit = false;

var dropTable = @"DROP TABLE steps";

var createTable = @"CREATE TABLE steps(
    id INTEGER PRIMARY KEY,
    steps INTEGER NOT NULL,
    date DATE NOT NULL
)";

try
{
    using SqliteConnection connection = new($"Data Source={Environment.CurrentDirectory}/StepTracker.db");
    connection.Open();

    using SqliteCommand dropTableCommand = new(dropTable, connection);
    dropTableCommand.ExecuteNonQuery();

    using SqliteCommand createTableCommand = new(createTable, connection);
    createTableCommand.ExecuteNonQuery();

    Console.WriteLine("Table 'steps' created successfully.");
}
catch (SqliteException ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    Menu.PrintTitle();
    Menu.PrintMainMenu();

    string? input = Console.ReadLine();

    while (!Input.ValidateMainMenuInput(input))
    {
        Console.Clear();
        Menu.PrintInvalidInput();
        Menu.PrintMainMenu();
        input = Console.ReadLine();
    }

    int parsedInput = int.Parse(input);

    switch (parsedInput)
    {
        case 0:
            exit = true;
            break;
    }

    Console.Clear();

    if (exit) break;
}