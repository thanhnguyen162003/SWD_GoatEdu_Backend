using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.VoteInterface;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class VoteService : IVoteService
{
    private readonly IClaimsService _claimsService;
    private readonly ICurrentTime _currentTime;
    private readonly IUnitOfWork _unitOfWork;

    public VoteService(IClaimsService claimsService, ICurrentTime currentTime, IUnitOfWork unitOfWork)
    {
        _claimsService = claimsService;
        _currentTime = currentTime;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> DiscussionVoting(Guid guid)
    {
        var userId = _claimsService.GetCurrentUserId;
        var vote = await _unitOfWork.VoteRepository.GetDiscussionVote(guid, userId);
        var discussion = await _unitOfWork.DiscussionRepository.GetByIdAsync(guid);

        if (discussion is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Discussion not found!");
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (vote is null)
            {
                var newVote = new Vote
                {
                    UserId = userId,
                    DiscussionId = guid
                };
                await _unitOfWork.VoteRepository.AddVote(newVote);
                discussion.DiscussionVote++;
                _unitOfWork.DiscussionRepository.Update(discussion);
                var result = await _unitOfWork.SaveChangesAsync();

                return await DoTransactionAsync(result);
            }
            
            _unitOfWork.VoteRepository.DeleteVote(vote);
            discussion.DiscussionVote--;
            var save = await _unitOfWork.SaveChangesAsync();
            return await DoTransactionAsync(save);

        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ResponseDto(HttpStatusCode.InternalServerError, "Some thing went wrong while voting!", ex);
        }
    }

    public async Task<ResponseDto> AnswerVoting(Guid guid)
    {
        var userId = _claimsService.GetCurrentUserId;
        var vote = await _unitOfWork.VoteRepository.GetAnswerVote(guid, userId);
        var answer = await _unitOfWork.AnswerRepository.GetByIdAsync(guid);

        if (answer is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Answer not found!");
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (vote is null)
            {
                var newVote = new Vote
                {
                    UserId = userId,
                    DiscussionId = guid
                };
                await _unitOfWork.VoteRepository.AddVote(newVote);
                answer.AnswerVote++;
                _unitOfWork.AnswerRepository.Update(answer);
                var result = await _unitOfWork.SaveChangesAsync();
                return await DoTransactionAsync(result);
            }
            
            _unitOfWork.VoteRepository.DeleteVote(vote);
            answer.AnswerVote--;
            var save = await _unitOfWork.SaveChangesAsync();
            return await DoTransactionAsync(save);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ResponseDto(HttpStatusCode.InternalServerError, "Some thing went wrong while voting!", ex);
        }
    }

    private async Task<ResponseDto> DoTransactionAsync(int result)
    {
        if (result > 0)
        {
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseDto(HttpStatusCode.OK, "Vote Successfully!");
        }
        await _unitOfWork.RollbackTransactionAsync();
        return new ResponseDto(HttpStatusCode.BadRequest, "Vote Failed!");
    }
}