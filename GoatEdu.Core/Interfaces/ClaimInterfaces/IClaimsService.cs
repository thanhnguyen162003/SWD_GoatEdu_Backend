namespace GoatEdu.Core.Interfaces.ClaimInterfaces;

public interface IClaimsService
{
    public Guid GetCurrentUserId {  get; }
    public string GetCurrentUsername {  get; }
    public string GetCurrentFullname {  get; }
    public string GetCurrentEmail {  get; }

}