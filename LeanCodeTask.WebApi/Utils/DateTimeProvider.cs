namespace LeanCodeTask.WebApi.Utils;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTimeUTC()
    {
        return DateTime.UtcNow;
    }
}