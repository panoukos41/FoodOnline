namespace FoodOnline;

public static class Errors
{
    public static readonly Er NotFound = new()
    {
        Error = nameof(NotFound),
        Reason = string.Empty,
    };
}
