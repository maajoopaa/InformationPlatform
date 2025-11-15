using System.Text;
using InformationPlatform.Application.Business;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Helpers;
using InformationPlatform.Application.Helpers.Interfaces;
using InformationPlatform.Application.Middlewares;
using InformationPlatform.Database;
using InformationPlatform.Repository;
using InformationPlatform.Repository.Repositories;
using InformationPlatform.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Swagger
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT in the field",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

//Database
builder.Services.AddDbContext<InformationPlatformDbContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddTransient<IChatsRepository, ChatsRepository>();
builder.Services.AddTransient<ICommentsRepository, CommentsRepository>();
builder.Services.AddTransient<IImagesRepository, ImagesRepository>();
builder.Services.AddTransient<ILikesRepository, LikesRepository>();
builder.Services.AddTransient<IMessagesRepository, MessagesRepository>();
builder.Services.AddTransient<IPostsRepository, PostsRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IUserSettingsRepository, UserSettingsRepository>();

//JWT Helper
builder.Services.AddTransient<JWTHelper>();

//Unit of work
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()               
    .WriteTo.Console()         
    .CreateLogger();

builder.Host.UseSerilog();

//Authorization
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,      
            ValidateIssuerSigningKey = true,  
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddAuthorization();

//Services
builder.Services.AddTransient<IAuthorizationBusinessService, AuthorizationBusinessService>();
builder.Services.AddTransient<IChatsBusinessService, ChatsBusinessService>();
builder.Services.AddTransient<ICommentsBusinessService, CommentsBusinessService>();
builder.Services.AddTransient<IImagesBusinessService, ImagesBusinessService>();
builder.Services.AddTransient<ILikesBusinessService, LikesBusinessService>();
builder.Services.AddTransient<IMessagesBusinessService, MessagesBusinessService>();
builder.Services.AddTransient<IPostsBusinessService, PostsBusinessService>();
builder.Services.AddTransient<IUsersBusinessService, UsersBusinessService>();
builder.Services.AddTransient<IUserSettingsBusinessService, UserSettingsBusinessService>();
builder.Services.AddTransient<IPermissionsService, PermissionsService>();



var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();
app.Run();