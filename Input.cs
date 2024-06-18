namespace SqliteStepTracker;

internal static class Input
{
    internal static bool IsValidMainMenuInput(string? input)
    {
        bool isValidInput = (input != null) &&
                            int.TryParse(input, out int parsedInput) &&
                            parsedInput >= 0 &&
                            parsedInput <= 4;
        return isValidInput;
    }

    internal static bool IsValidStepsInput(string? steps)
    {
        bool isValidInput = int.TryParse(steps, out int parsedSteps) && parsedSteps >= 0;
        return isValidInput;
    }

    internal static bool IsValidIdInput(string? id)
    {
        bool isValidId = int.TryParse(id, out int parsedId) && parsedId > 0;
        return isValidId;
    }
}
