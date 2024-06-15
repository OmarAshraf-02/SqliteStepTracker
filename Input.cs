namespace SqliteStepTracker;

internal static class Input
{
    internal static bool ValidateMainMenuInput(string? input)
    {
        bool invalidInput = (input == null) ||
                            (!int.TryParse(input, out int parsedInput)) ||
                             parsedInput < 0 ||
                             parsedInput > 4;

        return !invalidInput;
    }
}
