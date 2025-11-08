using InformationPlatform.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace InformationPlatform.Repository;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IChatsRepository chats, ICommentsRepository comments, IImagesRepository images, 
        ILikesRepository likes, IMessagesRepository messages, IPostsRepository posts, IUserSettingsRepository userSettings,
        IUsersRepository users)
    {
        Chats =  chats;
        Comments = comments;
        Images = images;
        Likes = likes;
        Messages = messages;
        Posts = posts;
        UserSettings = userSettings;
        Users = users;
    }

    public IChatsRepository Chats { get; }
    public ICommentsRepository Comments { get; }
    public IImagesRepository Images { get; }
    public ILikesRepository Likes { get; }
    public IMessagesRepository Messages { get; }
    public IPostsRepository Posts { get; }
    public IUserSettingsRepository UserSettings { get; }
    public IUsersRepository Users { get; }
}