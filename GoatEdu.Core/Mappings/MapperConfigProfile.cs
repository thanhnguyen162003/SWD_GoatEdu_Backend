using AutoMapper;
using GoatEdu.Core.DTOs.NotificationDto;
using Infrastructure;

namespace GoatEdu.Core.Mappings;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<Notification, NotificationResponseDto>().ReverseMap();
    }
}