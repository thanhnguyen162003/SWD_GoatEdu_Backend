using AutoMapper;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Models;

namespace Infrastructure.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role, RoleResponseDto>().ReverseMap();
    
      
    }
}