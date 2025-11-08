using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class PostsRepository : BaseRepository<DbPost,InformationPlatformDbContext>, IPostsRepository
{
    public PostsRepository(InformationPlatformDbContext context) 
        : base(context) { }
}