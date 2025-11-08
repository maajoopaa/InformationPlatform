using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class UserSettingsRepository : BaseRepository<DbUserSettings,InformationPlatformDbContext>, IUserSettingsRepository
{
    public UserSettingsRepository(InformationPlatformDbContext context) 
        : base(context) { }
}