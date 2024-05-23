using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Models;
using Infrastructure;

namespace GoatEdu.Core.Mappings;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<Notification, NotificationResponseDto>().ReverseMap();
        CreateMap<Notification, NotificationRequestDto>().ReverseMap();
        CreateMap<Subject, SubjectResponseDto>().ReverseMap();
        CreateMap<Subject, SubjectCreateDto>().ReverseMap();
        CreateMap<Subject, SubjectDto>().ReverseMap();
        CreateMap<SubjectResponseDto, Subject>().ReverseMap();
        CreateMap<Note, NoteResponseDto>().ReverseMap();
        CreateMap<Note, NoteRequestDto>().ReverseMap();
        CreateMap<Note, NoteDetailResponseDto>().ReverseMap();
        
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.emailVerify, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}