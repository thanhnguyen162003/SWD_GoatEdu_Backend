using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.QuestionInQuizDto;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;
using Infrastructure;

namespace GoatEdu.Core.Mappings;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<Notification, NotificationDto>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Subject, SubjectDto>()
            .ForMember(dest => dest.NumberOfChapters, opt => opt.MapFrom(src => src.Chapters.Count(x => x.IsDeleted == false)))
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
            .ReverseMap();

        CreateMap<Chapter, ChapterSubjectDto>().ReverseMap();
       
        CreateMap<FlashcardContent, FlashcardContentDto>().ReverseMap();
        
        CreateMap<Flashcard, FlashcardDto>()
            .ForMember(dest => dest.numberOfFlashcardContent, opt => opt.MapFrom(src => src.FlashcardContents.Count(x => x.IsDeleted == false)))
            .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForMember(dest => dest.userImage, opt => opt.MapFrom(src => src.User.Image))
            .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.User.Id))
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
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Answers.Count(x => x.IsDeleted == false)))
            .ForPath(dest => dest.UserAndSubject.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForPath(dest => dest.UserAndSubject.UserName, opt => opt.MapFrom(src => src.User.Username))
            .ForPath(dest => dest.UserAndSubject.FullName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForPath(dest => dest.UserAndSubject.UserImage, opt => opt.MapFrom(src => src.User.Image))
            .ForPath(dest => dest.UserAndSubject.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
            .ForPath(dest => dest.UserAndSubject.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      
        CreateMap<DiscussionDto, Discussion>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.emailVerify, opt => opt.MapFrom(src => src.EmailVerify))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.isNewUser, opt => opt.MapFrom(src => src.IsNewUser))
            .ForMember(dest => dest.subscription, opt =>opt.Ignore())
            .ReverseMap();


        CreateMap<Answer, AnswerDto>()
            .ForPath(dest => dest.UserInformation.UserId, opts => opts.MapFrom(src => src.UserId))
            .ForPath(dest => dest.UserInformation.UserImage, opts => opts.MapFrom(src => src.User.Image))
            .ForPath(dest => dest.UserInformation.FullName, opts => opts.MapFrom(src => src.User.Fullname))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<AnswerDto, Answer>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
        
        CreateMap<Lesson, LessonDto>()
            .ForMember(dest => dest.QuizCount, opt => opt.MapFrom(src => src.Quizzes.Count(x => x.IsDeleted == false)))
            .ForMember(dest => dest.TheoryCount, opt => opt.MapFrom(src => src.Theories.Count(x => x.IsDeleted == false)))
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Theory, TheoryDto>()
            .ForMember(dest => dest.FlashcardCount, opts => opts.MapFrom(src => src.TheoryFlashCardContents.Count(x => x.IsDeleted == false)))
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<TheoryFlashCardContent, TheoryFlashcardContentsDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Quiz, QuizDto>()
            .ForMember(dest => dest.QuestionCount, opts => opts.MapFrom(src => src.QuestionInQuizzes.Count(x => x.IsDeleted == false)))
            .ForMember(dest => dest.QuestionInQuizzes, opts => opts.MapFrom(src => src.QuestionInQuizzes))
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;

        CreateMap<QuestionInQuiz, QuestionInQuizDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        
    }
}