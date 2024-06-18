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
    _ = int.TryParse(input, out int parsedInput);

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

            _ = int.TryParse(stepsInput, out int steps);
            string idExistsQuery = $"SELECT * FROM steps WHERE date='{date}'";

            SqliteCommand idExists = new(idExistsQuery, connection);
            SqliteDataReader idExistsReader = idExists.ExecuteReader();

            if (idExistsReader.HasRows)
            {
                Menu.PrintError("You already logged your steps for today, select 'Update Steps' to change this entry");
                Menu.EnterToContinue();
                continue;
            }

            string insertQuery = $"INSERT INTO steps(steps,date) VALUES({steps},'{date}')";
            using (SqliteCommand insert = new(insertQuery, connection))
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
            using (SqliteCommand getTable = new(Queries.getTable, connection))
            {
                using SqliteDataReader reader = getTable.ExecuteReader();
                Menu.PrintTableRows(reader);
            }
            while (true)
            {
                Console.WriteLine("Choose which entry to update");
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
            using (SqliteCommand getTable = new(Queries.getTable, connection))
            {
                using SqliteDataReader reader = getTable.ExecuteReader();
                Menu.PrintTableRows(reader);
            }
            while (true)
            {
                Console.WriteLine("Choose which entry to delete");
                Console.Write("ID: ");
                string? idToDeleteInput = Console.ReadLine();

                bool isValidNumber = int.TryParse(idToDeleteInput, out int parsedIdToDelete) && parsedIdToDelete > 0;
                if (!isValidNumber)
                {
                    Menu.PrintError("Input a valid integer ID > 0");
                    continue;
                }

                string findIdQuery = $"SELECT * FROM steps WHERE id={parsedIdToDelete}";
                SqliteCommand findId = new(findIdQuery, connection);
                SqliteDataReader findIdReader = findId.ExecuteReader();

                if (!findIdReader.HasRows)
                {
                    Menu.PrintError("ID wasn't found");
                    continue;
                }

                string deleteQuery = $"DELETE from steps WHERE id={parsedIdToDelete}";
                SqliteCommand deleteSteps = new(deleteQuery, connection);
                deleteSteps.ExecuteNonQuery();

                Menu.PrintCyan($"Deleted step entry no.{parsedIdToDelete}");
                Menu.EnterToContinue();
                break;
            }
            break;
    }
    if (exit) break;
}
Console.Clear();
