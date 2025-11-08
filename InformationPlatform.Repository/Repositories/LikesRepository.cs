using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class LikesRepository : BaseRepository<DbLike,InformationPlatformDbContext>, ILikesRepository
{
    public LikesRepository(InformationPlatformDbContext context) 
        : base(context) { }
}