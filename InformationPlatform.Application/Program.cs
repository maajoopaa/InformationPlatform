using FluentValidation.AspNetCore;
using InformationPlatform.Application.Helpers;
using InformationPlatform.Application.Middlewares;
using InformationPlatform.Database;
using InformationPlatform.Repository;
using InformationPlatform.Repository.Repositories;
using InformationPlatform.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();