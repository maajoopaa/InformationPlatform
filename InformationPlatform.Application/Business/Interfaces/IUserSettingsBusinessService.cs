using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IUserSettingsBusinessService
{
    Task UpdateUserSettingsAsync(Guid userId, UpdateUserSettingsRequest request, CancellationToken cancellationToken);
}