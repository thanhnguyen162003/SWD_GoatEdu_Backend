namespace GoatEdu.Core.Interfaces.ModeratorInterfaces;

public interface IModeratorRepository
{
    Task ApprovedDiscussions(List<Guid> guids);
}