using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.API.Request.LessonViewModel;
using GoatEdu.API.Response;
using GoatEdu.API.Response.LessonViewModel;
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
        // Subject
        CreateMap<SubjectDto, SubjectCreateModel>()
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.ImageConvert))
            .ReverseMap();
        CreateMap<SubjectDto, SubjectResponseModel>().ReverseMap();
        
        // Chapter
        CreateMap<ChapterDto, ChapterCreateModel>().ReverseMap();
        CreateMap<ChapterDto, ChapterResponseModel>().ReverseMap();
        CreateMap<ChapterDto, ChapterUpdateModel>().ReverseMap();
        CreateMap<ChapterSubjectDto, ChapterResponseModel>().ReverseMap();
        CreateMap<ChapterDto, ChapterSubjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.ChapterName))
            .ForMember(dest => dest.ChapterLevel, opt => opt.MapFrom(src => src.ChapterLevel))
            .ReverseMap();
        
        // User
        CreateMap<CreateUserDto, CreateUserRequestModel>().ReverseMap();
        CreateMap<CreateUserDto, CreateUserResponseModel>().ReverseMap();
        CreateMap<UserUpdateModel, UserUpdateModel>().ReverseMap(); // ??? what the hell is this ?
        CreateMap<UserUpdateModel, UserUpdateModel>().ReverseMap();
        
        // Note
        CreateMap<NoteDto, NoteRequestModel>().ReverseMap();
        CreateMap<NoteDto, NoteUpdateModel>().ReverseMap();
        CreateMap<NoteDto, NoteResponseModel>().ReverseMap();
        
        // Notification
        CreateMap<NotificationDto, NotificationRequestModel>().ReverseMap();
        CreateMap<NotificationDto, NotiDetailResponseModel>().ReverseMap();
        
        // Flashcard
        CreateMap<FlashcardDto, FlashcardCreateModel>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardUpdateModel>().ReverseMap();
        CreateMap<FlashcardDto, FlashcardResponseModel>()
            .ForMember(dest => dest.numberOfFlashcardContent, opt => opt.MapFrom(src => src.numberOfFlashcardContent))
            .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.fullName))
            .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src.subjectName))
            .ReverseMap();
        CreateMap<FlashcardDto, FlashcardDetailResponseModel>()
            .ReverseMap();
        
        // Report
        CreateMap<ReportDto, ReportRequestModel>().ReverseMap();
        CreateMap<ReportDto, SubjectResponseModel>().ReverseMap(); // ??? what the hell is this ?
        
        // Tag
        CreateMap<TagDto, TagRequestModel>().ReverseMap();
        CreateMap<TagDto, TagUpdateModel>().ReverseMap();
        CreateMap<TagDto, TagResponseModel>().ReverseMap();
        
        // Role
        CreateMap<RoleDto, RoleResponseModel>().ReverseMap();

        // Discussion
        CreateMap<DiscussionDto, DiscussionRequestModel>()
            .ForMember(dest => dest.DiscussionImage, opt => opt.MapFrom(src => src.DiscussionImageConvert))
            .ReverseMap();
        CreateMap<DiscussionDto, DiscussionUpdateModel>()
            .ForMember(dest => dest.DiscussionImage, opt => opt.MapFrom(src => src.DiscussionImageConvert))
            .ReverseMap();
        CreateMap<DiscussionDto, DiscussionResponseModel>().ReverseMap();
        CreateMap<DiscussionDto, DiscussionDetailResponseModel>().ReverseMap();
        
        // Login
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.emailVerify, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        
        // Answer
        CreateMap<AnswerDto, AnswerRequestModel>()
            .ForMember(dest => dest.AnswerImage, opt => opt.MapFrom(src => src.AnswerImageConvert))
            .ReverseMap();
        CreateMap<AnswerDto, AnswerUpdateModel>()
            .ForMember(dest => dest.AnswerImage, opt => opt.MapFrom(src => src.AnswerImageConvert))
            .ReverseMap();
        CreateMap<AnswerDto, AnswerResponseModel>().ReverseMap();
        
        // Lesson
        CreateMap<LessonDto, LessonRequestModel>().ReverseMap();
        CreateMap<LessonDto, LessonUpdateModel>().ReverseMap();
        CreateMap<LessonDto, LessonResponseModel>().ReverseMap();
        CreateMap<LessonDto, LessonDetailResponseModel>().ReverseMap();
        
        // Mapping for PageList
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListTypeConverter<,>));
            
    }
}