using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
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
using LoginResponseDto = GoatEdu.Core.DTOs.LoginResponseDto;

namespace GoatEdu.API.Mapping;

public class MapperConfigController : Profile
{
    public MapperConfigController()
    {
        CreateMap<SubjectDto, SubjectCreateModel>()
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.ImageConvert))
            .ReverseMap();
        CreateMap<ChapterDto, ChapterCreateModel>().ReverseMap();
        CreateMap<ChapterDto, ChapterResponseModel>().ReverseMap();
        CreateMap<CreateUserDto, CreateUserRequestModel>().ReverseMap();
        CreateMap<CreateUserDto, CreateUserResponseModel>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionRequestModel>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionResponseModel>().ReverseMap();
        CreateMap<NoteDto, NoteRequestModel>().ReverseMap();
        CreateMap<NoteDto, NoteResponseModel>().ReverseMap();
        CreateMap<NotificationDto, NotificationRequestModel>().ReverseMap();
        CreateMap<NotificationDto, NotiDetailResponseModel>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardCreateModel>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardUpdateModel>().ReverseMap();
        CreateMap<ReportDto, ReportRequestModel>().ReverseMap();
        CreateMap<TagDto, TagRequestModel>().ReverseMap();
        CreateMap<TagDto, TagResponseModel>().ReverseMap();
        CreateMap<UserUpdateModel, UserUpdateModel>().ReverseMap();
        CreateMap<RoleDto, RoleResponseModel>().ReverseMap();
        CreateMap<UserUpdateModel, UserUpdateModel>().ReverseMap();
        CreateMap<ReportDto, SubjectResponseModel>().ReverseMap();
        CreateMap<SubjectDto, SubjectResponseModel>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardResponseModel>()
            .ForMember(dest => dest.numberOfFlashcardContent, opt => opt.MapFrom(src => src.numberOfFlashcardContent))
            .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.fullName))
            .ReverseMap();
        CreateMap<FlashcardDto, FlashcardDetailResponseModel>()
            .ReverseMap();

        CreateMap<ChapterDto, ChapterSubjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.ChapterName))
            .ForMember(dest => dest.ChapterLevel, opt => opt.MapFrom(src => src.ChapterLevel))
            .ReverseMap();

        CreateMap<DiscussionUpdateDto, DiscussionRequestModel>()
            .ForMember(dest => dest.DiscussionImage, opt => opt.MapFrom(src => src.DiscussionImageConvert))
            .ReverseMap();
        CreateMap<DiscussionDto, DiscussionDetailResponseModel>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionResponseModel>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionRequestModel>()
            .ForMember(dest => dest.DiscussionImage, opt => opt.MapFrom(src => src.DiscussionImageConvert))
            .ReverseMap();
        
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.emailVerify, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<AnswerDto, AnswerRequestModel>()
            .ForMember(dest => dest.AnswerImage, opt => opt.MapFrom(src => src.AnswerImageConvert))
            .ReverseMap();

        CreateMap<AnswerDto, AnswerResponseModel>().ReverseMap();
        
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListTypeConverter<,>));
    }
}