using InformationPlatform.Domain.Models;
using Templates.Repositories.Interfaces;

namespace InformationPlatform.Repository.Repositories.Interfaces;

public interface IUsersRepository : IBaseRepository<DbUser>
{
    Task<DbUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}