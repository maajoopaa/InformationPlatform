using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class CommentsRepository : BaseRepository<DbComment,InformationPlatformDbContext>, ICommentsRepository
{
    public CommentsRepository(InformationPlatformDbContext context) 
        : base(context) { }
}