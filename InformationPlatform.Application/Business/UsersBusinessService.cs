using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Helpers;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Business;

namespace InformationPlatform.Application.Business;

public class UsersBusinessService :BaseBusinessService, IUsersBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersBusinessService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var userEntity = await _unitOfWork.Users.GetByUsernameAsync(username, cancellationToken);
        
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var userEntities = await _unitOfWork.Users.GetAsync(null, cancellationToken);
        
        return _mapper.Map<List<UserDto>>(userEntities);
    }
}