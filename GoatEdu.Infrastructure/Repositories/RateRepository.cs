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

    public async Task GetNumberRating()
    {
        var flashcards = await _context.Flashcards.ToListAsync();

        foreach (var flashcard in flashcards)
        {
            var ratings = await _context.Rates
                                        .Where(r => r.FlashcardId == flashcard.Id)
                                        .ToListAsync();

            if (ratings.Any())
            {
                flashcard.Star = (int)ratings.Average(r => r.RateValue.GetValueOrDefault());
            }
            else
            {
                flashcard.Star = 0; // or null, depending on preference
            }

            _context.Flashcards.Update(flashcard);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ResponseDto> RateFlashcard(Rate rate)
    {
        _context.Rates.Add(rate);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Create Success");
    }

    public async Task<ResponseDto> GetUserRateFlashcard(Guid userId, Guid flashcardId)
    {
        var result = await _context.Rates
                                   .AsNoTracking()
                                   .Where(x => x.UserId == userId && x.FlashcardId == flashcardId)
                                   .FirstOrDefaultAsync();

        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "You have not rated this flashcard yet");
        }

        return new ResponseDto(HttpStatusCode.OK, "Here is your rate", result.RateValue);
    }

    public async Task<bool> IsFlashcardRated(Guid userId, Guid flashcardId)
    {
        var result = await _context.Rates.AsNoTracking().Where(x => x.UserId == userId && x.FlashcardId == flashcardId)
            .FirstOrDefaultAsync();
        if (result is null)
        {
            return false;
        }
        return true;
    }

}