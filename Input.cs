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

    internal static bool ValidateInsertionInput(ref string? steps)
    {
        if (!int.TryParse(steps, out int parsedSteps) || parsedSteps < 0) return false;

        return true;
    }
}
