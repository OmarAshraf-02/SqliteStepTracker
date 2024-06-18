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
            using (SqliteCommand insert = new(sql, connection))
            {
                insert.ExecuteNonQuery();
            }

            Menu.PrintCyan($"Logged steps for {currentDate:dd-MM-yyyy}!");
            Menu.EnterToContinue();
            break;
        case 2:
            using (SqliteCommand getTable = new(Queries.getTable, connection))
            {
                using SqliteDataReader reader = getTable.ExecuteReader();
                Menu.PrintTableRows(reader);

                Menu.EnterToContinue();
            }
            break;
        case 3:
            while (true)
            {
                using (SqliteCommand getTable = new(Queries.getTable, connection))
                {
                    using SqliteDataReader reader = getTable.ExecuteReader();
                    Menu.PrintTableRows(reader);
                }

                Console.WriteLine("Choose which ID to update");
                Console.Write("ID: ");
                string? idInput = Console.ReadLine();

                bool isValidNumber = int.TryParse(idInput, out int parsedId) && parsedId > 0;
                if (!isValidNumber)
                {
                    Menu.PrintError("Input a valid integer ID > 0");
                    continue;
                }

                string findIdQuery = $"SELECT * FROM steps WHERE id={parsedId}";
                SqliteCommand findId = new(findIdQuery, connection);
                SqliteDataReader findIdReader = findId.ExecuteReader();

                if (!findIdReader.HasRows)
                {
                    Menu.PrintError("ID wasn't found");
                    continue;
                }

                Console.Write($"Updated Steps for entry no.{parsedId}: ");
                string? stepInput = Console.ReadLine();
                bool isValidSteps = int.TryParse(stepInput, out int parsedSteps) && parsedSteps > 0;

                if (!isValidSteps)
                {
                    Menu.PrintError("Input a valid number of steps >= 0");
                    continue;
                }

                string updateQuery = $"UPDATE steps SET steps = {parsedSteps} WHERE id = {parsedId}";
                SqliteCommand updateSteps = new(updateQuery, connection);
                updateSteps.ExecuteNonQuery();

                Menu.PrintCyan($"Updated step entry no.{parsedId} to {parsedSteps} steps");
                Menu.EnterToContinue();
                break;
            }
            break;
        case 4:

            break;
    }
    if (exit) break;
}
Console.Clear();
