using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business;

public class UserSettingsBusinessService : BaseBusinessService, IUserSettingsBusinessService
{
    public UserSettingsBusinessService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        
    }
    public Task UpdateUserSettingsAsync(Guid userId, UpdateUserSettingsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}