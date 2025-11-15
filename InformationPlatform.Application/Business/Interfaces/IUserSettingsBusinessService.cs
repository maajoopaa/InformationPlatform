using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IUserSettingsBusinessService : IBaseBusinessService
{
    Task UpdateUserSettingsAsync(Guid userId, UpdateUserSettingsRequest request, CancellationToken cancellationToken);
}