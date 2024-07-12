namespace GoatEdu.Core.Interfaces.ModeratorInterfaces;

public interface IModeratorRepository
{
    Task<Guid?> ApprovedDiscussions(Guid discussionId);
}