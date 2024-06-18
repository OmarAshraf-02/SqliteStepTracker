using SqliteStepTracker;
using Microsoft.Data.Sqlite;

Console.Clear();
bool exit = false;
string path = $"Data Source={Environment.CurrentDirectory}/Steps.db";
using SqliteConnection connection = Db.InitializeDb(path);

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
            Menu.StepInputLoop(ref stepsInput);

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
            Menu.PrintTable(connection);
            Menu.EnterToContinue();
            break;
        case 3:
            Menu.PrintTable(connection);
            while (true)
            {
                Console.WriteLine("Choose which entry to update");
                Console.Write("ID: ");

                string? idInput = Console.ReadLine();
                Menu.IdInputLoop(ref idInput, connection);

                _ = int.TryParse(idInput, out int parsedId);

                Console.Write($"Steps for entry no.{parsedId}: ");
                string? stepInput = Console.ReadLine();
                Menu.StepInputLoop(ref stepInput);

                _ = int.TryParse(stepInput, out int parsedSteps);

                Db.UpdateSteps(parsedId, parsedSteps, connection);
                Menu.PrintCyan($"Updated step entry no.{parsedId} to {parsedSteps} steps");
                Menu.EnterToContinue();
                break;
            }
            break;
        case 4:
            Menu.PrintTable(connection);
            while (true)
            {
                Console.WriteLine("Choose which entry to delete");
                Console.Write("ID: ");

                string? idToDeleteInput = Console.ReadLine();
                Menu.IdInputLoop(ref idToDeleteInput, connection);

                _ = int.TryParse(idToDeleteInput, out int parsedIdToDelete);

                Db.DeleteSteps(parsedIdToDelete, connection);
                Menu.PrintCyan($"Deleted step entry no.{parsedIdToDelete}");
                Menu.EnterToContinue();
                break;
            }
            break;
    }
    if (exit) break;
}
Console.Clear();
