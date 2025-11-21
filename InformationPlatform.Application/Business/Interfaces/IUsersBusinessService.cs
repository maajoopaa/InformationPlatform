using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IUsersBusinessService : IBaseBusinessService
{
    Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserDto?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);
}