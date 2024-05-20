using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Mappings;
using GoatEdu.Core.Services;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using MailKit;
using IMailService = GoatEdu.Core.Interfaces.MailInterfaces.IMailService;
using MailService = GoatEdu.Core.Services.MailService;

namespace GoatEdu.API;

public static class DI
{
    public static IServiceCollection AddWebAPIService(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        
        
        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<INotificationService, NotificationService>();
        
        
        // Others
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<JWTGenerator, JWTConfig>();
        services.AddScoped<ICurrentTime, CurrentTime>();
        return services;
    }
}