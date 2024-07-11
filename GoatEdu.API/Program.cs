/*
+@ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @+
   @@       o o                                           @@
   @@       | |                                           @@
   @@      _L_L_                                          @@
   @@   ❮\/__-__\/❯ Programming isn't about what you know @@
   @@   ❮(|~o.o~|)❯  It's about what you can figure out   @@
   @@   ❮/ \`-'/ \❯                                       @@
   @@     _/`U'\_                                         @@
   @@    ( .   . )     .----------------------------.     @@
   @@   / /     \ \    | while( ! (succeed=try() ) ) |     @@
   @@   \ |  ,  | /    '----------------------------'     @@
   @@    \|=====|/                                        @@
   @@     |_.^._|                                         @@
   @@     | |"| |                                         @@
   @@     ( ) ( )   Testing leads to failure              @@
   @@     |_| |_|   and failure leads to understanding    @@
   @@ _.-' _j L_ '-._                                     @@
   @@(___.'     '.___)                                    @@
   +@ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @+
*/
using System.Text;
using GoatEdu.API;
using GoatEdu.API.Middelware;
using GoatEdu.Core.DTOs.MailDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Services.BackgroudTask;
using GoatEdu.Core.Services.SignalR;
using HealthChecks.UI.Client;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.CacheRepository;
using Infrastructure.Ultils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaypalCheckoutExample.Clients;
using Stripe;
using SwaggerThemes;


var builder = WebApplication.CreateBuilder(args);

//Other Config
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Register RoleRepository first
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Then decorate it with CachedRoleRepository
builder.Services.Decorate<IRoleRepository, CachedRoleRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.Decorate<ISubjectRepository, CacheSubjectRepository>();


//paypal payment 
builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);

//add Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

// Register Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    var redisConnection = builder.Configuration.GetConnectionString("Redis");
    var redisPassword = builder.Configuration["ConnectionStrings:RedisPassword"];
    options.Configuration = $"{redisConnection},password={redisPassword}";
});
builder.Services.AddDistributedMemoryCache();

//add Stripe Payment Gateway
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ChargeService>();
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeOptions:SecretKey");

//dbcontext
builder.Services.AddDbContext<GoatEduContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();
builder.Services.AddWebApiService();
builder.Services.AddControllers();

//JWT Config
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWTSetting:ValidIssuer"],
        ValidAudience = builder.Configuration["JWTSetting:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSetting:SecurityKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("Authentication failed: {0}", context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Token validated for user: {0}", context.Principal.Identity.Name);
            return Task.CompletedTask;
        }
    };
});
StripeConfiguration.ApiKey = builder.Configuration["StripeOptions:SecretKey"];

//Backgroud Service
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>());
//
// builder.Services.AddHostedService(
//     provider => provider.GetRequiredService<FlashcardHostedService>());

// fluent mail config
var mailSetting = configuration.GetSection("GmailSetting").Get<MailSetting>();

builder.Services.AddFluentEmail(mailSetting.Mail)
    .AddSmtpSender(mailSetting.SmtpServer, mailSetting.Port,
        mailSetting.DisplayName, mailSetting.Password)
    .AddRazorRenderer();

builder.Services.AddEndpointsApiExplorer();

//SwaggerConfig
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "GoatEdu API", Version = "v2" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
// add health check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddRedis(builder.Configuration.GetConnectionString("RedisCheck"));

//config unlock potential of upload image
//Set size limit for request
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10mb*1024*1024
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10mb*1024*1024
});

// Add SignalR
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//random theme =)))
var availableThemes = new[] { Theme.OneDark, Theme.UniversalDark, Theme.XCodeLight, Theme.Sepia, Theme.Dracula, Theme.NordDark }; 
var randomIndex = new Random().Next(availableThemes.Length); // 0 to (array length - 1)
var selectedTheme = availableThemes[randomIndex];
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerThemes(selectedTheme);
    app.UseSwaggerUI();
// }
//Cors config
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000", "https://www.goatedu.tech", "https://goat-edu-admin.vercel.app","http://localhost:5500")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithExposedHeaders("X-Pagination");
});
app.UseHttpsRedirection();

app.MapMethods("/background", new[] { "PATCH" }, (
    PeriodicHostedServiceState state, 
    PeriodicHostedService service) =>
{
    service.IsEnabled = true;// can config later
});

// app.MapMethods("/flashcardbackground", new[] { "PATCH" }, (
//     PeriodicHostedServiceState state, 
//     FlashcardHostedService service) =>
// {
//     service.IsEnabled = true;// can config later
// });
// using middelware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication(); // Ensure this is before UseAuthorization

app.UseAuthorization();

app.MapControllers();

// SignalR
app.MapHub<HubService>("/hub");

app.Run();