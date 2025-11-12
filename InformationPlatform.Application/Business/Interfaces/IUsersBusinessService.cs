using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IUsersBusinessService
{
    Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserDto?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}