using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.RateInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RateRepository : IRateRepository
{
    private readonly GoatEduContext _context;

    public RateRepository(GoatEduContext context)
    {
        _context = context;
    }

    public async Task<int> GetNumberRating(Guid flashcardId)
    {
        var ratings = await _context.Rates
                                   .Where(r => r.FlashcardId == flashcardId)
                                   .ToListAsync();

        if (ratings == null || !ratings.Any())
        {
            return 0; // No ratings found, return 0
        }

        var averageRating = ratings.Average(r => r.RateValue.GetValueOrDefault());
        return (int)averageRating;
    }

    public async Task<ResponseDto> RateFlashcard(Rate rate)
    {
        _context.Rates.Add(rate);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Create Success");
    }

    
}