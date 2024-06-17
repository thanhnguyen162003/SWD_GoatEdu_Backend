using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.WalletInterfaces;

public interface IWalletRepository
{
    Task<Guid> CreateWallet();
   
}