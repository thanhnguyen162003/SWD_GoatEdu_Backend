using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AdminRepository : BaseRepository<User>, IAdminRepository
{
    private readonly GoatEduContext _context;
    public AdminRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ResponseDto> SuppenseUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return new ResponseDto (HttpStatusCode.NotFound,"User not found" );
        }

        user.IsDeleted = true;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK,"User marked as deleted successfully");
    }
    
}