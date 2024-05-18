using AutoMapper;
using GoatEdu.Core.DTOs.RoleDto;

namespace Infrastructure.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role, RoleResponseDto>();
        CreateMap<RoleResponseDto, Role>();

      
    }
}