using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;
using Templates.Repositories.Interfaces;

namespace InformationPlatform.Repository.Repositories;

public class ChatsRepository : BaseRepository<DbChat,InformationPlatformDbContext>, IChatsRepository
{
    public ChatsRepository(InformationPlatformDbContext context) 
        : base(context) { }
}