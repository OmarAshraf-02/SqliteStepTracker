namespace SqliteStepTracker;

internal static class Queries
{
    internal static readonly string createTable = @"CREATE TABLE IF NOT EXISTS steps(
                                                    id INTEGER PRIMARY KEY,
                                                    steps INTEGER NOT NULL,
                                                    date DATE NOT NULL
                                                )";
    internal static readonly string getTable = "SELECT * FROM steps";

    internal static readonly string dropTable = "DROP TABLE steps";

    internal static readonly string dropDb = "DROP DATABASE Steps";

}
