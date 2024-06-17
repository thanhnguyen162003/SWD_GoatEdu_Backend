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
        if (result is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Add Transtraction Error");
        }

        return new ResponseDto(HttpStatusCode.OK, "Successfully Add Transtraction", result.Entity.Id);
    }
}