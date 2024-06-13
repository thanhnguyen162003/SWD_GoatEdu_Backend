using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.RateInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RateRepository : IRateRepository
{
    private readonly GoatEduContext _context;

    public RateRepository(GoatEduContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> RateFlashcard(Rate rate)
    {
        _context.Rates.Add(rate);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Create Success");
    }

    public async Task<bool> IsRated(Guid userId, Guid flashcardId)
    {
        throw new NotImplementedException();
    }
}