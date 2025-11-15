using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IAuthorizationBusinessService : IBaseBusinessService
{
    Task<AuthorizationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<AuthorizationResponse> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken);
}