using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<User>> GetUsers(UserQueryFilter queryFilter)
    {
        var users = _entities
            .Include(x => x.Role) // Include the Role navigation property
            .AsQueryable();

        users = ApplyFilterSortAndSearch(users, queryFilter);
        users = ApplySorting(users, queryFilter);

        return await users.ToListAsync();
    }

    public async Task<ICollection<User>> GetStudent(UserQueryFilter queryFilter)
    {
        var users = _entities.Where(x => x.Role.RoleName == IdEnumeration.STUDENT_ROLE)
            .Include(x => x.Role)
            .AsQueryable();
        users = ApplyFilterSortAndSearch(users, queryFilter);
        users = ApplySorting(users, queryFilter);
        return await users.ToListAsync();
    }

    public async Task<ICollection<User>> GetTeacher(UserQueryFilter queryFilter)
    {
        var users = _entities.Where(x => x.Role.RoleName == IdEnumeration.TEACHER_ROLE)
            .Include(x => x.Role)
            .AsQueryable();
        users = ApplyFilterSortAndSearch(users, queryFilter);
        users = ApplySorting(users, queryFilter);
        return await users.ToListAsync();
    }

    private IQueryable<User> ApplyFilterSortAndSearch(IQueryable<User> users, UserQueryFilter queryFilter)
    {
        users = users.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            users = users.Where(x => x.Email.Contains(queryFilter.search));
        }
        return users;
    }
    
    private IQueryable<User> ApplySorting(IQueryable<User> users, UserQueryFilter queryFilter)
    {
        users = queryFilter.sort.ToLower() switch
        {
            "name" => queryFilter.sort_direction.ToLower() == "desc"
                ? users.OrderByDescending(x => x.Email)
                : users.OrderBy(x => x.Email),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? users.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Email)
                : users.OrderBy(x => x.CreatedAt).ThenBy(x => x.Email),
        };
        return users;
    }
    
}