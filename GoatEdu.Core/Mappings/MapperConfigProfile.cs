using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.FlashcardDto;
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
        CreateMap<Notification, NotificationDto>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Subject, SubjectDto>()
            .ForMember(dest => dest.NumberOfChapters, opt => opt.MapFrom(src => src.Chapters.Count))
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
            .ReverseMap();

        CreateMap<Chapter, ChapterSubjectDto>().ReverseMap();
       
        CreateMap<FlashcardContent, FlashcardContentDto>().ReverseMap();
        
        CreateMap<Flashcard, FlashcardDto>()
            .ForMember(dest => dest.numberOfFlashcardContent, opt => opt.MapFrom(src => src.FlashcardContents.Count))
            .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForMember(dest => dest.userImage, opt => opt.MapFrom(src => src.User.Image))
            .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
            .ReverseMap();

        // Map Chapter to ChapterSubjectDto
        CreateMap<Chapter, ChapterDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.ChapterName))
            .ForMember(dest => dest.ChapterLevel, opt => opt.MapFrom(src => src.ChapterLevel))
            .ReverseMap();

        CreateMap<Note, NoteDto>()
            .ReverseMap();
        
        CreateMap<Tag, TagDto>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<User, UserMinimalDto>()
            .ForMember(dest => dest.IsConfirmMail, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ReverseMap()
            .ForMember(dest => dest.EmailVerify, opt => opt.MapFrom(src => src.IsConfirmMail))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        CreateMap<Discussion, DiscussionDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Answers.Count))
            .ForPath(dest => dest.UserAndSubject.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForPath(dest => dest.UserAndSubject.UserName, opt => opt.MapFrom(src => src.User.Username))
            .ForPath(dest => dest.UserAndSubject.FullName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForPath(dest => dest.UserAndSubject.UserImage, opt => opt.MapFrom(src => src.User.Image))
            .ForPath(dest => dest.UserAndSubject.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
            .ForPath(dest => dest.UserAndSubject.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.emailVerify, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<Answer, AnswerDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Lesson, LessonDto>()
            .ForMember(dest => dest.QuizCount, opt => opt.MapFrom(src => src.Quizzes.Count))
            .ForMember(dest => dest.TheoryCount, opt => opt.MapFrom(src => src.Theories.Count))
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}