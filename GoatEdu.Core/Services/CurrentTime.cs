using GoatEdu.Core.Interfaces.GenericInterfaces;

namespace GoatEdu.Core.Services;

public class CurrentTime : ICurrentTime
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow.AddHours(7);
    }
}