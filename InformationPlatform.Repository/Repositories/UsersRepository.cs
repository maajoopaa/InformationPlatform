using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class UsersRepository : BaseRepository<DbUser,InformationPlatformDbContext>, IUsersRepository
{
    public UsersRepository(InformationPlatformDbContext context) 
        : base(context) { }

    public Task<DbUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Username == username,cancellationToken);
    }
}