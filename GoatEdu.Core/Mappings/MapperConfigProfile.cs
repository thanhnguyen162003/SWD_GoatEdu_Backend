using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.DTOs.TagDto;
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
        CreateMap<Chapter, ChapterDto>().ReverseMap();
        CreateMap<ChapterDto, Chapter>().ReverseMap();
        CreateMap<Chapter, ChapterResponseDto>().ReverseMap();
        CreateMap<ChapterResponseDto, Chapter>().ReverseMap();
        CreateMap<SubjectResponseDto, Subject>().ReverseMap();
        CreateMap<Note, NoteResponseDto>().ReverseMap();
        CreateMap<Note, NoteRequestDto>().ReverseMap();
        CreateMap<Note, NoteDetailResponseDto>().ReverseMap();
        CreateMap<Tag, TagRequestDto>().ReverseMap();
        CreateMap<Tag, TagResponseDto>().ReverseMap();
        CreateMap<Discussion, DiscussionRequestDto>().ReverseMap();
        CreateMap<Discussion, DiscussionResponseDto>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<CreateUserResponse, User>().ReverseMap();
        CreateMap<Discussion, DiscussionDetailResponseDto>()
            .ForPath(dest => dest.UserAndSubject.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForPath(dest => dest.UserAndSubject.UserName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForPath(dest => dest.UserAndSubject.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
            .ForPath(dest => dest.UserAndSubject.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName));
        
        
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