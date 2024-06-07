using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.ReportDto;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.DTOs.TagDto;
using Infrastructure;
using DiscussionResponseDto = GoatEdu.API.Response.DiscussionResponseDto;
using LoginResponseDto = GoatEdu.Core.DTOs.LoginResponseDto;

namespace GoatEdu.API.Mapping;

public class MapperConfigController : Profile
{
    public MapperConfigController()
    {
        CreateMap<SubjectDto, SubjectCreateDto>()
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.ImageConvert))
            .ReverseMap();
        CreateMap<ChapterDto, ChapterCreateDto>().ReverseMap();
        CreateMap<ChapterDto, ChapterResponseDto>().ReverseMap();
        CreateMap<CreateUserDto, CreateUserRequestDto>().ReverseMap();
        CreateMap<CreateUserDto, CreateUserResponse>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionRequestDto>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionResponseDto>().ReverseMap();
        CreateMap<NoteDto, NoteRequestDto>().ReverseMap();
        CreateMap<NoteDto, NoteResponseDto>().ReverseMap();
        CreateMap<NotificationDto, NotificationRequestDto>().ReverseMap();
        CreateMap<NotificationDto, NotiDetailResponseDto>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardCreateDto>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardUpdateDto>().ReverseMap();
        CreateMap<ReportDto, ReportRequestDto>().ReverseMap();
        CreateMap<TagDto, TagRequestDto>().ReverseMap();
        CreateMap<TagDto, TagResponseDto>().ReverseMap();
        CreateMap<UserUpdateDto, UserUpdateDto>().ReverseMap();
        CreateMap<RoleDto, RoleResponseDto>().ReverseMap();
        CreateMap<UserUpdateDto, UserUpdateDto>().ReverseMap();
        CreateMap<ReportDto, SubjectResponseDto>().ReverseMap();
        CreateMap<SubjectDto, SubjectResponseDto>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardResponseDto>()
            .ForMember(dest => dest.numberOfFlashcardContent, opt => opt.MapFrom(src => src.numberOfFlashcardContent))
            .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.fullName))
            .ReverseMap();
        CreateMap<FlashcardDto, FlashcardDetailResponse>()
            .ReverseMap();

        CreateMap<ChapterDto, ChapterSubjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.ChapterName))
            .ForMember(dest => dest.ChapterLevel, opt => opt.MapFrom(src => src.ChapterLevel))
            .ReverseMap();
        
        CreateMap<DiscussionDto, DiscussionDetailResponseDto>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionResponseDto>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionRequestDto>()
            .ForMember(dest => dest.DiscussionImage, opt => opt.MapFrom(src => src.DiscussionImageConvert))
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ReverseMap();
        
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