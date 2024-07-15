using AutoMapper;

namespace GoatEdu.API.Mapping;

public class StringToGuidConverter : IValueConverter<string, Guid?>
{
    public Guid? Convert(string sourceMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(sourceMember))
        {
            return Guid.Empty;
        }
        
        return Guid.TryParse(sourceMember, out Guid result) ? result : Guid.Empty;
    }
}