using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.TranstractionInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class TranstractionRepository : BaseRepository<Transaction>, ITranstractionRepository
{
    private readonly GoatEduContext _context;

    public TranstractionRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddTranstraction(Transaction transaction)
    {
        var result = await _entities.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Successfully Added Transaction", result.Entity.Id);
    }
}