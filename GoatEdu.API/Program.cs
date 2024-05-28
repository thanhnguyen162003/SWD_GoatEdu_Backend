using System.Text;
using GoatEdu.API;
using GoatEdu.Core.DTOs.MailDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.CacheRepository;
using Infrastructure.Ultils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaypalCheckoutExample.Clients;
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
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "GoatEdu API", Version = "v1" });
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
var app = builder.Build();

//random theme =)))
var availableThemes = new[] { Theme.OneDark, Theme.UniversalDark, Theme.XCodeLight, Theme.Sepia }; 
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
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure this is before UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();