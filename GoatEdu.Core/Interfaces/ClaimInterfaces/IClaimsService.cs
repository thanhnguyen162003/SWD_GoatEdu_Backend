namespace GoatEdu.Core.Interfaces.ClaimInterfaces;

public interface IClaimsService
{
    public Guid? GetCurrentUserId {  get; }
}