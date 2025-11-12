using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IAuthorizationBusinessService
{
    Task<AuthorizationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<AuthorizationResponse> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken);
}