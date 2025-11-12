using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business;

public class UserSettingsBusinessService : IUserSettingsBusinessService
{
    public Task UpdateUserSettingsAsync(Guid userId, UpdateUserSettingsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}