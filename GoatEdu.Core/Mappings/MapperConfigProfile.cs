using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.RoleDto;
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

        CreateMap<Chapter, ChapterResponseDto>().ReverseMap();

        CreateMap<Subject, SubjectResponseDto>()
            .ForMember(dest => dest.NumberOfChapters, opt => opt.MapFrom(src => src.Chapters.Count))
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
            .ReverseMap();

        // Map Chapter to ChapterSubjectDto
        CreateMap<Chapter, ChapterSubjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.ChapterName))
            .ForMember(dest => dest.ChapterLevel, opt => opt.MapFrom(src => src.ChapterLevel))
            .ReverseMap();
        
        CreateMap<Note, NoteResponseDto>().ReverseMap();
        CreateMap<Note, NoteRequestDto>().ReverseMap();
        CreateMap<Note, NoteDetailResponseDto>().ReverseMap();
        CreateMap<Tag, TagRequestDto>().ReverseMap();
        CreateMap<Tag, TagResponseDto>().ReverseMap();
        CreateMap<Discussion, DiscussionRequestDto>().ReverseMap()
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
        CreateMap<Discussion, DiscussionResponseDto>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<CreateUserResponse, User>().ReverseMap();
        CreateMap<User, UserMinimalDto>()
            .ForMember(dest => dest.IsConfirmMail, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ReverseMap()
            .ForMember(dest => dest.EmailVerify, opt => opt.MapFrom(src => src.IsConfirmMail))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
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