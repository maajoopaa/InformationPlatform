using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class MessagesRepository : BaseRepository<DbMessage,InformationPlatformDbContext>, IMessagesRepository
{
    public MessagesRepository(InformationPlatformDbContext context) 
        : base(context) { }
}