using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class UsersRepository : BaseRepository<DbUser,InformationPlatformDbContext>, IUsersRepository
{
    public UsersRepository(InformationPlatformDbContext context) 
        : base(context) { }
}