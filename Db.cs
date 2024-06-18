using Microsoft.Data.Sqlite;

namespace SqliteStepTracker;

internal class Db
{
    internal static bool FindId(int parsedId, SqliteConnection connection)
    {
        string findIdQuery = $"SELECT * FROM steps WHERE id={parsedId}";
        SqliteCommand findId = new(findIdQuery, connection);
        SqliteDataReader findIdReader = findId.ExecuteReader();
        return findIdReader.HasRows;
    }

    internal static void UpdateSteps(int parsedId, int parsedSteps, SqliteConnection connection)
    {
        string updateQuery = $"UPDATE steps SET steps = {parsedSteps} WHERE id = {parsedId}";
        SqliteCommand updateSteps = new(updateQuery, connection);
        updateSteps.ExecuteNonQuery();
    }

    internal static void DeleteSteps(int parsedId, SqliteConnection connection)
    {
        string deleteQuery = $"DELETE from steps WHERE id={parsedId}";
        SqliteCommand deleteSteps = new(deleteQuery, connection);
        deleteSteps.ExecuteNonQuery();
    }

    internal static SqliteConnection InitializeDb(string path)
    {
        SqliteConnection connection = new(path);
        connection.Open();
        SqliteCommand createTable = new(Queries.createTable, connection);
        createTable.ExecuteNonQuery();

        return connection;
    }
}
