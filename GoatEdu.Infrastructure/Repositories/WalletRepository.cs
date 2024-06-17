using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.WalletInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
{
    private readonly GoatEduContext _context;

    public WalletRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Guid> CreateWallet()
    {
        Wallet wallet = new Wallet()
        {
            NumberWallet = 0,
            CreatedAt = DateTime.Now,
            IsDeleted = false,
            UpdatedAt = DateTime.Now
        };
        var result = await _entities.AddAsync(wallet);
        if (result is null)
        {
            return Guid.Empty;
        }

        return result.Entity.Id;
    }

   
}