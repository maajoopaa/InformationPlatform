using InformationPlatform.Repository.Repositories.Interfaces;

namespace InformationPlatform.Repository;

public interface IUnitOfWork
{
    IChatsRepository Chats { get; }
    ICommentsRepository Comments { get; }
    IImagesRepository Images { get; }
    ILikesRepository Likes { get; }
    IMessagesRepository Messages { get; }
    IPostsRepository Posts { get; }
    IUserSettingsRepository UserSettings { get; }
    IUsersRepository Users { get; }
}